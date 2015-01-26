namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class SetFileUploadCompleteDbStatement : ISetFileUploadCompleteDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task ExecuteAsync(FileId fileId, long blobSize, DateTime timeStamp)
        {
            fileId.AssertNotNull("fileId");

            var newFile = new File
            {
                State = FileState.UploadComplete,
                UploadCompletedDate = timeStamp,
                BlobSizeBytes = blobSize,
                Id = fileId.Value,
            };

            return this.fifthweekDbContext.Database.Connection.UpdateAsync(
                newFile,
                File.Fields.State | File.Fields.UploadCompletedDate | File.Fields.BlobSizeBytes);
        }
    }
}