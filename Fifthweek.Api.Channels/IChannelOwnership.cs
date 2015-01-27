namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IChannelOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.ChannelId channelId);
    }
}