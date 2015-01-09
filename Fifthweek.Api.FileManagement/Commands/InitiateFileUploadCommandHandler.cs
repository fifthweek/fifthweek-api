namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>))]
    [AutoConstructor]
    public partial class InitiateFileUploadCommandHandler : ICommandHandler<InitiateFileUploadCommand>
    {
        private readonly IBlobService blobService;

        private readonly IFileRepository fileRepository;

        public async Task HandleAsync(InitiateFileUploadCommand command)
        {
            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            await this.blobService.CreateBlobContainerAsync(ContainerName);

            string fileNameWithoutExtension = null;
            string fileExtension = null;

            if (!string.IsNullOrWhiteSpace(command.FilePath))
            {
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(command.FilePath);
                fileExtension = System.IO.Path.GetExtension(command.FilePath);
            }

            await this.fileRepository.AddNewFileAsync(
                command.FileId,
                command.Requester,
                fileNameWithoutExtension ?? string.Empty,
                fileExtension ?? string.Empty,
                command.Purpose ?? string.Empty);
        }
    }
}