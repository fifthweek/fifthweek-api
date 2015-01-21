namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetChannelsAndCollectionsDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId1 = new CollectionId(Guid.NewGuid());
        private static readonly CollectionId CollectionId2 = new CollectionId(Guid.NewGuid());
        private static readonly CollectionId CollectionId3 = new CollectionId(Guid.NewGuid());

        private GetChannelsAndCollectionsDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            var strictDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.target = new GetChannelsAndCollectionsDbStatement(strictDbContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalled_ItShouldCheckThatUserIdIsNotNull()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalled_ItShouldReturnChannelsAndCollections()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetChannelsAndCollectionsDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Channels.Count);
                Assert.AreEqual(0, result.Channels[0].Collections.Count);
                Assert.AreEqual(1, result.Channels[1].Collections.Count);
                Assert.AreEqual(2, result.Channels[2].Collections.Count);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNoChannels_ItShouldReturnEmptyChannelsAndCollections()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetChannelsAndCollectionsDbStatement(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);
                
                Assert.IsNotNull(result);
                Assert.AreEqual(0, result.Channels.Count);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                // First channel has no collections.
                await databaseContext.CreateTestChannelAsync(UserId.Value, ChannelId1.Value);

                // Second channel has one collection.
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId2.Value, CollectionId1.Value);

                // Third channel has two collections.
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId3.Value, CollectionId2.Value);
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId3.Value, CollectionId3.Value);
            }
        }
    }
}