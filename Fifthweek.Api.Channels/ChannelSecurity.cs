namespace Fifthweek.Api.Channels
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class ChannelSecurity : IChannelSecurity
    {
        private readonly IChannelOwnership channelOwnership;

        public Task<bool> IsWriteAllowedAsync(UserId requester, Shared.ChannelId channelId)
        {
            requester.AssertNotNull("requester");
            channelId.AssertNotNull("channelId");

            return this.channelOwnership.IsOwnerAsync(requester, channelId);
        }

        public async Task AssertWriteAllowedAsync(UserId requester, Shared.ChannelId channelId)
        {
            requester.AssertNotNull("requester");
            channelId.AssertNotNull("channelId");

            var isWriteAllowed = await this.IsWriteAllowedAsync(requester, channelId);
            if (!isWriteAllowed)
            {
                throw new UnauthorizedException("Not allowed to write to channel. {0} {1}", requester, channelId);
            }
        }
    }
}