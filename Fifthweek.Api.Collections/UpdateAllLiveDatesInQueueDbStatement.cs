namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateAllLiveDatesInQueueDbStatement : IUpdateAllLiveDatesInQueueDbStatement
    {
        private const string SqlPreInserts =
            @"
            CREATE TABLE #NewLiveDates(LiveDate datetime, [Index] int);";

        private static readonly string SqlPostInserts = string.Format(
            @"
            CREATE TABLE #PostsToUpdate(Id uniqueidentifier, [Index] int);
            INSERT INTO #PostsToUpdate 
            SELECT      {1}, ROW_NUMBER() OVER (ORDER BY LiveDate ASC)
            FROM        {0}
            WHERE       {4} = @CollectionId
            AND         {2} > @Now
            AND         {3} = 1;

            DECLARE @QueuedPostCount int = (SELECT COUNT(*) FROM #PostsToUpdate);
            DECLARE @NewLiveDateCount int = (SELECT COUNT(*) FROM #NewLiveDates);

            IF @QueuedPostCount <= @NewLiveDateCount
            BEGIN
                -- Take LiveDate from new list of dates.
                CREATE TABLE #UpdatedPosts(Id uniqueidentifier, LiveDate datetime);
                INSERT INTO #UpdatedPosts 
                SELECT      post.Id, newLiveDate.LiveDate
                FROM        #PostsToUpdate post
                INNER JOIN  #NewLiveDates newLiveDate
                  ON        post.[Index] = newLiveDate.[Index];

                -- Update Posts.LiveDate so they match UpdatedPosts.
                UPDATE      {0} 
                SET         {2} = updatedPost.LiveDate
                FROM        {0} post
                INNER JOIN  #UpdatedPosts updatedPost
                  ON        post.{1} = updatedPost.Id;

                SELECT -1 AS QueueOverflow;
            END
            ELSE
            BEGIN
                SELECT @QueuedPostCount AS QueueOverflow;
            END",
            Post.Table,
            Post.Fields.Id,
            Post.Fields.LiveDate,
            Post.Fields.ScheduledByQueue,
            Post.Fields.CollectionId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(CollectionId collectionId, IReadOnlyList<DateTime> ascendingLiveDates, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            ascendingLiveDates.AssertNotNull("ascendingLiveDates");
            ascendingLiveDates.AssertNonEmpty("ascendingLiveDates");
            now.AssertUtc("now");

            if (ascendingLiveDates.Any(_ => _.Kind != DateTimeKind.Utc))
            {
                throw new ArgumentException("All dates must be UTC", "ascendingLiveDates");
            }

            if (ascendingLiveDates.First() <= now)
            {
                throw new ArgumentException("All dates must be in future", "ascendingLiveDates");
            }

            ascendingLiveDates.Aggregate((previous, current) =>
            {
                if (previous >= current)
                {
                    throw new ArgumentException("Must be in ascending order with no duplicates", "ascendingLiveDates");
                }

                return current;
            });

            var parameters = new DynamicParameters(new
            {
                CollectionId = collectionId.Value,
                Now = now
            });

            var sql = new StringBuilder();
            sql.AppendLine(SqlPreInserts);
            for (var i = 0; i < ascendingLiveDates.Count; i++)
            {
                var rowNumber = i + 1; // Need to match the output of ROW_NUMBER, which is 1-based.
                sql.Append("INSERT INTO #NewLiveDates VALUES (@LiveDate");
                sql.Append(rowNumber);
                sql.Append(", ");
                sql.Append(rowNumber);
                sql.AppendLine(");");

                var liveDate = ascendingLiveDates[i];
                parameters.Add("LiveDate" + rowNumber, liveDate);
            }

            sql.AppendLine(SqlPostInserts);

            using (var connection = this.connectionFactory.CreateConnection())
            {
                var queueOverflow = await connection.ExecuteScalarAsync<int>(sql.ToString(), parameters);

                if (queueOverflow != -1)
                {
                    throw new ArgumentException(
                        string.Format(
                            "Insufficient quantity of live dates provided. Given {0}. Required at least {1}.",
                            ascendingLiveDates.Count,
                            queueOverflow),
                        "ascendingLiveDates");
                }
            }
        }
    }
}