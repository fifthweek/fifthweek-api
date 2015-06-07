namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.Services;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UnsubscribeFromChannelDbStatement : IUnsubscribeFromChannelDbStatement
    { 
        private static readonly string DeleteStatement = string.Format(
            @"DELETE FROM {0} WHERE {1}=@ChannelId AND {2}=@UserId",
            ChannelSubscription.Table,
            ChannelSubscription.Fields.ChannelId,
            ChannelSubscription.Fields.UserId);

        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task ExecuteAsync(UserId userId, ChannelId channelId)
        {
            userId.AssertNotNull("userId");
            channelId.AssertNotNull("channelId");

            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    await connection.ExecuteAsync(DeleteStatement, new { ChannelId = channelId.Value, UserId = userId.Value });
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.SubscriberChannels);

                transaction.Complete();
            }
        }
    }
}