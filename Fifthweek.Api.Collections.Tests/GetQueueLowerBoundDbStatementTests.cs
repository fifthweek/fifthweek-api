namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetQueueLowerBoundDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime LowerBound = DateTime.SpecifyKind(new SqlDateTime(DateTime.UtcNow).Value, DateTimeKind.Utc);

        private Mock<IFifthweekDbConnectionFactory> databaseConnectionFactory;
        private GetQueueLowerBoundDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.databaseConnectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetQueueLowerBoundDbStatement(this.databaseConnectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, LowerBound);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(QueueId, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenCollectionDoesNotExist_ItShouldThrowException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetQueueLowerBoundDbStatement(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await ExpectedException.AssertExceptionAsync<Exception>(() =>
                {
                    var now = LowerBound;
                    return this.target.ExecuteAsync(QueueId, now);
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionExists_AndLowerBoundGreaterThanNow_ItShouldReturnLowerBound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetQueueLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var now = LowerBound.AddDays(-10);
                var actual = await this.target.ExecuteAsync(QueueId, now);

                Assert.AreEqual(LowerBound, actual);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionExists_AndLowerBoundLessThanNow_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetQueueLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var now = LowerBound.AddDays(10);
                var actual = await this.target.ExecuteAsync(QueueId, now);

                Assert.AreEqual(now, actual);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var collection = QueueTests.UniqueEntityWithForeignEntities(UserId.Value, ChannelId.Value, QueueId.Value);
                collection.QueueExclusiveLowerBound = LowerBound;
                databaseContext.Queues.Add(collection);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}