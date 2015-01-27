namespace Fifthweek.Api.FileManagement
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetFileExtensionDbStatement : IGetFileExtensionDbStatement
    {
        private readonly IFifthweekDbContext databaseContext;

        public async Task<string> ExecuteAsync(Shared.FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            var extension = await this.databaseContext.Database.Connection.ExecuteScalarAsync<string>(
                @"SELECT FileExtension
                  FROM   Files
                  WHERE  Id = @FileId",
                new
                {
                    FileId = fileId.Value
                });

            if (extension == null)
            {
                throw new Exception(string.Format("File not found. {0}", fileId));
            }

            return extension;
        }
    }
}