namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueueOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private QueueOwnership target;

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldPassIfAtLeastOneCollectionMatchesCollectionAndCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new QueueOwnership(testDatabase);
                await this.CreateCollectionAsync(UserId, QueueId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, QueueId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new QueueOwnership(testDatabase);

                using (var databaseContext = testDatabase.CreateContext())
                {
                    // We must delete ChannelSubscriptions first as there isn't a cascade delete setup
                    // due to multiple cascade branches.
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Likes;DELETE FROM Comments;DELETE FROM ChannelSubscriptions;DELETE FROM Channels");

                    // Deleting all channels will actually delete all collections. We don't delete collections directly due to cascading deletes
                    // not being possible on this table due to possible cyclic references in the schema. The following line should have no effect
                    // on the database, but is added for clarity.
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Collections");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, QueueId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCollectionOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new QueueOwnership(testDatabase);
                await this.CreateCollectionAsync(new UserId(Guid.NewGuid()), new QueueId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, QueueId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new QueueOwnership(testDatabase);
                await this.CreateCollectionAsync(UserId, new QueueId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, QueueId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new QueueOwnership(testDatabase);
                await this.CreateCollectionAsync(new UserId(Guid.NewGuid()), QueueId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, QueueId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateCollectionAsync(UserId newUserId, QueueId newQueueId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestEntitiesAsync(newUserId.Value, Guid.NewGuid(), newQueueId.Value);
            }
        }
    }
}