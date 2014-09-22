namespace Dexter.Api
{
    using System.Linq;
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
            var executingAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(executingAssembly);

            builder.RegisterAssemblyTypes(executingAssembly)
                .As(t => t.GetInterfaces()
                    .Where(v => v.IsClosedTypeOf(typeof(ICommandHandler<>)))
                    .Select(v => new KeyedService(typeof(ICommandHandler<>).Name, v))).InstancePerRequest();

            builder.RegisterGenericDecorator(
                typeof(TransactionCommandHandlerDecorator<>),
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<>).Name);

            builder.RegisterGenericDecorator(
                typeof(ValidationCommandHandlerDecorator<>),
                typeof(ICommandHandler<>),
                typeof(ICommandHandler<>).Name);

            builder.RegisterAssemblyTypes(executingAssembly)
                .As(t => t.GetInterfaces()
                    .Where(v => v.IsClosedTypeOf(typeof(IQueryHandler<,>)))
                    .Select(v => new KeyedService(typeof(IQueryHandler<,>).Name, v))).InstancePerRequest();

            builder.RegisterGenericDecorator(
                typeof(ValidationQueryHandlerDecorator<,>),
                typeof(IQueryHandler<,>),
                typeof(IQueryHandler<,>).Name);

            builder.RegisterType<DexterDbContext>().As<IDexterDbContext>().InstancePerRequest();
            builder.RegisterType<AuthenticationRepository>().As<IAuthenticationRepository>().InstancePerRequest();
            builder.RegisterType<RefreshTokenRepository>().As<IRefreshTokenRepository>().InstancePerRequest();
            builder.RegisterType<ClientRepository>().As<IClientRepository>().InstancePerRequest();

            builder.RegisterType<DexterAuthorizationServerProvider>().SingleInstance();
            builder.RegisterType<DexterAuthorizationServerHandler>().As<IDexterAuthorizationServerHandler>().InstancePerRequest();
            builder.RegisterType<DexterRefreshTokenProvider>().SingleInstance();
            builder.RegisterType<DexterRefreshTokenHandler>().As<IDexterRefreshTokenHandler>().InstancePerRequest();

            builder.RegisterModule<LogRequestModule>();

            var container = builder.Build();

            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(httpConfiguration);

            var resolver = new ErrorLoggingWebApiDependencyResolver(new AutofacWebApiDependencyResolver(container), container);
            GlobalConfiguration.Configuration.DependencyResolver = httpConfiguration.DependencyResolver = resolver;
        }
    }
}