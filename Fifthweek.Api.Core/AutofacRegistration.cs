namespace Fifthweek.Api.Core
{
    using Autofac;

    using Fifthweek.Shared;

    using Microsoft.Practices.EnterpriseLibrary.TransientFaultHandling;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<TraceService>().As<ITraceService>();
            builder.RegisterType<FifthweekTransientErrorDetectionStrategy>().As<ITransientErrorDetectionStrategy>();
            builder.RegisterType<RequestContext>().As<IRequestContext>().InstancePerRequest();
        }
    }
}