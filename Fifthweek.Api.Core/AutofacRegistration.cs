namespace Fifthweek.Api.Core
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GuidCreator>().As<IGuidCreator>().SingleInstance();
            builder.RegisterType<TraceService>().As<ITraceService>().SingleInstance();
        }
    }
}