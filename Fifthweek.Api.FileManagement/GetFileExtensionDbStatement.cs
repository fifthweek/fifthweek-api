namespace Fifthweek.Api.FileManagement
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetFileExtensionDbStatement : IGetFileExtensionDbStatement
    {
        private readonly IFifthweekDbContext databaseContext;

        public Task<string> ExecuteAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<string>(
                @"SELECT FileExtension
                  FROM   Files
                  WHERE  Id = @FileId",
                new
                {
                    FileId = fileId.Value
                });
        }
    }
}