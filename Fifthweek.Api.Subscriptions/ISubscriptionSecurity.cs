namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface ISubscriptionSecurity
    {
        Task<bool> IsCreationAllowedAsync(UserId requester);

        Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);

        Task AssertCreationAllowedAsync(UserId requester);

        Task AssertUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);
    }
}