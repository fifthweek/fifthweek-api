namespace Fifthweek.Payments.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetCreatorsAndFirstSubscribedDatesDbStatement : IGetCreatorsAndFirstSubscribedDatesDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT scsi.{0}, MIN(scs.{6}) AS FirstSubscribedDate
                FROM {1} scs
	            INNER JOIN {2} scsi ON scs.{3} = scsi.{4}
	            WHERE scs.{5} = @SubscriberId
                    AND scsi.{0} NOT IN (SELECT r.{8} FROM {7} r WHERE r.{9}='{10}')
	            GROUP BY scsi.{0}",
            SubscriberChannelsSnapshotItem.Fields.CreatorId,
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshotItem.Table,
            SubscriberChannelsSnapshot.Fields.Id,
            SubscriberChannelsSnapshotItem.Fields.SubscriberChannelsSnapshotId,
            SubscriberChannelsSnapshot.Fields.SubscriberId,
            SubscriberChannelsSnapshot.Fields.Timestamp,
            FifthweekUserRole.Table,
            FifthweekUserRole.Fields.UserId,
            FifthweekUserRole.Fields.RoleId,
            FifthweekRole.TestUserId.ToString());

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<CreatorIdAndFirstSubscribedDate>> ExecuteAsync(UserId subscriberId)
        {
            using (PaymentsPerformanceLogger.Instance.Log(typeof(GetCreatorsAndFirstSubscribedDatesDbStatement)))
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<DatabaseResult>(
                    Sql,
                    new
                        {
                            SubscriberId = subscriberId.Value
                        });

                return result.Select(v => new CreatorIdAndFirstSubscribedDate(new UserId(v.CreatorId), DateTime.SpecifyKind(v.FirstSubscribedDate, DateTimeKind.Utc))).ToList();
            }
        }

        private class DatabaseResult
        {
            public Guid CreatorId { get; set; }

            public DateTime FirstSubscribedDate { get; set; }
        }
    }
}