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
            builder.RegisterType<UpdateUserTimeStampsDbStatement>().As<IUpdateUserTimeStampsDbStatement>();
            builder.RegisterType<UpsertRefreshTokenDbStatement>().As<IUpsertRefreshTokenDbStatement>();
            builder.RegisterType<TryGetRefreshTokenDbStatement>().As<ITryGetRefreshTokenDbStatement>();
            builder.RegisterType<TryGetRefreshTokenByEncryptedIdDbStatement>().As<ITryGetRefreshTokenByEncryptedIdDbStatement>();
            builder.RegisterType<UpdateUserTimeStampsDbStatement>().As<IUpdateUserTimeStampsDbStatement>();
            builder.RegisterType<GetUserAndRolesFromCredentialsDbStatement>().As<IGetUserAndRolesFromCredentialsDbStatement>();
            builder.RegisterType<GetUserAndRolesFromUserIdDbStatement>().As<IGetUserAndRolesFromUserIdDbStatement>();
            builder.RegisterType<FifthweekAuthorizationServerProvider>();
            builder.RegisterType<FifthweekAuthorizationServerHandler>().As<IFifthweekAuthorizationServerHandler>();
            builder.RegisterType<FifthweekRefreshTokenProvider>();
            builder.RegisterType<FifthweekRefreshTokenHandler>().As<IFifthweekRefreshTokenHandler>();
            builder.RegisterType<ClientRepository>().As<IClientRepository>();
            builder.RegisterType<AesEncryptionService>().As<IAesEncryptionService>();
            builder.RegisterType<RefreshTokenIdEncryptionService>().As<IRefreshTokenIdEncryptionService>();
        }
    }
}