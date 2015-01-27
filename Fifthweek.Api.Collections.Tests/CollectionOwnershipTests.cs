namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class CollectionOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private CollectionOwnership target;

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldPassIfAtLeastOneCollectionMatchesCollectionAndCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CollectionOwnership(testDatabase.NewContext());
                await this.CreateCollectionAsync(UserId, CollectionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, CollectionId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CollectionOwnership(testDatabase.NewContext());

                using (var databaseContext = testDatabase.NewContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Channels");

                    // Deleting all channels will actually delete all collections. We don't delete collections directly due to cascading deletes
                    // not being possible on this table due to possible cyclic references in the schema. The following line should have no effect
                    // on the database, but is added for clarity.
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Collections");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, CollectionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCollectionOrCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CollectionOwnership(testDatabase.NewContext());
                await this.CreateCollectionAsync(new UserId(Guid.NewGuid()), new CollectionId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, CollectionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CollectionOwnership(testDatabase.NewContext());
                await this.CreateCollectionAsync(UserId, new CollectionId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, CollectionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingCollectionOwnership_ItShouldFailIfNoCollectionsMatchCreator()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new CollectionOwnership(testDatabase.NewContext());
                await this.CreateCollectionAsync(new UserId(Guid.NewGuid()), CollectionId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, CollectionId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateCollectionAsync(UserId newUserId, CollectionId newCollectionId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(newUserId.Value, Guid.NewGuid(), newCollectionId.Value);
            }
        }
    }
}