namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class QueueOwnership : IQueueOwnership
    {
        private static readonly string Sql = string.Format(
            @"IF EXISTS(SELECT *
                        FROM        {0} queue
                        INNER JOIN  {1} blog
                            ON      queue.{2} = blog.{3}
                        WHERE       queue.{4} = @QueueId
                        AND         blog.{5} = @CreatorId)
                SELECT 1 AS FOUND
            ELSE
                SELECT 0 AS FOUND",
            Queue.Table,
            Blog.Table,
            Queue.Fields.BlogId,
            Blog.Fields.Id,
            Queue.Fields.Id,
            Blog.Fields.CreatorId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<bool> IsOwnerAsync(UserId userId, Shared.QueueId queueId)
        {
            userId.AssertNotNull("userId");
            queueId.AssertNotNull("queueId");

            using (var connection = this.connectionFactory.CreateConnection())
            {
                return await connection.ExecuteScalarAsync<bool>(
                    Sql,
                    new
                    {
                        QueueId = queueId.Value,
                        CreatorId = userId.Value
                    });
            }
        }
    }
}