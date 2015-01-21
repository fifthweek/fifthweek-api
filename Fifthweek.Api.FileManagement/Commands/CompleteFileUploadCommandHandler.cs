namespace Fifthweek.Api.FileManagement.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;
    using Fifthweek.WebJobs.Files.Shared;

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

            UserId userId;
            command.Requester.AssertAuthenticated(out userId);
            
            var file = await this.fileRepository.GetFileWaitingForUploadAsync(command.FileId);
            command.Requester.AssertAuthenticatedAs(file.UserId);

            var mimeType = this.mimeTypeMap.GetMimeType(file.FileExtension);

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(userId, command.FileId, file.Purpose);
            var blobLength = await this.blobService.GetBlobLengthAndSetContentTypeAsync(blobLocation.ContainerName, blobLocation.BlobName, mimeType);

            await this.fileRepository.SetFileUploadComplete(command.FileId, blobLength);

            var messageContent = new ProcessFileMessage(blobLocation.ContainerName, blobLocation.BlobName, file.Purpose, false);
            await this.queueService.AddMessageToQueueAsync(WebJobs.Files.Shared.Constants.FilesQueueName, messageContent);
        }
    }
}