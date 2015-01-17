namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>))]
    [AutoConstructor]
    public partial class CompleteFileUploadCommandHandler : ICommandHandler<CompleteFileUploadCommand>
    {
        private readonly IFileRepository fileRepository;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly IBlobService blobService;
        private readonly IQueueService queueService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        
        public async Task HandleAsync(CompleteFileUploadCommand command)
        {
            command.AssertNotNull("command");
            command.AuthenticatedUserId.AssertAuthenticated();
            
            var file = await this.fileRepository.GetFileWaitingForUploadAsync(command.FileId);
            command.AuthenticatedUserId.AssertAuthorizedFor(file.UserId);

            var mimeType = this.mimeTypeMap.GetMimeType(file.FileExtension);
            
            var blobLocation = this.blobLocationGenerator.GetBlobLocation(command.AuthenticatedUserId, command.FileId, file.Purpose);
            var blobLength = await this.blobService.GetBlobLengthAndSetContentTypeAsync(blobLocation.ContainerName, blobLocation.BlobName, mimeType);

            await this.fileRepository.SetFileUploadComplete(command.FileId, blobLength);

            await this.queueService.PostFileUploadCompletedMessageToQueueAsync(blobLocation.ContainerName, blobLocation.BlobName, file.Purpose);
        }
    }
}