namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class DataOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private DataOwnership target;

        [TestInitialize]
        public override void Initialize()
        {
            base.Initialize();
            this.target = new DataOwnership(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldPassIfAtLeastOneSubscriptionMatchesSubscriptionAndCreator()
        {
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsTrue(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsExist()
        {
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscriptionOrCreator()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscription()
        {
            await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchCreator()
        {
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

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