namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscriptionSecurityTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private Mock<IUserManager> userManager;
        private SubscriptionSecurity target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.userManager = new Mock<IUserManager>();
            this.target = new SubscriptionSecurity(this.userManager.Object, this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
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
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldAllowIfAtLeastOneSubscriptionMatchesSubscriptionAndCreator()
        {
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsTrue(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfNoSubscriptionsExist()
        {
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfNoSubscriptionsMatchSubscriptionOrCreator()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfNoSubscriptionsMatchSubscription()
        {
            await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfNoSubscriptionsMatchCreator()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsUpdateAllowedAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = newSubscriptionId.Value;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Subscriptions.Add(subscription);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}