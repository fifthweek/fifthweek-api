namespace Fifthweek.Api.Identity.Membership
{
    using Autofac;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UpdateUserTimeStampsDbStatement>().As<IUpdateUserTimeStampsDbStatement>().InstancePerRequest();
            builder.RegisterType<RegisterUserDbStatement>().As<IRegisterUserDbStatement>().InstancePerRequest();
        }
    }
}