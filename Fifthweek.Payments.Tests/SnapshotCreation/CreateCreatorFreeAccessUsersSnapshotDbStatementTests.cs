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
    public class CreateCreatorFreeAccessUsersSnapshotDbStatementTests : PersistenceTestsBase
    {
        private static readonly Guid SnapshotId = Guid.NewGuid();
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());

        private static readonly string Email1 = "email1";
        private static readonly string Email2 = "email2";

        private Mock<IGuidCreator> guidCreator;
        private Mock<ISnapshotTimestampCreator> timestampCreator;
        private CreateCreatorFreeAccessUsersSnapshotDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.timestampCreator = new Mock<ISnapshotTimestampCreator>();

            this.target = new CreateCreatorFreeAccessUsersSnapshotDbStatement(
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

                this.target = new CreateCreatorFreeAccessUsersSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId);

                var expectedCreatorFreeAccessUsersSnapshot = new CreatorFreeAccessUsersSnapshot(SnapshotId, Now, CreatorId.Value);
                var expectedCreatorFreeAccessUsersItem1 = new CreatorFreeAccessUsersSnapshotItem(SnapshotId, null, Email1);
                var expectedCreatorFreeAccessUsersItem2 = new CreatorFreeAccessUsersSnapshotItem(SnapshotId, null, Email2);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedCreatorFreeAccessUsersSnapshot,
                        expectedCreatorFreeAccessUsersItem1,
                        expectedCreatorFreeAccessUsersItem2,
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

                this.target = new CreateCreatorFreeAccessUsersSnapshotDbStatement(
                    this.guidCreator.Object,
                    this.timestampCreator.Object,
                    testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var creatorId = UserId.Random();
                await this.target.ExecuteAsync(creatorId);

                var expectedCreatorFreeAccessUsersSnapshot = new CreatorFreeAccessUsersSnapshot(SnapshotId, Now, creatorId.Value);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedCreatorFreeAccessUsersSnapshot,
                    }
                };
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelsAsync(
                    CreatorId.Value,
                    BlogId.Value,
                    ChannelId1.Value,
                    ChannelId2.Value);

                await databaseContext.SaveChangesAsync();
            }

            using (var databaseConnection = testDatabase.CreateConnection())
            {
                await databaseConnection.InsertAsync(new FreeAccessUser(BlogId.Value, Email1));
                await databaseConnection.InsertAsync(new FreeAccessUser(BlogId.Value, Email2));
            }
        }
    }
}