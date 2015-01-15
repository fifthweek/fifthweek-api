namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Identity.Membership;

    public interface IBlobLocationGenerator
    {
        BlobLocation GetBlobLocation(UserId userId, FileId fileId, string filePurpose);
    }
}