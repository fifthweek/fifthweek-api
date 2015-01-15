namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubscriptionOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private SubscriptionOwnership target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SubscriptionOwnership(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldPassIfAtLeastOneSubscriptionMatchesSubscriptionAndCreator()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateSubscriptionAsync(UserId, SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsTrue(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsExist()
        {
            await this.InitializeDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscriptionOrCreator()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscription()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()));
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchCreator()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), SubscriptionId);
            await this.SnapshotDatabaseAsync();

            var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

            Assert.IsFalse(result);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId)
        {
            using (var databaseContext = this.NewDbContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newSubscriptionId.Value);
            }
        }
    }
}