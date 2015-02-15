namespace Fifthweek.Api
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.WebApi;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Logging;
    using Fifthweek.Shared;

    using Owin;

    public static class AutofacConfig
    {
        private static readonly Type CommandRetryDecoratorType = typeof(RetryOnTransientErrorCommandHandlerDecorator<>);
        private static readonly Type QueryRetryDecoratorType = typeof(RetryOnTransientErrorQueryHandlerDecorator<,>);

        private enum HandlerType
        {
            CommandHandler,
            QueryHandler,
            EventHandler,
        }

        public static void Register(HttpConfiguration httpConfiguration, IAppBuilder app)
        {
            var container = CreateContainer();

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);

            var resolver = new ErrorLoggingWebApiDependencyResolver(new AutofacWebApiDependencyResolver(container), container);
            GlobalConfiguration.Configuration.DependencyResolver = httpConfiguration.DependencyResolver = resolver;
        }

        internal static IContainer CreateContainer()
        {
            var builder = new ContainerBuilder();

            RegisterControllerAssemblies(builder);
            RegisterHandlers(builder);
            RegisterModules(builder);

            builder.RegisterType<OwinExceptionHandler>().As<IOwinExceptionHandler>().SingleInstance();
            builder.RegisterType<ExceptionHandler>().As<IExceptionHandler>().InstancePerRequest();
            builder.RegisterInstance(HardwiredDependencies.NewDefaultDeveloperRepository()).As<IDeveloperRepository>().SingleInstance();
            builder.RegisterInstance(HardwiredDependencies.NewDefaultSendEmailService()).As<ISendEmailService>().SingleInstance();
            builder.RegisterInstance(HardwiredDependencies.NewDefaultReportingService()).As<IReportingService>().SingleInstance();

            builder.RegisterType<HttpClient>().InstancePerDependency();

            // Uncoment this to log Autofac requests to trace output.
            //// builder.RegisterModule<LogRequestModule>();

            var container = builder.Build();
            return container;
        }

        private static void RegisterModules(ContainerBuilder builder)
        {
            var autofacRegistrationTypes = FifthweekAssembliesResolver.Assemblies
                .SelectMany(v => v.GetTypes())
                .Where(v => v.IsClass)
                .Where(v => typeof(IAutofacRegistration).IsAssignableFrom(v))
                .ToList();

            foreach (var t in autofacRegistrationTypes)
            {
                var instance = (IAutofacRegistration)Activator.CreateInstance(t);
                instance.Register(builder);
            }
        }

        private static void RegisterControllerAssemblies(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(FifthweekAssembliesResolver.Assemblies.ToArray());
        }

        private static void RegisterHandlers(
            ContainerBuilder builder)
        {
            var types = FifthweekAssembliesResolver.Assemblies.SelectMany(v => v.GetTypes()).ToList();

            var commandHandlers = (from t in types
                                   where t.IsClass
                                   from i in t.GetInterfaces()
                                   where i.IsClosedTypeOf(typeof(ICommandHandler<>))
                                   select new { Type = t, Interface = i, HandlerType = HandlerType.CommandHandler }).ToList();

            var eventHandlers = (from t in types
                                 where t.IsClass
                                 from i in t.GetInterfaces()
                                 where i.IsClosedTypeOf(typeof(IEventHandler<>))
                                 select new { Type = t, Interface = i, HandlerType = HandlerType.EventHandler }).ToList();

            var queryHandlers = (from t in types
                                 where t.IsClass
                                 from i in t.GetInterfaces()
                                 where i.IsClosedTypeOf(typeof(IQueryHandler<,>))
                                 select new { Type = t, Interface = i, HandlerType = HandlerType.QueryHandler }).ToList();

            foreach (var item in commandHandlers.Concat(queryHandlers))
            {
                var decoratorTypes = GetDecoratorTypes(item.Type, item.HandlerType);

                if (decoratorTypes.Count == 0)
                {
                    builder.RegisterType(item.Type).As(item.Interface);
                }
                else
                {
                    builder.RegisterType(item.Type)
                        .As(new KeyedService(GetDecoratedTypeName(item.Type, decoratorTypes.Count), item.Interface));

                    var decoratedType = item.Interface.GetGenericTypeDefinition();
                    for (int i = decoratorTypes.Count - 1; i >= 0; i--)
                    {
                        var decorator = decoratorTypes[i];
                        builder.RegisterGenericDecorator(
                            decorator,
                            decoratedType,
                            GetDecoratedTypeName(item.Type, i + 1),
                            i == 0 ? null : GetDecoratedTypeName(item.Type, i));
                    }
                }
            }

            foreach (var eventHandlersWithCommonInterface in eventHandlers.GroupBy(_ => _.Interface))
            {
                var handlerTypes = eventHandlersWithCommonInterface.Select(_ => _.Type).ToArray();
                var commonHandlerType = eventHandlersWithCommonInterface.Key;
                var commonEventType = commonHandlerType.GetGenericArguments().Single();
                var collectionType = typeof(IEnumerable<>).MakeGenericType(commonHandlerType);
                var aggregateHandlerType = typeof(AggregateEventHandler<>).MakeGenericType(commonEventType);
                var collectionKey = commonHandlerType.Name;

                foreach (var handlerType in handlerTypes)
                {
                    builder.RegisterType(handlerType).Named(collectionKey, commonHandlerType);
                }

                builder
                    .Register(context =>
                    {
                        var handlers = context.ResolveKeyed(collectionKey, collectionType);
                        var aggregateHandler = Activator.CreateInstance(aggregateHandlerType, handlers);
                        return aggregateHandler;
                    })
                    .As(commonHandlerType);
            }
        }

        private static List<Type> GetDecoratorTypes(Type item, HandlerType handlerType)
        {
            var result = new List<Type>();

            bool foundRetryDecorator = false;

            var decoratorAttribute = item.GetCustomAttribute<DecoratorAttribute>(false);
            bool omitDefaultDecorators = false;
            if (decoratorAttribute != null)
            {
                omitDefaultDecorators = decoratorAttribute.OmitDefaultDecorators;
                foreach (var decoratorType in decoratorAttribute.DecoratorTypes)
                {
                    if (decoratorType == CommandRetryDecoratorType || decoratorType == QueryRetryDecoratorType)
                    {
                        foundRetryDecorator = true;
                    }

                    result.Add(decoratorType);
                }
            }

            if (omitDefaultDecorators == false && foundRetryDecorator == false)
            {
                if (handlerType == HandlerType.CommandHandler)
                {
                    result.Insert(0, CommandRetryDecoratorType);
                }
                else if (handlerType == HandlerType.QueryHandler)
                {
                    result.Insert(0, QueryRetryDecoratorType);
                }
            }

            return result;
        }

        private static string GetDecoratedTypeName(Type type, int level)
        {
            return type.FullName + "{" + level + "}";
        }
    }
}