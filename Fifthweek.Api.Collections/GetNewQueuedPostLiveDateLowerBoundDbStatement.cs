namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    /// <summary>
    /// Gets the exclusive lower bound for a hypothetical new post's live date when queued in the given collection.
    /// </summary>
    [AutoConstructor]
    public partial class GetNewQueuedPostLiveDateLowerBoundDbStatement : IGetNewQueuedPostLiveDateLowerBoundDbStatement
    {
        private static readonly string Sql = string.Format(
                @"SELECT ISNULL(MAX({2}), @Now)
                FROM    {0}
                WHERE   {1} = @QueueId
                AND     {2} > @Now",
                Post.Table,
                Post.Fields.QueueId,
                Post.Fields.LiveDate);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<DateTime> ExecuteAsync(Shared.QueueId queueId, DateTime now)
        {
            queueId.AssertNotNull("queueId");
            now.AssertUtc("now");

            var parameters = new
            {
                QueueId = queueId.Value,
                Now = now
            };

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync<DateTime>(Sql, parameters);

                return DateTime.SpecifyKind(result, DateTimeKind.Utc);
            }
        }
    }
}