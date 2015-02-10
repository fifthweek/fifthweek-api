namespace Fifthweek.Api.Availability
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<CountUsersDbStatement>().As<ICountUsersDbStatement>();
        }
    }
}