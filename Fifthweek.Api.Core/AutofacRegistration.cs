namespace Fifthweek.Api.Core
{
    using Autofac;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GuidCreator>().As<IGuidCreator>().SingleInstance();
            builder.RegisterType<TraceService>().As<ITraceService>().SingleInstance();
        }
    }
}