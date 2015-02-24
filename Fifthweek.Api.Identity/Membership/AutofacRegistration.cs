namespace Fifthweek.Api.Identity.Membership
{
    using Autofac;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<RegisterUserDbStatement>().As<IRegisterUserDbStatement>();
            builder.RegisterType<RequesterSecurity>().As<IRequesterSecurity>().SingleInstance();
            builder.RegisterType<RequesterContext>().As<IRequesterContext>().InstancePerRequest();
            builder.RegisterType<GetAccountSettingsDbStatement>().As<IGetAccountSettingsDbStatement>();
            builder.RegisterType<UpdateAccountSettingsDbStatement>().As<IUpdateAccountSettingsDbStatement>();
        }
    }
}