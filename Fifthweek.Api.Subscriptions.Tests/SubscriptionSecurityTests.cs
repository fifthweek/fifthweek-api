namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscriptionSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ISubscriptionOwnership> subscriptionOwnership;
        private SubscriptionSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.subscriptionOwnership = new Mock<ISubscriptionOwnership>();
            this.target = new SubscriptionSecurity(this.requesterSecurity.Object, this.subscriptionOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldAllowIfUserHasCreatorRole()
        {
            this.requesterSecurity.Setup(_ => _.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var result = await this.target.IsCreationAllowedAsync(Requester);

            Assert.IsTrue(result);

            await this.target.AssertCreationAllowedAsync(Requester);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldForbidIfUserHasCreatorRole()
        {
            this.requesterSecurity.Setup(_ => _.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(false);

            var result = await this.target.IsCreationAllowedAsync(Requester);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertCreationAllowedAsync(Requester);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldAllowIfUserOwnsSubscription()
        {
            this.subscriptionOwnership.Setup(_ => _.IsOwnerAsync(UserId, SubscriptionId)).ReturnsAsync(true);

            var result = await this.target.IsWriteAllowedAsync(UserId, SubscriptionId);

            Assert.IsTrue(result);

            await this.target.AssertWriteAllowedAsync(UserId, SubscriptionId);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfUserDoesNotOwnSubscription()
        {
            this.subscriptionOwnership.Setup(_ => _.IsOwnerAsync(UserId, SubscriptionId)).ReturnsAsync(false);

            var result = await this.target.IsWriteAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertWriteAllowedAsync(UserId, SubscriptionId);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }
    }
}