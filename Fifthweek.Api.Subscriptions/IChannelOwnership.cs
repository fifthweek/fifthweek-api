namespace Fifthweek.Api.Subscriptions
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IChannelOwnership
    {
        Task<bool> IsOwnerAsync(UserId user, ChannelId channelId);
    }
}