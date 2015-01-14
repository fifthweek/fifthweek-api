namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscriptionSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private Mock<IUserManager> userManager;
        private Mock<ISubscriptionOwnership> subscriptionOwnership;
        private SubscriptionSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.subscriptionOwnership = new Mock<ISubscriptionOwnership>();
            this.target = new SubscriptionSecurity(this.userManager.Object, this.subscriptionOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldAllowIfUserHasCreatorRole()
        {
            this.userManager.Setup(_ => _.IsInRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(true);
            
            var result = await this.target.IsCreationAllowedAsync(UserId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldForbidIfUserHasCreatorRole()
        {
            this.userManager.Setup(_ => _.IsInRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(false);
            
            var result = await this.target.IsCreationAllowedAsync(UserId);

            Assert.IsFalse(result);

            try
            {
                await this.target.AssertCreationAllowedAsync(UserId);
                Assert.Fail("Expected unauthorized exception");
            }
            catch (UnauthorizedException)
            {
            }
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldAllowIfUserOwnsSubscription()
        {
            this.subscriptionOwnership.Setup(_ => _.IsOwnerAsync(UserId, SubscriptionId)).ReturnsAsync(true);

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfUserDoesNotOwnSubscription()
        {
            this.subscriptionOwnership.Setup(_ => _.IsOwnerAsync(UserId, SubscriptionId)).ReturnsAsync(false);

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);

            try
            {
                await this.target.AssertUpdateAllowedAsync(UserId, SubscriptionId);
                Assert.Fail("Expected unauthorized exception");
            }
            catch (UnauthorizedException)
            {
            }
        }
    }
}