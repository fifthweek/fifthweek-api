namespace Fifthweek.Api.Subscriptions.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISubscriptionSecurity
    {
        Task<bool> IsCreationAllowedAsync(UserId requester);

        Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);

        Task AssertCreationAllowedAsync(UserId requester);

        Task AssertUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);
    }
}