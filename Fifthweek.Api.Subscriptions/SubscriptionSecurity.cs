using System;
using System.Threading.Tasks;
using Dapper;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions
{
    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

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

            return this.fifthweekDbContext.Database.Connection.ExecuteScalarAsync<bool>(
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
    }
}