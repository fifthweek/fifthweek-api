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

            this.fifthweekDbContext.Files.Add(file);
            return this.fifthweekDbContext.SaveChangesAsync();
        }

        public Task SetFileUploadComplete(FileId fileId, long blobSize)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                    @"
                    UPDATE dbo.Files 
                    SET State=@State, CompletionDate=@CompletionDate, BlobSizeBytes=@BlobSizeBytes
                    WHERE Id=@FileId",
                    new 
                    {
                        State = FileState.UploadComplete,
                        CompletionDate = DateTime.UtcNow,
                        BlobSizeBytes = blobSize,
                        FileId = fileId
                    });
        }

        public async Task AssertFileBelongsToUserAsync(UserId userId, FileId fileId)
        {
            var fileBelongsToUser = await this.CheckFileBelongsToUserAsync(userId, fileId);

            if (!fileBelongsToUser)
            {
                throw new SecurityException("The file doesn't belong to the user");
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