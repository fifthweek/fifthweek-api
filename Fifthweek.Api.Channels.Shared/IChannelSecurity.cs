namespace Fifthweek.Api.Channels.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IChannelSecurity
    {
        Task<bool> IsWriteAllowedAsync(UserId requester, ChannelId channelId);

        Task AssertWriteAllowedAsync(UserId requester, ChannelId channelId);
    }
}