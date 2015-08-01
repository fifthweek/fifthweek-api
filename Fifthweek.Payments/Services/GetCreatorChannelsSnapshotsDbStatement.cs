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
    public partial class GetCreatorChannelsSnapshotsDbStatement : IGetCreatorChannelsSnapshotsDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT * FROM {0} ccs LEFT OUTER JOIN {1} ccsi ON ccs.{2} = ccsi.{3}
              WHERE ccs.{4} = @CreatorId 
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
                              WHERE {4}=@CreatorId AND {5} < @StartTimestampInclusive ORDER BY {5} DESC
                          )
                      )
                  )",
            CreatorChannelsSnapshot.Table,
            CreatorChannelsSnapshotItem.Table,
            CreatorChannelsSnapshot.Fields.Id,
            CreatorChannelsSnapshotItem.Fields.CreatorChannelsSnapshotId,
            CreatorChannelsSnapshot.Fields.CreatorId,
            CreatorChannelsSnapshot.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<Snapshots.CreatorChannelsSnapshot>> ExecuteAsync(UserId creatorId, DateTime startTimestampInclusive, DateTime endTimestampExclusive)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(GetCreatorChannelsSnapshotsDbStatement)))
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var databaseResult = await connection.QueryAsync<DbResult>(
                    Sql,
                    new 
                    {
                        CreatorId = creatorId.Value,
                        StartTimestampInclusive = startTimestampInclusive,
                        EndTimestampExclusive = endTimestampExclusive
                    });

                var groupedResult = databaseResult.GroupBy(v => v.Id);

                var snapshots = new List<Snapshots.CreatorChannelsSnapshot>();
                foreach (var group in groupedResult)
                {
                    var firstItem = group.First();

                    var creatorChannels = (from item in @group 
                                           where item.ChannelId != null && item.Price != null 
                                           select new Snapshots.CreatorChannelsSnapshotItem(
                                               new ChannelId(item.ChannelId.Value), 
                                               item.Price.Value)).ToList();

                    snapshots.Add(new Snapshots.CreatorChannelsSnapshot(
                        DateTime.SpecifyKind(firstItem.Timestamp, DateTimeKind.Utc),
                        new UserId(firstItem.CreatorId),
                        creatorChannels));
                }

                return snapshots;
            }
        }

        private class DbResult
        {
            public Guid Id { get; set; }

            public DateTime Timestamp { get; set; }

            public Guid CreatorId { get; set; }

            public Guid? ChannelId { get; set; }

            public int? Price { get; set; }
        }
    }
}