namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class FileInformationAggregator : IFileInformationAggregator
    {
        ////private readonly IBlobService blobService;
        private readonly IBlobLocationGenerator blobLocationGenerator;

        public async Task<FileInformation> GetFileInformationAsync(ChannelId channelId, FileId fileId, string filePurpose)
        {
            fileId.AssertNotNull("fileId");
            filePurpose.AssertNotNull("filePurpose");

            var blobLocation = this.blobLocationGenerator.GetBlobLocation(
                channelId,
                fileId,
                filePurpose);

            ////var blobInformation = await this.blobService.GetBlobInformationAsync(blobLocation.ContainerName, blobLocation.BlobName);

            return new FileInformation(
                fileId,
                blobLocation.ContainerName);
        }
    }
}