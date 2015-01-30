namespace Fifthweek.Api.Identity.OAuth
{
    using Autofac;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UpsertRefreshTokenDbStatement>().As<IUpsertRefreshTokenDbStatement>();
            builder.RegisterType<RemoveRefreshTokenDbStatement>().As<IRemoveRefreshTokenDbStatement>();
            builder.RegisterType<TryGetRefreshTokenDbStatement>().As<ITryGetRefreshTokenDbStatement>();
            builder.RegisterType<UpdateUserTimeStampsDbStatement>().As<IUpdateUserTimeStampsDbStatement>();
            builder.RegisterType<GetUserAndRolesFromCredentialsDbStatement>().As<IGetUserAndRolesFromCredentialsDbStatement>();
            builder.RegisterType<GetUserAndRolesFromUserIdDbStatement>().As<IGetUserAndRolesFromUserIdDbStatement>();

            builder.RegisterType<ClientRepository>().As<IClientRepository>().SingleInstance();
            builder.RegisterType<FifthweekAuthorizationServerProvider>().SingleInstance();
            builder.RegisterType<FifthweekAuthorizationServerHandler>().As<IFifthweekAuthorizationServerHandler>().InstancePerRequest();
            builder.RegisterType<FifthweekRefreshTokenProvider>().SingleInstance();
            builder.RegisterType<FifthweekRefreshTokenHandler>().As<IFifthweekRefreshTokenHandler>().InstancePerRequest();
            builder.RegisterType<RequesterContext>().As<IRequesterContext>().InstancePerRequest();
        }
    }
}