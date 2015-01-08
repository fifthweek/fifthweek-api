namespace Fifthweek.Api.Identity.Membership
{
    using Autofac;

    using Fifthweek.Api.Core;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerRequest();
        }
    }
}