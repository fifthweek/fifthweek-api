namespace Fifthweek.Api.FileManagement
{
    using Autofac;

    using Fifthweek.Api.Core;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobLocationGenerator>().As<IBlobLocationGenerator>().SingleInstance();
            builder.RegisterType<FileRepository>().As<IFileRepository>();
            builder.RegisterType<FileSecurity>().As<IFileSecurity>();
        }
    }
}