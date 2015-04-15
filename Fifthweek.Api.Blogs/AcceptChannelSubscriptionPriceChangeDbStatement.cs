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
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class AcceptChannelSubscriptionPriceChangeDbStatement : IAcceptChannelSubscriptionPriceChangeDbStatement
    {
        private readonly IFifthweekDbConnectionFactory connectionFactory;

        public async Task ExecuteAsync(
            UserId userId, 
            ChannelId channelId,
            ValidAcceptedChannelPriceInUsCentsPerWeek acceptedPrice,
            DateTime now)
        {
            userId.AssertNotNull("userId");
            channelId.AssertNotNull("channelId");
            acceptedPrice.AssertNotNull("acceptedPrice");

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
                    = ChannelSubscription.Fields.AcceptedPriceInUsCentsPerWeek
                    | ChannelSubscription.Fields.PriceLastAcceptedDate;

                await connection.UpdateAsync(
                    channelSubscription,
                    UpdateFields);
            }
        }
    }
}