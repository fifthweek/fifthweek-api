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

        public async Task AssertFileBelongsToUserAsync(UserId userId, FileId fileId)
        {
            var fileBelongsToUser = await this.CheckFileBelongsToUserAsync(userId, fileId);

            if (!fileBelongsToUser)
            {
                throw new ForbiddenException("The user " + userId.Value + " does not have permission to access file " + fileId.Value);
            }
        }

        private Task<bool> CheckFileBelongsToUserAsync(UserId userId, FileId fileId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (fileId == null)
            {
                throw new ArgumentNullException("fileId");
            }

            return this.fifthweekDbContext.Database.Connection.ExecuteScalarAsync<bool>(
                @"IF EXISTS(SELECT *
                            FROM   Files
                            WHERE  Id = @FileId
                            AND    UserId = @UserId)
                    SELECT 1 AS FOUND
                ELSE
                    SELECT 0 AS FOUND", 
                new
                {
                    FileId = fileId.Value,
                    UserId = userId.Value
                });
        }
    }
}