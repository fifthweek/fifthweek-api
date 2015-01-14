namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IUserManager userManager;
        private readonly IFifthweekDbContext databaseContext;

        public Task<bool> IsCreationAllowedAsync(UserId requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            return this.userManager.IsInRoleAsync(requester.Value, FifthweekRole.Creator);
        }

        public Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<bool>(
                @"IF EXISTS(SELECT *
                            FROM   Subscriptions
                            WHERE  Id = @SubscriptionId
                            AND    CreatorId = @CreatorId)
                    SELECT 1 AS FOUND
                ELSE
                    SELECT 0 AS FOUND", 
                new
                {
                    SubscriptionId = subscriptionId.Value,
                    CreatorId = requester.Value
                });
        }

        public Task<bool> IsUpdateAllowedAsync(UserId requester, ChannelId channelId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            return this.databaseContext.Database.Connection.ExecuteScalarAsync<bool>(
                @"IF EXISTS(SELECT *
                            FROM        Channels channel
                            INNER JOIN  Subscriptions subscription
                            ON          channel.SubscriptionId = subscription.Id
                            WHERE       channel.Id = @ChannelId
                            AND         subscription.CreatorId = @CreatorId)
                    SELECT 1 AS FOUND
                ELSE
                    SELECT 0 AS FOUND", 
                new
                {
                    ChannelId = channelId.Value,
                    CreatorId = requester.Value
                });
        }

        public async Task AssertCreationAllowedAsync(UserId requester)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            var isCreationAllowed = await this.IsCreationAllowedAsync(requester);
            if (!isCreationAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to create subscription. {0}", requester));
            }
        }

        public async Task AssertUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            var isUpdateAllowed = await this.IsUpdateAllowedAsync(requester, subscriptionId);
            if (!isUpdateAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to update subscription. {0} {1}", requester, subscriptionId));
            }
        }

        public async Task AssertUpdateAllowedAsync(UserId requester, ChannelId channelId)
        {
            if (requester == null)
            {
                throw new ArgumentNullException("requester");
            }

            if (channelId == null)
            {
                throw new ArgumentNullException("channelId");
            }

            var isUpdateAllowed = await this.IsUpdateAllowedAsync(requester, channelId);
            if (!isUpdateAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to update channel. {0} {1}", requester, channelId));
            }
        }
    }
}