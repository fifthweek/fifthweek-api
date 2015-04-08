namespace Fifthweek.Api.Subscriptions
{
    using Autofac;

    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlogOwnership>().As<IBlogOwnership>();
            builder.RegisterType<SubscriptionSecurity>().As<ISubscriptionSecurity>();
            builder.RegisterType<GetSubscriptionDbStatement>().As<IGetSubscriptionDbStatement>();
        }
    }
}