namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>))]
    [AutoConstructor]
    public partial class CompleteFileUploadCommandHandler : ICommandHandler<CompleteFileUploadCommand>
    {
        private readonly IFileRepository fileRepository;

        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;
        
        public async Task HandleAsync(CompleteFileUploadCommand command)
        {
            command.AssertNotNull("command");
            command.AuthenticatedUserId.AssertAuthenticated();
            await this.fileRepository.AssertFileBelongsToUserAsync(command.AuthenticatedUserId, command.FileId);

            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            var blobName = this.blobNameCreator.CreateFileName(command.FileId);
            var blobProperties = await this.blobService.GetBlobPropertiesAsync(ContainerName, blobName);

            await this.fileRepository.SetFileUploadComplete(command.FileId, blobProperties.Length);
        }
    }
}