namespace Fifthweek.Api.Azure
{
    using Autofac;

    using Fifthweek.Azure;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobService>().As<IBlobService>();
            builder.RegisterType<QueueService>().As<IQueueService>();
            builder.RegisterType<FifthweekCloudStorageAccount>().As<ICloudStorageAccount>();
            builder.RegisterType<AzureConfiguration>().As<IAzureConfiguration>();
        }
    }
}