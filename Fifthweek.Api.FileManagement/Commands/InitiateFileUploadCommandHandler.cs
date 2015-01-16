namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>))]
    [AutoConstructor]
    public partial class InitiateFileUploadCommandHandler : ICommandHandler<InitiateFileUploadCommand>
    {
        private readonly IBlobService blobService;

        private readonly IFileRepository fileRepository;

        public async Task HandleAsync(InitiateFileUploadCommand command)
        {
            command.AssertNotNull("command");
            command.AuthenticatedUserId.AssertAuthenticated();

            const string ContainerName = FileManagement.Constants.PublicFileBlobContainerName;
            await this.blobService.CreateBlobContainerAsync(ContainerName);

            string fileNameWithoutExtension = null;
            string fileExtension = null;

            if (!string.IsNullOrWhiteSpace(command.FilePath))
            {
                fileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(command.FilePath);
                fileExtension = System.IO.Path.GetExtension(command.FilePath);

                if (!string.IsNullOrEmpty(fileExtension) && fileExtension[0] == '.')
                {
                    fileExtension = fileExtension.Substring(1);
                }

                // If it's a hidden file in linux, windows treats it as a file with no name.
                if (string.IsNullOrEmpty(fileNameWithoutExtension) && !string.IsNullOrEmpty(fileExtension))
                {
                    fileNameWithoutExtension = fileExtension;
                    fileExtension = string.Empty;
                }
            }

            await this.fileRepository.AddNewFileAsync(
                command.FileId,
                command.AuthenticatedUserId,
                fileNameWithoutExtension ?? string.Empty,
                fileExtension ?? string.Empty,
                command.Purpose ?? string.Empty);
        }
    }
}