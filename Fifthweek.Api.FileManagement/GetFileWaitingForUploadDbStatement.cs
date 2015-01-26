namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetFileWaitingForUploadDbStatement : IGetFileWaitingForUploadDbStatement
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public async Task<FileWaitingForUpload> ExecuteAsync(FileId fileId)
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