namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class InitiateFileUploadRequestCommandHandler : ICommandHandler<InitiateFileUploadRequestCommand>
    {
        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;

        public InitiateFileUploadRequestCommandHandler(IBlobService blobService, IBlobNameCreator blobNameCreator)
        {
            this.blobService = blobService;
            this.blobNameCreator = blobNameCreator;
        }

        public Task HandleAsync(InitiateFileUploadRequestCommand command)
        {
            var username = string.Empty;

            this.blobService.CreateBlobContainerAsync(username);

            var uri = this.blobService.GetBlobSasUriForWritingAsync(username, this.blobNameCreator.CreateFileName());
            
            // Create blob.

            // Get user.

            ////var file = new File(
            ////    command.FileId.Value,
            ////    userId,
            ////    FileState.WaitingForClient,
            ////    blobReference,
            ////    DateTime.UtcNow,
            ////    null,
            ////    null,
            ////    fileName,
            ////    mimeType);
            
            return Task.FromResult(0);
        }
    }
}