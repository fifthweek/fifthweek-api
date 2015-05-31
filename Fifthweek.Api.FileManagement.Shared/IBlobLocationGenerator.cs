namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IBlobLocationGenerator
    {
        BlobLocation GetBlobLocation(ChannelId channelId, FileId fileId, string filePurpose);

        string GetBlobContainerName(ChannelId channelId);
    }
}