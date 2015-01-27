namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface ISubscriptionOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, SubscriptionId subscriptionId);
    }
}