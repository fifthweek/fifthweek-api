namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class TryGetUnqueuedPostCollectionDbStatementTests : PersistenceTestsBase
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private TryGetUnqueuedPostCollectionDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new TryGetUnqueuedPostCollectionDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndIsQueued_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, queuePost: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndWasNotQueuedAndIsLive_ItShouldReturnCollectionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, queuePost: false, liveDateInFuture: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId);

                Assert.AreEqual(result, QueueId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExistsAndIsNotQueued_ItShouldReturnCollectionId()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, queuePost: false, liveDateInFuture: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(PostId);

                Assert.AreEqual(result, QueueId);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool queuePost, bool liveDateInFuture = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(userId, channelId, QueueId.Value);

                var post = PostTests.UniqueFileOrImage(new Random());
                post.Id = PostId.Value;
                post.ChannelId = channelId;
                post.QueueId = QueueId.Value;
                post.ScheduledByQueue = queuePost;
                post.LiveDate = liveDateInFuture ? Now.AddDays(1) : Now.AddDays(-1);
                await databaseContext.Database.Connection.InsertAsync(post);
            }
        }
    }
}