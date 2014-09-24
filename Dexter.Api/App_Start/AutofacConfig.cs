namespace Dexter.Api
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Reflection;
    using System.Web.Http;

    using Autofac;
    using Autofac.Core;
    using Autofac.Integration.WebApi;

    using Dexter.Api.CommandHandlers;
    using Dexter.Api.Providers;
    using Dexter.Api.QueryHandlers;
    using Dexter.Api.Repositories;

    using Owin;

    public static class AutofacConfig
    {
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
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            RegisterHandlers(
                builder, 
                typeof(ICommandHandler<>),
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(ValidationCommandHandlerDecorator<>));

            RegisterHandlers(
                builder, 
                typeof(IQueryHandler<,>),
                typeof(ValidationQueryHandlerDecorator<,>));

            builder.RegisterType<DexterDbContext>().As<IDexterDbContext>().InstancePerRequest();
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerRequest();
            builder.RegisterType<RefreshTokenRepository>().As<IRefreshTokenRepository>().InstancePerRequest();
            builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerRequest();

            builder.RegisterType<DexterAuthorizationServerProvider>().SingleInstance();
            builder.RegisterType<DexterAuthorizationServerHandler>()
                .As<IDexterAuthorizationServerHandler>()
                .InstancePerRequest();
            builder.RegisterType<DexterRefreshTokenProvider>().SingleInstance();
            builder.RegisterType<DexterRefreshTokenHandler>().As<IDexterRefreshTokenHandler>().InstancePerRequest();

            builder.RegisterType<HttpClient>().InstancePerDependency();

            builder.RegisterModule<LogRequestModule>();

            var container = builder.Build();
            return container;
        }

        private static void RegisterHandlers(
            ContainerBuilder builder, 
            Type handlerType,
            params Type[] decorators)
        {
            RegisterHandlers(builder, handlerType);

            for (int i = 0; i < decorators.Length; i++)
            {
                RegisterGenericDecorator(
                    builder,
                    decorators[i],
                    handlerType,
                    i == 0 ? handlerType : decorators[i - 1],
                    i != decorators.Length - 1);
            }
        }

        private static void RegisterHandlers(ContainerBuilder builder, Type handlerType)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
                .As(t => t.GetInterfaces()
                        .Where(v => v.IsClosedTypeOf(handlerType))
                        .Select(v => new KeyedService(handlerType.Name, v)))
                .InstancePerRequest();
        }

        private static void RegisterGenericDecorator(
            ContainerBuilder builder,
            Type decoratorType,
            Type decoratedServiceType,
            Type fromKeyType,
            bool hasKey)
        {
            var result = builder.RegisterGenericDecorator(
               decoratorType,
               decoratedServiceType,
               fromKeyType.Name);

            if (hasKey)
            {
                result.Keyed(decoratorType.Name, decoratedServiceType);
            }
        }
    }
}