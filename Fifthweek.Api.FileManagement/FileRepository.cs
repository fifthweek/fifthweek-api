namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
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
            fileId.AssertNotNull("fileId");
            userId.AssertNotNull("userId");
            fileNameWithoutExtension.AssertNotNull("fileNameWithoutExtension");
            fileExtension.AssertNotNull("fileExtension");
            purpose.AssertNotNull("purpose");

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
            fileId.AssertNotNull("fileId");

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

        public async Task<FileWaitingForUpload> GetFileWaitingForUploadAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            var items = await this.fifthweekDbContext.Database.Connection.QueryAsync<File>(
                string.Format(
                    @"SELECT {0}, {1}, {2}, {3}, {4} FROM Files WHERE Id=@FileId",
                    File.Fields.Id,
                    File.Fields.UserId,
                    File.Fields.FileNameWithoutExtension,
                    File.Fields.FileExtension,
                    File.Fields.Purpose),
                new { FileId = fileId.Value });

            var result = items.SingleOrDefault();

            if (result == null)
            {
                throw new InvalidOperationException("The File " + fileId + " couldn't be found.");
            }

            return new FileWaitingForUpload(new FileId(result.Id), new UserId(result.UserId), result.FileNameWithoutExtension, result.FileExtension, result.Purpose);
        }
    }
}