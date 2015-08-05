namespace Fifthweek.Api.FileManagement.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [AutoConstructor]
    public partial class InitiateFileUploadCommandHandler : ICommandHandler<InitiateFileUploadCommand>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IChannelSecurity channelSecurity;
        private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;
        private readonly IAddNewFileDbStatement addNewFile;

        public async Task HandleAsync(InitiateFileUploadCommand command)
        {
            command.AssertNotNull("command");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(command.Requester);
            if (command.ChannelId != null)
            {
                await this.channelSecurity.AssertWriteAllowedAsync(authenticatedUserId, command.ChannelId);
            }

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(command.ChannelId, command.FileId, command.Purpose);

            if (blobLocation.ContainerName != Constants.PublicFileBlobContainerName)
            {
                await this.blobService.CreateBlobContainerAsync(blobLocation.ContainerName);
            }

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

            await this.addNewFile.ExecuteAsync(
                command.FileId,
                authenticatedUserId,
                command.ChannelId,
                fileNameWithoutExtension ?? string.Empty,
                fileExtension ?? string.Empty,
                command.Purpose ?? string.Empty,
                DateTime.UtcNow);
        }
    }
}