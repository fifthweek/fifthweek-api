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
    public partial class GetCreatorFreeAccessUsersSnapshotsDbStatement : IGetCreatorFreeAccessUsersSnapshotsDbStatement
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
            CreatorFreeAccessUsersSnapshot.Table,
            CreatorFreeAccessUsersSnapshotItem.Table,
            CreatorFreeAccessUsersSnapshot.Fields.Id,
            CreatorFreeAccessUsersSnapshotItem.Fields.CreatorFreeAccessUsersSnapshotId,
            CreatorFreeAccessUsersSnapshot.Fields.CreatorId,
            CreatorFreeAccessUsersSnapshot.Fields.Timestamp);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<Snapshots.CreatorFreeAccessUsersSnapshot>> ExecuteAsync(UserId creatorId, DateTime startTimestampInclusive, DateTime endTimestampExclusive)
        {
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

                var snapshots = new List<Snapshots.CreatorFreeAccessUsersSnapshot>();
                foreach (var group in groupedResult)
                {
                    var firstItem = group.First();

                    var creatorFreeAccessUsers 
                        = (from item in @group
                           where !string.IsNullOrWhiteSpace(item.Email)
                           select item.Email).ToList();

                    snapshots.Add(new Snapshots.CreatorFreeAccessUsersSnapshot(
                        DateTime.SpecifyKind(firstItem.Timestamp, DateTimeKind.Utc),
                        new UserId(firstItem.CreatorId),
                        creatorFreeAccessUsers));
                }

                return snapshots;
            }
        }

        private class DbResult
        {
            public Guid Id { get; set; }

            public DateTime Timestamp { get; set; }

            public Guid CreatorId { get; set; }

            public string Email { get; set; }
        }
    }
}