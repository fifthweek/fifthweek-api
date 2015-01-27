namespace Fifthweek.Api.Subscriptions
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class SubscriptionOwnership : ISubscriptionOwnership
    {
        private readonly IFifthweekDbContext databaseContext;

        public Task<bool> IsOwnerAsync(UserId userId, Shared.SubscriptionId subscriptionId)
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
                    SELECT 0 AS FOUND",
                new
                {
                    SubscriptionId = subscriptionId.Value,
                    CreatorId = userId.Value
                });
        }
    }
}