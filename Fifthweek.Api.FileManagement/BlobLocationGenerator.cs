namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Shared;

    public class BlobLocationGenerator : IBlobLocationGenerator
    {
        public BlobLocation GetBlobLocation(UserId userId, FileId fileId, string filePurpose)
        {
            var purpose = FilePurposes.TryGetFilePurpose(filePurpose);
            if (purpose.IsPublic)
            {
                return new BlobLocation(Constants.PublicFileBlobContainerName, fileId.Value.EncodeGuid());
            }

            return new BlobLocation(userId.Value.ToString("N"), fileId.Value.ToString("N"));
        }
    }
}