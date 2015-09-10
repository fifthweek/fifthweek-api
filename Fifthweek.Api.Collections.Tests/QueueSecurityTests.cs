namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class QueueSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private Mock<IQueueOwnership> collectionOwnership;
        private QueueSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionOwnership = new Mock<IQueueOwnership>();
            this.target = new QueueSecurity(this.collectionOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldAllowIfUserOwnsCollection()
        {
            this.collectionOwnership.Setup(_ => _.IsOwnerAsync(UserId, QueueId)).ReturnsAsync(true);

            var result = await this.target.IsWriteAllowedAsync(UserId, QueueId);

            Assert.IsTrue(result);

            await this.target.AssertWriteAllowedAsync(UserId, QueueId);
        }

        [TestMethod]
        public async Task WhenAuthorizingPost_ItShouldForbidIfUserDoesNotOwnCollection()
        {
            this.collectionOwnership.Setup(_ => _.IsOwnerAsync(UserId, QueueId)).ReturnsAsync(false);

            var result = await this.target.IsWriteAllowedAsync(UserId, QueueId);

            Assert.IsFalse(result);

        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAuthorizingPost_ItShouldForbidIfUserDoesNotOwnCollection2()
        {
            this.collectionOwnership.Setup(_ => _.IsOwnerAsync(UserId, QueueId)).ReturnsAsync(false);

            await this.target.AssertWriteAllowedAsync(UserId, QueueId);
        }
    }
}