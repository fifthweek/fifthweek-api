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
    public class CreateCreatorChannelsSnapshotDbStatementTests : PersistenceTestsBase
    {
        private static readonly Guid SnapshotId = Guid.NewGuid();
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());

        private Mock<IGuidCreator> guidCreator;
        private Mock<ISnapshotTimestampCreator> timestampCreator;
        private CreateCreatorChannelsSnapshotDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.timestampCreator = new Mock<ISnapshotTimestampCreator>();

            this.target = new CreateCreatorChannelsSnapshotDbStatement(
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

                this.target = new CreateCreatorChannelsSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                var channels = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId);

                var expectedCreatorChannelSnapshot = new CreatorChannelsSnapshot(SnapshotId, Now, CreatorId.Value);
                var expectedCreatorChannelItem1 = new CreatorChannelsSnapshotItem(SnapshotId, null, ChannelId1.Value, channels[0].Price);
                var expectedCreatorChannelItem2 = new CreatorChannelsSnapshotItem(SnapshotId, null, ChannelId2.Value, channels[1].Price);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedCreatorChannelSnapshot,
                        expectedCreatorChannelItem1,
                        expectedCreatorChannelItem2,
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

                this.target = new CreateCreatorChannelsSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                var channels = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var creatorId = UserId.Random();
                await this.target.ExecuteAsync(creatorId);

                var expectedCreatorChannelSnapshot = new CreatorChannelsSnapshot(SnapshotId, Now, creatorId.Value);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedCreatorChannelSnapshot,
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

                await databaseContext.SaveChangesAsync();

                return result;
            }
        }
    }
}