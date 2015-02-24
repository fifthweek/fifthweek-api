namespace Fifthweek.Api.FileManagement.Shared
{
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IBlobLocationGenerator
    {
        BlobLocation GetBlobLocation(UserId userId, FileId fileId, string filePurpose);

        string GetBlobContainerName(UserId userId);
    }
}