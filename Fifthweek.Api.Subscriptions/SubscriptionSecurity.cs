using System;
using System.Data.Entity;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Subscriptions
{
    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public Task<bool> CanUpdateAsync(UserId userId, SubscriptionId subscriptionId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            if (subscriptionId == null)
            {
                throw new ArgumentNullException("subscriptionId");
            }

            return this.fifthweekDbContext.Subscriptions.AnyAsync(
                _ => _.Id == subscriptionId.Value && _.CreatorId == userId.Value);
        }
    }
}