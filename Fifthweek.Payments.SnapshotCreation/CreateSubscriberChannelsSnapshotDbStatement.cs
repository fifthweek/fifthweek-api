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
        private readonly IGuidCreator guidCreator;
        private readonly ISnapshotTimestampCreator timestampCreator;
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        private static readonly string Sql = string.Format(
            @"INSERT INTO {0} VALUES (@RecordId, @Timestamp, @SubscriberId)
              INSERT INTO {1} SELECT @RecordId, {2}, {3}, {4} FROM {5} WHERE {6}=@SubscriberId",
            SubscriberChannelsSnapshot.Table,
            SubscriberChannelsSnapshotItem.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek,
            ChannelSubscription.Fields.SubscriptionStartDate,
            ChannelSubscription.Table,
            ChannelSubscription.Fields.UserId);

        public async Task ExecuteAsync(UserId subscriberId)
        {
            subscriberId.AssertNotNull("subscriberId");
            var timestamp = this.timestampCreator.Create();
            var recordId = this.guidCreator.CreateSqlSequential();

            using (var connection = this.connectionFactory.CreateConnection())
            {
                await connection.ExecuteAsync(Sql, new { RecordId = recordId, Timestamp = timestamp, SubscriberId = subscriberId });
            }
        }
    }
}