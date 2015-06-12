namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateSubscriberChannelsSnapshotDbStatementTests : PersistenceTestsBase
    {
        private static readonly Guid SnapshotId = Guid.NewGuid();
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly UserId SubscriberId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());

        private static readonly int AcceptedPrice1 = 10;
        private static readonly int AcceptedPrice2 = 20;

        private Mock<IGuidCreator> guidCreator;
        private Mock<ISnapshotTimestampCreator> timestampCreator;
        private CreateSubscriberChannelsSnapshotDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.timestampCreator = new Mock<ISnapshotTimestampCreator>();

            this.target = new CreateSubscriberChannelsSnapshotDbStatement(
                this.guidCreator.Object,
                this.timestampCreator.Object,
                new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreatorIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenCreatorExists_ShouldCreateSnapshot()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(SnapshotId);
                this.timestampCreator.Setup(v => v.Create()).Returns(Now);

                this.target = new CreateSubscriberChannelsSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                var channels = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(SubscriberId);

                var expectedSubscriberChannelSnapshot = new SubscriberChannelsSnapshot(SnapshotId, Now, SubscriberId.Value);
                var expectedSubscriberChannelItem1 = new SubscriberChannelsSnapshotItem(SnapshotId, null, ChannelId1.Value, AcceptedPrice1, Now);
                var expectedSubscriberChannelItem2 = new SubscriberChannelsSnapshotItem(SnapshotId, null, ChannelId2.Value, AcceptedPrice2, Now);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedSubscriberChannelSnapshot,
                        expectedSubscriberChannelItem1,
                        expectedSubscriberChannelItem2,
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenCreatorDoesNotExist_ShouldCreateEmptySnapshot()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(SnapshotId);
                this.timestampCreator.Setup(v => v.Create()).Returns(Now);

                this.target = new CreateSubscriberChannelsSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                var channels = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var subscriberId = UserId.Random();
                await this.target.ExecuteAsync(subscriberId);

                var expectedSubscriberChannelSnapshot = new SubscriberChannelsSnapshot(SnapshotId, Now, subscriberId.Value);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedSubscriberChannelSnapshot,
                    }
                };
            });
        }

        private async Task<IReadOnlyList<Channel>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var result = await databaseContext.CreateTestChannelsAsync(
                    CreatorId.Value,
                    BlogId.Value,
                    ChannelId1.Value,
                    ChannelId2.Value);

                await databaseContext.CreateTestUserAsync(SubscriberId.Value);

                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(SubscriberId.Value, ChannelId1.Value, Now, AcceptedPrice1);
                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(SubscriberId.Value, ChannelId2.Value, Now, AcceptedPrice2);
                
                await databaseContext.SaveChangesAsync();

                return result;
            }
        }
    }
}