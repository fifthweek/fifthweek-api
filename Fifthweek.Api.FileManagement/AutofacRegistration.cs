namespace Fifthweek.Api.FileManagement
{
    using Autofac;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    public class AutofacRegistration : IAutofacRegistration
    {
        public void Register(ContainerBuilder builder)
        {
            builder.RegisterType<BlobLocationGenerator>().As<IBlobLocationGenerator>().SingleInstance();
            builder.RegisterType<AddNewFileDbStatement>().As<IAddNewFileDbStatement>();
            builder.RegisterType<GetFileWaitingForUploadDbStatement>().As<IGetFileWaitingForUploadDbStatement>();
            builder.RegisterType<SetFileUploadCompleteDbStatement>().As<ISetFileUploadCompleteDbStatement>();
            builder.RegisterType<FileOwnership>().As<IFileOwnership>();
            builder.RegisterType<FileSecurity>().As<IFileSecurity>();
            builder.RegisterType<GetFileExtensionDbStatement>().As<IGetFileExtensionDbStatement>();
            builder.RegisterType<ScheduleGarbageCollectionStatement>().As<IScheduleGarbageCollectionStatement>();
        }
    }
}