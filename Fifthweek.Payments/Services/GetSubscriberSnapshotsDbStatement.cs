namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetSubscriberSnapshotsDbStatement : IGetSubscriberSnapshotsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0}
              WHERE {1} = @SubscriberId 
                  AND 
                  (
                      (
                          {2} >= @StartTimestampInclusive AND {2} < @EndTimestampExclusive
                      )
                      OR 
                      (
                          {2} = 
                          (
                              SELECT TOP 1 {2} 
                              FROM {0} 
                              WHERE {1}=@SubscriberId AND {2} < @StartTimestampInclusive ORDER BY {2} DESC
                          )
                      )
                  )",
            SubscriberSnapshot.Table,
            SubscriberSnapshot.Fields.SubscriberId,
            SubscriberSnapshot.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<Snapshots.SubscriberSnapshot>> ExecuteAsync(UserId subscriberId, DateTime startTimestampInclusive, DateTime endTimestampExclusive)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var databaseResult = await connection.QueryAsync<SubscriberSnapshot>(
                    Sql,
                    new
                    {
                        SubscriberId = subscriberId.Value,
                        StartTimestampInclusive = startTimestampInclusive,
                        EndTimestampExclusive = endTimestampExclusive
                    });

                return databaseResult
                    .Select(v => new Snapshots.SubscriberSnapshot(
                        DateTime.SpecifyKind(v.Timestamp, DateTimeKind.Utc), 
                        new UserId(v.SubscriberId), v.Email))
                    .ToList();
            }
        }
    }
}