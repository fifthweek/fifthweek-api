using System;
using System.Threading.Tasks;
using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Persistence;
using Fifthweek.Api.Persistence.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private Mock<IUserManager> userManager;
        private SubscriptionSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.target = new SubscriptionSecurity(this.userManager.Object);
        }

        [TestMethod]
        public async Task WhenUserHasCreatorRole_ItShouldAllowCreation()
        {
            this.userManager.Setup(_ => _.IsInRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(true);
            
            var result = await this.target.IsCreationAllowedAsync(UserId);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenUserDoesNotHaveCreatorRole_ItShouldForbidCreation()
        {
            this.userManager.Setup(_ => _.IsInRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(false);
            
            var result = await this.target.IsCreationAllowedAsync(UserId);

            Assert.IsFalse(result);
        }
    }
}