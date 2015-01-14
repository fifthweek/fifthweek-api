namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface ISubscriptionSecurity
    {
        Task<bool> IsCreationAllowedAsync(UserId requester);

        Task<bool> IsUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);

        Task<bool> IsUpdateAllowedAsync(UserId requester, ChannelId channelId);

        Task AssertCreationAllowedAsync(UserId requester);

        Task AssertUpdateAllowedAsync(UserId requester, SubscriptionId subscriptionId);

        Task AssertUpdateAllowedAsync(UserId requester, ChannelId channelId);
    }
}