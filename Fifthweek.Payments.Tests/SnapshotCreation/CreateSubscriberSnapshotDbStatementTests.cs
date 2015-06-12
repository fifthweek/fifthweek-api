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
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateSubscriberSnapshotDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId SubscriberId = new UserId(Guid.NewGuid());

        private static readonly int AcceptedPrice1 = 10;
        private static readonly int AcceptedPrice2 = 20;

        private Mock<ISnapshotTimestampCreator> timestampCreator;
        private CreateSubscriberSnapshotDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ISnapshotTimestampCreator>();

            this.target = new CreateSubscriberSnapshotDbStatement(
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
                this.timestampCreator.Setup(v => v.Create()).Returns(Now);

                this.target = new CreateSubscriberSnapshotDbStatement(
                    this.timestampCreator.Object,
                    testDatabase);

                var user = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(SubscriberId);

                var expectedSubscriberSnapshot = new SubscriberSnapshot(Now, SubscriberId.Value, user.Email);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedSubscriberSnapshot
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenCreatorDoesNotExist_ShouldCreateEmptySnapshot()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.timestampCreator.Setup(v => v.Create()).Returns(Now);

                this.target = new CreateSubscriberSnapshotDbStatement(
                    this.timestampCreator.Object,
                    testDatabase);

                var user = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var subscriberId = UserId.Random();
                await this.target.ExecuteAsync(subscriberId);

                var expectedCreatorChannelSnapshot = new SubscriberSnapshot(Now, subscriberId.Value, null);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        expectedCreatorChannelSnapshot,
                    }
                };
            });
        }

        private async Task<FifthweekUser> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var user = await databaseContext.CreateTestUserAsync(SubscriberId.Value);

                await databaseContext.SaveChangesAsync();

                return user;
            }
        }
    }
}