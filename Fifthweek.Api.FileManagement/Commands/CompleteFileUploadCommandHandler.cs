namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CompleteFileUploadCommandHandler : ICommandHandler<CompleteFileUploadCommand>
    {
        private readonly IGetFileWaitingForUploadDbStatement getFileWaitingForUpload;
        private readonly ISetFileUploadCompleteDbStatement setFileUploadComplete;
        private readonly IMimeTypeMap mimeTypeMap;
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IFileProcessor fileProcessor;
        
        public async Task HandleAsync(CompleteFileUploadCommand command)
        {
            command.AssertNotNull("command");

            UserId userId = await this.requesterSecurity.AuthenticateAsync(command.Requester);

            var file = await this.getFileWaitingForUpload.ExecuteAsync(command.FileId);
            await this.requesterSecurity.AuthenticateAsAsync(command.Requester, file.UserId);

            var mimeType = this.mimeTypeMap.GetMimeType(file.FileExtension);

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(userId, command.FileId, file.Purpose);

            var timeSpan = this.GetExpiryTimeSpan(blobLocation);

            var blobLength = await this.blobService.GetBlobLengthAndSetPropertiesAsync(
                blobLocation.ContainerName, blobLocation.BlobName, mimeType, timeSpan);

            await this.setFileUploadComplete.ExecuteAsync(command.FileId, blobLength, DateTime.UtcNow);

            await this.fileProcessor.ProcessFileAsync(blobLocation.ContainerName, blobLocation.BlobName, file.Purpose);
        }

        private TimeSpan GetExpiryTimeSpan(BlobLocation blobLocation)
        {
            // We set the cache to the maximum time a SAS URL needs to be valid.
            var baseTimeSpan
                = this.IsPublic(blobLocation.ContainerName)
                ? FileManagement.Constants.PublicReadSignatureTimeSpan
                : FileManagement.Constants.PrivateReadSignatureTimeSpan;

            return baseTimeSpan + FileManagement.Constants.ReadSignatureMinimumExpiryTime;
        }

        private bool IsPublic(string containerName)
        {
            return containerName == FileManagement.Constants.PublicFileBlobContainerName;
        }
    }
}