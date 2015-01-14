namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface ISubscriptionOwnership
    {
        Task<bool> IsOwnerAsync(UserId user, SubscriptionId subscriptionId);
    }
}