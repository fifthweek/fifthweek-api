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
    public partial class GetSubscriberChannelsSnapshotsDbStatement : IGetSubscriberChannelsSnapshotsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0} ccs LEFT OUTER JOIN {1} ccsi ON ccs.{2} = ccsi.{3}
              WHERE ccs.{4} = @SubscriberId 
                  AND 
                  (
                      (
                          ccs.{5} >= @StartTimestampInclusive AND ccs.{5} < @EndTimestampExclusive
                      )
                      OR 
                      (
                          ccs.{5} = 
                          (
                              SELECT TOP 1 {5} 
                              FROM {0} 
                              WHERE {4}=@SubscriberId AND {5} < @StartTimestampInclusive ORDER BY {5} DESC
                          )
                      )
                  )",
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshotItem.Table,
            SubscriberChannelsSnapshot.Fields.Id,
            SubscriberChannelsSnapshotItem.Fields.SubscriberChannelsSnapshotId,
            SubscriberChannelsSnapshot.Fields.SubscriberId,
            SubscriberChannelsSnapshot.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<Snapshots.SubscriberChannelsSnapshot>> ExecuteAsync(UserId subscriberId, DateTime startTimestampInclusive, DateTime endTimestampExclusive)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var databaseResult = await connection.QueryAsync<DbResult>(
                    Sql,
                    new
                    {
                        SubscriberId = subscriberId.Value,
                        StartTimestampInclusive = startTimestampInclusive,
                        EndTimestampExclusive = endTimestampExclusive
                    });

                var groupedResult = databaseResult.GroupBy(v => v.Id);

                var snapshots = new List<Snapshots.SubscriberChannelsSnapshot>();
                foreach (var group in groupedResult)
                {
                    var firstItem = group.First();

                    var subscriberChannels = (from item in @group
                                              where item.ChannelId != null
                                                && item.AcceptedPriceInUsCentsPerWeek != null
                                                && item.SubscriptionStartDate != null
                                              select new Snapshots.SubscriberChannelsSnapshotItem(
                                                new ChannelId(item.ChannelId.Value),
                                                item.AcceptedPriceInUsCentsPerWeek.Value,
                                                DateTime.SpecifyKind(item.SubscriptionStartDate.Value, DateTimeKind.Utc))).ToList();

                    snapshots.Add(new Snapshots.SubscriberChannelsSnapshot(
                        DateTime.SpecifyKind(firstItem.Timestamp, DateTimeKind.Utc),
                        new UserId(firstItem.SubscriberId),
                        subscriberChannels));
                }

                return snapshots;
            }
        }

        private class DbResult
        {
            public Guid Id { get; set; }

            public DateTime Timestamp { get; set; }

            public Guid SubscriberId { get; set; }

            public Guid? ChannelId { get; set; }

            public int? AcceptedPriceInUsCentsPerWeek { get; set; }

            public DateTime? SubscriptionStartDate { get; set; }
        }
    }
}