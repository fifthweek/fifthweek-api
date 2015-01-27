namespace Fifthweek.Api.Channels.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IChannelSecurity
    {
        Task<bool> IsPostingAllowedAsync(UserId requester, ChannelId channelId);

        Task AssertPostingAllowedAsync(UserId requester, ChannelId channelId);
    }
}