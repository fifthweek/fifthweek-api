namespace Fifthweek.Api.FileManagement
{
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    public class BlobLocationGenerator : IBlobLocationGenerator
    {
        public BlobLocation GetBlobLocation(UserId userId, FileId fileId, string filePurpose)
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

            userId.AssertNotNull("userId");
            return new BlobLocation(this.GetBlobContainerName(userId), fileId.Value.ToString("N"));
        }

        public string GetBlobContainerName(UserId userId)
        {
            return userId.Value.ToString("N");
        }
    }
}