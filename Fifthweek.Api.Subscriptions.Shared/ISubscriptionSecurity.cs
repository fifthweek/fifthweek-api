namespace Fifthweek.Api.Subscriptions.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISubscriptionSecurity
    {
        Task<bool> IsCreationAllowedAsync(Requester requester);

        Task<bool> IsWriteAllowedAsync(UserId requester, SubscriptionId subscriptionId);

        Task AssertCreationAllowedAsync(Requester requester);

        Task AssertWriteAllowedAsync(UserId requester, SubscriptionId subscriptionId);
    }
}