namespace Fifthweek.Api.Blogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateChannelSubscriptionDbStatement : IUpdateChannelSubscriptionDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;
        private readonly IRequestSnapshotService requestSnapshot;

        public async Task ExecuteAsync(
            UserId userId, 
            ChannelId channelId,
            ValidAcceptedChannelPrice acceptedPrice,
            DateTime now)
        {
            userId.AssertNotNull("userId");
            channelId.AssertNotNull("channelId");
            acceptedPrice.AssertNotNull("acceptedPrice");

            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                using (var connection = this.connectionFactory.CreateConnection())
                {
                    var channelSubscription = new ChannelSubscription(
                        channelId.Value,
                        null,
                        userId.Value,
                        null,
                        acceptedPrice.Value,
                        now,
                        now);

                    const ChannelSubscription.Fields UpdateFields
                        = ChannelSubscription.Fields.AcceptedPrice
                        | ChannelSubscription.Fields.PriceLastAcceptedDate;

                    await connection.UpsertAsync(
                        channelSubscription,
                        UpdateFields);
                }

                await this.requestSnapshot.ExecuteAsync(userId, SnapshotType.SubscriberChannels);

                transaction.Complete();
            }
        }
    }
}