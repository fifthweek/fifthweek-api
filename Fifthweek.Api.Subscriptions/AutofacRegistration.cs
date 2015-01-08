using Autofac;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Subscriptions
{
    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<SubscriptionSecurity>().As<ISubscriptionSecurity>().InstancePerRequest();
        }
    }
}