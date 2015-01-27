namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IChannelOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.ChannelId channelId);
    }
}