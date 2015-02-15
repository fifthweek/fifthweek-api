namespace Fifthweek.Api.Core
{
    using Autofac;

    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<GuidCreator>().As<IGuidCreator>().SingleInstance();
            builder.RegisterType<TraceService>().As<ITraceService>().SingleInstance();
            builder.RegisterType<FifthweekTransientErrorDetectionStrategy>().As<ITransientErrorDetectionStrategy>().SingleInstance();
            builder.RegisterType<RequestContext>().As<IRequestContext>().InstancePerRequest();
        }
    }
}