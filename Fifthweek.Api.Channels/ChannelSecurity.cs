namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class ChannelSecurity : IChannelSecurity
    {
        private readonly IChannelOwnership channelOwnership;

        public Task<bool> IsPostingAllowedAsync(UserId requester, Shared.ChannelId channelId)
        {
            requester.AssertNotNull("requester");
            channelId.AssertNotNull("channelId");

            return this.channelOwnership.IsOwnerAsync(requester, channelId);
        }

        public async Task AssertPostingAllowedAsync(UserId requester, Shared.ChannelId channelId)
        {
            requester.AssertNotNull("requester");
            channelId.AssertNotNull("channelId");

            var isPostingAllowed = await this.IsPostingAllowedAsync(requester, channelId);
            if (!isPostingAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to post to channel. {0} {1}", requester, channelId));
            }
        }
    }
}