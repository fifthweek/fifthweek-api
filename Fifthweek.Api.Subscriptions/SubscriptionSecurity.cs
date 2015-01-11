using System;
using System.Threading.Tasks;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Identity;

namespace Fifthweek.Api.Subscriptions
{
    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IUserManager userManager;

        public Task<bool> IsCreationAllowedAsync(UserId userId)
        {
            if (userId == null)
            {
                throw new ArgumentNullException("userId");
            }

            return this.userManager.IsInRoleAsync(userId.Value, FifthweekRole.Creator);
        }
    }
}