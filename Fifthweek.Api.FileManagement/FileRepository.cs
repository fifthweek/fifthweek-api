namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Security;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;

    [AutoConstructor]
    public partial class FileRepository : IFileRepository
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task AddNewFileAsync(
            FileId fileId, 
            UserId userId,
            string fileNameWithoutExtension,
            string fileExtension,
            string purpose)
        {
            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (fileNameWithoutExtension == null)
            {
                throw new ArgumentNullException("fileNameWithoutExtension");
            }

            if (fileExtension == null)
            {
                throw new ArgumentNullException("fileExtension");
            }

            if (purpose == null)
            {
                throw new ArgumentNullException("purpose");
            }

            var file = new File(
                fileId.Value,
                null,
                userId.Value,
                FileState.WaitingForUpload,
                DateTime.UtcNow,
                null,
                null,
                null,
                fileNameWithoutExtension,
                fileExtension,
                0L,
                purpose);

            return this.fifthweekDbContext.Database.Connection.InsertAsync(file, true);
        }

        public Task SetFileUploadComplete(FileId fileId, long blobSize)
        {
            var newFile = new File
            {
                State = FileState.UploadComplete,
                UploadCompletedDate = DateTime.UtcNow,
                BlobSizeBytes = blobSize,
                Id = fileId.Value,
            };

            return this.fifthweekDbContext.Database.Connection.UpdateAsync(
                newFile,
                File.Fields.State | File.Fields.UploadCompletedDate | File.Fields.BlobSizeBytes);
        }
    }
}