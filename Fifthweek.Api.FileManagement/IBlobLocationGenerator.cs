namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IBlobLocationGenerator
    {
        BlobLocation GetBlobLocation(UserId userId, FileId fileId, string filePurpose);

        string GetBlobContainerName(UserId userId);
    }
}