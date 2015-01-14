namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ChannelSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private Mock<IDataOwnership> dataOwnership;
        private ChannelSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.dataOwnership = new Mock<IDataOwnership>();
            this.target = new ChannelSecurity(this.dataOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldAllowIfUserOwnsChannel()
        {
            this.dataOwnership.Setup(_ => _.IsOwnerAsync(UserId, ChannelId)).ReturnsAsync(true);

            var result = await this.target.IsPostingAllowedAsync(UserId, ChannelId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldForbidIfUserDoesNotOwnChannel()
        {
            this.dataOwnership.Setup(_ => _.IsOwnerAsync(UserId, ChannelId)).ReturnsAsync(false);

            var result = await this.target.IsPostingAllowedAsync(UserId, ChannelId);

            Assert.IsFalse(result);

            try
            {
                await this.target.AssertPostingAllowedAsync(UserId, ChannelId);
                Assert.Fail("Expected unauthorized exception");
            }
            catch (UnauthorizedException)
            {
            }
        }
    }
}