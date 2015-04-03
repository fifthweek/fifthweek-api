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
                @"SELECT ISNULL(
                    MAX({2}), 
                   (SELECT  MAX(DefaultLowerBound)
                    FROM   (VALUES (@Now), ((SELECT TOP 1 {6} FROM {4} WHERE {5} = @CollectionId))) AS DefaultLowerBounds(DefaultLowerBound))
                )
                FROM    {0}
                WHERE   {1} = @CollectionId
                AND     {2} > @Now
                AND     {3} = 1",
                Post.Table,
                Post.Fields.CollectionId,
                Post.Fields.LiveDate,
                Post.Fields.ScheduledByQueue,
                Collection.Table,
                Collection.Fields.Id,
                Collection.Fields.QueueExclusiveLowerBound);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<DateTime> ExecuteAsync(Shared.CollectionId collectionId, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            now.AssertUtc("now");

            var parameters = new
            {
                CollectionId = collectionId.Value,
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