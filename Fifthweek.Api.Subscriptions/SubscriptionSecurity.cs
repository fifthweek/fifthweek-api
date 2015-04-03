namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class SubscriptionSecurity : ISubscriptionSecurity
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly ISubscriptionOwnership subscriptionOwnership;

        public Task<bool> IsCreationAllowedAsync(Requester requester)
        {
            requester.AssertNotNull("requester");

            return this.requesterSecurity.IsInRoleAsync(requester, FifthweekRole.Creator);
        }

        public Task<bool> IsWriteAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            requester.AssertNotNull("requester");
            subscriptionId.AssertNotNull("subscriptionId");

            return this.subscriptionOwnership.IsOwnerAsync(requester, subscriptionId);
        }

        public async Task AssertCreationAllowedAsync(Requester requester)
        {
            requester.AssertNotNull("requester");

            var isCreationAllowed = await this.IsCreationAllowedAsync(requester);
            if (!isCreationAllowed)
            {
                throw new UnauthorizedException("Not allowed to create subscription. {0}", requester);
            }
        }

        public async Task AssertWriteAllowedAsync(UserId requester, SubscriptionId subscriptionId)
        {
            requester.AssertNotNull("requester");
            subscriptionId.AssertNotNull("subscriptionId");

            var isUpdateAllowed = await this.IsWriteAllowedAsync(requester, subscriptionId);
            if (!isUpdateAllowed)
            {
                throw new UnauthorizedException("Not allowed to update subscription. {0} {1}", requester, subscriptionId);
            }
        }
    }
}