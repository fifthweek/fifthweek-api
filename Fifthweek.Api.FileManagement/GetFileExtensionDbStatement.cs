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
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<string> ExecuteAsync(Shared.FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var extension = await connection.ExecuteScalarAsync<string>(
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
}