namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubscriptionOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private SubscriptionOwnership target;

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldPassIfAtLeastOneSubscriptionMatchesSubscriptionAndCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SubscriptionOwnership(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(UserId, SubscriptionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SubscriptionOwnership(testDatabase.NewContext());

                using (var databaseContext = testDatabase.NewContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Subscriptions");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscriptionOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SubscriptionOwnership(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new SubscriptionId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SubscriptionOwnership(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(UserId, new SubscriptionId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SubscriptionOwnership(testDatabase.NewContext());
                await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), SubscriptionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, SubscriptionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, SubscriptionId newSubscriptionId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newSubscriptionId.Value);
            }
        }
    }
}