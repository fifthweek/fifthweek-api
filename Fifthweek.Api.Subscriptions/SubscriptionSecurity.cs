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

        public Task<bool> IsCreationAllowedAsync(UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return this.userManager.IsInRoleAsync(userId.Value, FifthweekRole.Creator);
        }

        public Task<bool> IsUpdateAllowedAsync(UserId userId, SubscriptionId subscriptionId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
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
                    SELECT 0 AS FOUND", new
                {
                    SubscriptionId = subscriptionId.Value,
                    CreatorId = userId.Value
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
                throw new UnauthorizedException(string.Format(
                    "Not allowed to create subscription. User={0}",
                    requester.Value));
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
                throw new UnauthorizedException(string.Format(
                    "Not allowed to update subscription. User={0} Subscription={1}",
                    requester.Value,
                    subscriptionId.Value));
            }
        }
    }
}