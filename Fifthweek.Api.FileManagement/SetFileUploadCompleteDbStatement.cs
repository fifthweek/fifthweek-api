namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SetFileUploadCompleteDbStatement : ISetFileUploadCompleteDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(Shared.FileId fileId, long blobSize, DateTime timeStamp)
        {
            fileId.AssertNotNull("fileId");

            var newFile = new File
            {
                State = FileState.UploadComplete,
                UploadCompletedDate = timeStamp,
                BlobSizeBytes = blobSize,
                Id = fileId.Value,
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.UpdateAsync(
                    newFile,
                    File.Fields.State | File.Fields.UploadCompletedDate | File.Fields.BlobSizeBytes);
            }
        }
    }
}