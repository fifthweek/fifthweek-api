namespace Fifthweek.Payments.SnapshotCreation
{
    using Autofac;

    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<RequestSnapshotService>().As<IRequestSnapshotService>();
        }
    }
}