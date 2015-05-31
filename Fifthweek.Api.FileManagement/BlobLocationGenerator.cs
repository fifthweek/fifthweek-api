namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class BlobLocationGenerator : IBlobLocationGenerator
    {
        public BlobLocation GetBlobLocation(ChannelId channelId, FileId fileId, string filePurpose)
        {
            fileId.AssertNotNull("fileId");
            filePurpose.AssertNotNull("filePurpose");

            var purpose = FilePurposes.TryGetFilePurpose(filePurpose);

            if (purpose == null)
            {
                throw new BadRequestException("Unknown file purpose.");
            }

            if (purpose.IsPublic)
            {
                return new BlobLocation(Constants.PublicFileBlobContainerName, fileId.Value.EncodeGuid());
            }

            channelId.AssertNotNull("channelId");
            return new BlobLocation(this.GetBlobContainerName(channelId), fileId.Value.EncodeGuid());
        }

        public string GetBlobContainerName(ChannelId channelId)
        {
            // https://msdn.microsoft.com/en-us/library/azure/dd135715.aspx
            return channelId.Value.ToString("N");
        }
    }
}