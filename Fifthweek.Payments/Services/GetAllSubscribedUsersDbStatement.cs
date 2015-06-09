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
    public partial class GetAllSubscribedUsersDbStatement : IGetAllSubscribedUsersDbStatement
    {
        private static readonly string Sql = string.Format(
            @"SELECT DISTINCT scs.{0} 
              FROM {1} scs INNER JOIN {2} scsi ON scs.{3} = scsi.{4} 
              WHERE scsi.{5} IN @ChannelIds",
            SubscriberChannelsSnapshot.Fields.SubscriberId,
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshotItem.Table,
            SubscriberChannelsSnapshot.Fields.Id,
            SubscriberChannelsSnapshotItem.Fields.SubscriberChannelsSnapshotId,
            SubscriberChannelsSnapshotItem.Fields.ChannelId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task<IReadOnlyList<UserId>> ExecuteAsync(IReadOnlyList<ChannelId> channelIds)
        {
            using (var connection = this.connectionFactory.CreateConnection())
            {
                var result = await connection.QueryAsync<Guid>(
                    Sql,
                    new
                    {
                        ChannelIds = channelIds.Select(v => v.Value).ToList()
                    });

                return result.Select(v => new UserId(v)).ToList();
            }
        }
    }
}