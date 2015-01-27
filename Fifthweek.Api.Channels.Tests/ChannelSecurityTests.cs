namespace Fifthweek.Api.Channels.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ChannelSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private Mock<IChannelOwnership> channelOwnership;
        private ChannelSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelOwnership = new Mock<IChannelOwnership>();
            this.target = new ChannelSecurity(this.channelOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldAllowIfUserOwnsChannel()
        {
            this.channelOwnership.Setup(_ => _.IsOwnerAsync(UserId, ChannelId)).ReturnsAsync(true);

            var result = await this.target.IsPostingAllowedAsync(UserId, ChannelId);

            Assert.IsTrue(result);

            await this.target.AssertPostingAllowedAsync(UserId, ChannelId);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldForbidIfUserDoesNotOwnChannel()
        {
            this.channelOwnership.Setup(_ => _.IsOwnerAsync(UserId, ChannelId)).ReturnsAsync(false);

            var result = await this.target.IsPostingAllowedAsync(UserId, ChannelId);

            Assert.IsFalse(result);

        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAuthorizingPost_ItShouldForbidIfUserDoesNotOwnChannel2()
        {
            this.channelOwnership.Setup(_ => _.IsOwnerAsync(UserId, ChannelId)).ReturnsAsync(false);

            await this.target.AssertPostingAllowedAsync(UserId, ChannelId);
        }
    }
}