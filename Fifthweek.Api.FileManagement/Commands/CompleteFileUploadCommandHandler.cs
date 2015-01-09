namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;

    [Decorator(typeof(RetryOnSqlDeadlockOrTimeoutCommandHandlerDecorator<>))]
    [AutoConstructor]
    public partial class CompleteFileUploadCommandHandler : ICommandHandler<CompleteFileUploadCommand>
    {
        private readonly IFileRepository fileRepository;

        private readonly IBlobService blobService;

        private readonly IBlobNameCreator blobNameCreator;
        
        public async Task HandleAsync(CompleteFileUploadCommand command)
        {
            await this.fileRepository.AssertFileBelongsToUserAsync(command.Requester, command.FileId);

            const string ContainerName = FileManagement.Constants.FileBlobContainerName;
            var blobName = this.blobNameCreator.CreateFileName(command.FileId);
            var blobProperties = await this.blobService.GetBlobPropertiesAsync(ContainerName, blobName);

            await this.fileRepository.SetFileUploadComplete(command.FileId, blobProperties.Length);
        }
    }
}