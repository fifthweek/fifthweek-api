namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IDataOwnership
    {
        Task<bool> IsOwnerAsync(UserId user, SubscriptionId subscriptionId);

        Task<bool> IsOwnerAsync(UserId user, ChannelId channelId);
    }
}