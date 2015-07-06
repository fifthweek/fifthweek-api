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
            @"SELECT ccs.{0}, MIN(scs.{12}) AS FirstSubscribedDate
                FROM {1} ccs 
	            INNER JOIN {2} ccsi ON ccs.{3} = ccsi.{4}
	            INNER JOIN {8} scsi ON ccsi.{5} = scsi.{6}
	            INNER JOIN {7} scs ON scs.{9} = scsi.{10}
	            WHERE scs.{11} = @SubscriberId
                    AND ccs.{0} NOT IN (SELECT r.{14} FROM {13} r WHERE r.{15}='{16}')
	            GROUP BY ccs.{0}",
            CreatorChannelsSnapshot.Fields.CreatorId,
            CreatorChannelsSnapshot.Table,
            CreatorChannelsSnapshotItem.Table,
            CreatorChannelsSnapshot.Fields.Id,
            CreatorChannelsSnapshotItem.Fields.CreatorChannelsSnapshotId,
            CreatorChannelsSnapshotItem.Fields.ChannelId,
            SubscriberChannelsSnapshotItem.Fields.ChannelId,
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