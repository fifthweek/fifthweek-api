namespace Fifthweek.Api.Azure
{
    using Autofac;

    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobService>().As<IBlobService>().SingleInstance();
            builder.RegisterType<FifthweekCloudStorageAccount>().As<ICloudStorageAccount>().SingleInstance();
        }
    }
}