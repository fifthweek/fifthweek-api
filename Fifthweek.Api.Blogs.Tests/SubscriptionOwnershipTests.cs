namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubscriptionOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private BlogOwnership target;

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldPassIfAtLeastOneSubscriptionMatchesSubscriptionAndCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateSubscriptionAsync(UserId, BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);

                using (var databaseContext = testDatabase.CreateContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Blogs");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscriptionOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), new BlogId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchSubscription()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateSubscriptionAsync(UserId, new BlogId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingSubscriptionOwnership_ItShouldFailIfNoSubscriptionsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new BlogOwnership(testDatabase);
                await this.CreateSubscriptionAsync(new UserId(Guid.NewGuid()), BlogId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, BlogId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateSubscriptionAsync(UserId newUserId, BlogId newBlogId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestSubscriptionAsync(newUserId.Value, newBlogId.Value);
            }
        }
    }
}