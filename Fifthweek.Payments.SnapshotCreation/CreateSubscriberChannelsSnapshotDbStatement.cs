namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class CreateSubscriberChannelsSnapshotDbStatement : ICreateSubscriberChannelsSnapshotDbStatement
    {
        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} ({18}, {19}, {20}) VALUES (@RecordId, @Timestamp, @SubscriberId)
              INSERT INTO {1} ({13}, {14}, {15}, {16}, {17}) 
                SELECT @RecordId, cs.{2}, b.{12}, cs.{3}, cs.{4} 
                FROM {5} cs 
                INNER JOIN {7} c ON cs.{2}=c.{8}
                INNER JOIN {9} b ON c.{10}=b.{11}
                WHERE cs.{6}=@SubscriberId",
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshotItem.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.AcceptedPrice,
            ChannelSubscription.Fields.SubscriptionStartDate,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.UserId,
            Channel.Table,
            Channel.Fields.Id,
            Blog.Table,
            Channel.Fields.BlogId,
            Blog.Fields.Id,
            Blog.Fields.CreatorId,
            SubscriberChannelsSnapshotItem.Fields.SubscriberChannelsSnapshotId,
            SubscriberChannelsSnapshotItem.Fields.ChannelId,
            SubscriberChannelsSnapshotItem.Fields.CreatorId,
            SubscriberChannelsSnapshotItem.Fields.AcceptedPrice,
            SubscriberChannelsSnapshotItem.Fields.SubscriptionStartDate,
            SubscriberChannelsSnapshot.Fields.Id,
            SubscriberChannelsSnapshot.Fields.Timestamp,
            SubscriberChannelsSnapshot.Fields.SubscriberId);

        private readonly IGuidCreator guidCreator;
        private readonly ISnapshotTimestampCreator timestampCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(UserId subscriberId)
        {
            subscriberId.AssertNotNull("subscriberId");
            var timestamp = this.timestampCreator.Create();
            var recordId = this.guidCreator.CreateSqlSequential();

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { RecordId = recordId, Timestamp = timestamp, SubscriberId = subscriberId.Value });
            }
        }
    }
}