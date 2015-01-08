namespace Fifthweek.Api.FileManagement
{
    using Autofac;

    using Fifthweek.Api.Core;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobNameCreator>().As<IBlobNameCreator>().SingleInstance();
        }
    }
}