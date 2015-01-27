namespace Fifthweek.Api.Channels
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<ChannelOwnership>().As<IChannelOwnership>();
            builder.RegisterType<ChannelSecurity>().As<IChannelSecurity>();
        }
    }
}