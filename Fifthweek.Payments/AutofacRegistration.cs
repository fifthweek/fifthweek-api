namespace Fifthweek.Payments
{
    using Autofac;

    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<RequestSnapshotService>().As<IRequestSnapshotService>();
        }
    }
}