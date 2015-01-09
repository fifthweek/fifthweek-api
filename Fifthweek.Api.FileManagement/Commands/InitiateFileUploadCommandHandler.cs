namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    public partial class InitiateFileUploadCommandHandler : ICommandHandler<InitiateFileUploadCommand>
    {
        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;

        public async Task HandleAsync(InitiateFileUploadCommand command)
        {
            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            var blobName = this.blobNameCreator.CreateFileName(command.FileId);

            await this.blobService.CreateBlobContainerAsync(ContainerName);

            var blobUri = await this.blobService.GetBlobSasUriForWritingAsync(ContainerName, blobName);

            var file = new File(
                command.FileId.Value,
                command.Requester.Value,
                FileState.WaitingForClient,
                blobUri,
                DateTime.UtcNow,
                null,
                null,
                fileName,
                mimeType);
         
            // Add to database.
        }
    }
}