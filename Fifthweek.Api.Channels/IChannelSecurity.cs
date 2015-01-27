namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IChannelSecurity
    {
        Task<bool> IsPostingAllowedAsync(UserId requester, ChannelId channelId);

        Task AssertPostingAllowedAsync(UserId requester, ChannelId channelId);
    }
}