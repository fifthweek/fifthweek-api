namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class InitiateFileUploadRequestCommandHandler : ICommandHandler<InitiateFileUploadRequestCommand>
    {
        public Task HandleAsync(InitiateFileUploadRequestCommand command)
        {
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