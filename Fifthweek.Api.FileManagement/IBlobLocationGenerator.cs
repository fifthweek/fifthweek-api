namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IBlobLocationGenerator
    {
        BlobLocation GetBlobLocation(UserId userId, Shared.FileId fileId, string filePurpose);

        string GetBlobContainerName(UserId userId);
    }
}