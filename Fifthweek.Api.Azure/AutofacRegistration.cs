namespace Fifthweek.Api.Azure
{
    using Autofac;

    using Fifthweek.Azure;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobService>().As<IBlobService>().SingleInstance();
            builder.RegisterType<QueueService>().As<IQueueService>().SingleInstance();
            builder.RegisterType<FifthweekCloudStorageAccount>().As<ICloudStorageAccount>().SingleInstance();
        }
    }
}