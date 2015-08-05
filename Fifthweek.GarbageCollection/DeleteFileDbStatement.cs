namespace Fifthweek.GarbageCollection
{
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteFileDbStatement : IDeleteFileDbStatement
    {
        private static readonly string Sql = string.Format(
            @"DELETE FROM {0} WHERE {1}=@FileId",
            File.Table,
            File.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(FileId fileId)
        {
            fileId.AssertNotNull("fileId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    Sql,
                    new 
                    { 
                        FileId = fileId.Value,
                    });
            }
        }
    }
}