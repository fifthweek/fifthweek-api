namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ISubscriptionOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.SubscriptionId subscriptionId);
    }
}