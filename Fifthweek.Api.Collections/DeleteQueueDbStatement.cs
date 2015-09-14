namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DeleteQueueDbStatement : IDeleteQueueDbStatement
    {
        private static readonly string DeleteSql = string.Format(
            @"
            UPDATE {0} SET {1}=NULL WHERE {1}=@QueueId;
            DELETE FROM {2} WHERE {3} = @QueueId;",
            Post.Table,
            Post.Fields.QueueId,
            Queue.Table,
            Queue.Fields.Id);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(QueueId queueId)
        {
            queueId.AssertNotNull("queueId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(
                    DeleteSql, 
                    new
                    {
                        QueueId = queueId.Value
                    });
            }
        }
    }
}