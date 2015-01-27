namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

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
        public async Task WhenCalled_ItShouldReturnChannelsAndCollections()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetChannelsAndCollectionsDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Channels.Count);

                var channel1 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId1));
                var channel2 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId2));
                var channel3 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId3));

                Assert.AreEqual(0, channel1.Collections.Count);
                Assert.AreEqual(1, channel2.Collections.Count);
                Assert.AreEqual(2, channel3.Collections.Count);

                Assert.IsTrue(channel2.Collections.Any(v => v.CollectionId.Equals(CollectionId1)));
                Assert.IsTrue(channel3.Collections.Any(v => v.CollectionId.Equals(CollectionId2)));
                Assert.IsTrue(channel3.Collections.Any(v => v.CollectionId.Equals(CollectionId3)));

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCalledWithNoChannels_ItShouldReturnEmptyChannelsAndCollections()
        {
            await this.DatabaseTestAsync(async testDatabase =>
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
                await this.CreateTestCollectionAsync(databaseContext);
            }
        }

        private Task CreateTestCollectionAsync(
            IFifthweekDbContext databaseContext)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = UserId.Value;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            // First channel has no collections.
            var channel1 = ChannelTests.UniqueEntity(random);
            channel1.Id = ChannelId1.Value;
            channel1.Subscription = subscription;
            channel1.SubscriptionId = subscription.Id;

            // Second channel has one collection.
            var channel2 = ChannelTests.UniqueEntity(random);
            channel2.Id = ChannelId2.Value;
            channel2.Subscription = subscription;
            channel2.SubscriptionId = subscription.Id;

            var collection1 = CollectionTests.UniqueEntity(random);
            collection1.Id = CollectionId1.Value;
            collection1.Channel = channel2;
            collection1.ChannelId = channel2.Id;

            // Third channel has two collections.
            var channel3 = ChannelTests.UniqueEntity(random);
            channel3.Id = ChannelId3.Value;
            channel3.Subscription = subscription;
            channel3.SubscriptionId = subscription.Id;

            var collection2 = CollectionTests.UniqueEntity(random);
            collection2.Id = CollectionId2.Value;
            collection2.Channel = channel3;
            collection2.ChannelId = channel3.Id;

            var collection3 = CollectionTests.UniqueEntity(random);
            collection3.Id = CollectionId3.Value;
            collection3.Channel = channel3;
            collection3.ChannelId = channel3.Id;

            databaseContext.Channels.Add(channel1);
            databaseContext.Channels.Add(channel2);
            databaseContext.Channels.Add(channel3);
            databaseContext.Collections.Add(collection1);
            databaseContext.Collections.Add(collection2);
            databaseContext.Collections.Add(collection3);

            return databaseContext.SaveChangesAsync();
        }
    }
}