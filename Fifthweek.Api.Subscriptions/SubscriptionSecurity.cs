namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IUserManager userManager;
        private readonly ISubscriptionOwnership subscriptionOwnership;

        public Task<bool> IsCreationAllowedAsync(UserId requester)
        {
            requester.AssertNotNull("requester");

            return this.userManager.IsInRoleAsync(requester.Value, FifthweekRole.Creator);
        }

        public Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            requester.AssertNotNull("requester");
            subscriptionId.AssertNotNull("subscriptionId");

            return this.subscriptionOwnership.IsOwnerAsync(requester, subscriptionId);
        }

        public async Task AssertCreationAllowedAsync(UserId requester)
        {
            requester.AssertNotNull("requester");

            var isCreationAllowed = await this.IsCreationAllowedAsync(requester);
            if (!isCreationAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to create subscription. {0}", requester));
            }
        }

        public async Task AssertUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            requester.AssertNotNull("requester");
            subscriptionId.AssertNotNull("subscriptionId");

            var isUpdateAllowed = await this.IsUpdateAllowedAsync(requester, subscriptionId);
            if (!isUpdateAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to update subscription. {0} {1}", requester, subscriptionId));
            }
        }
    }
}