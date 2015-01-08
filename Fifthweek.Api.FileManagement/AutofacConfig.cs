namespace Fifthweek.Api.FileManagement
{
    using Autofac;

    public static class AutofacConfig
    {
        public static void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobNameCreator>().As<IBlobNameCreator>().SingleInstance();
        }
    }
}