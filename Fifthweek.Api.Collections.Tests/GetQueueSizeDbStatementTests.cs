namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetQueueSizeDbStatementTests : PersistenceTestsBase
    {
        private const int PostCount = 10;

        private static readonly Random Random = new Random();
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private GetQueueSizeDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new GetQueueSizeDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(QueueId, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenNoPostsExist_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionWithoutPostsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreNotQueued_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionWithPostsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndWereQueuedButNowLive_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionWithPostsAsync(testDatabase, queuePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreQueued_ItShouldReturnPositiveCount()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionWithPostsAsync(testDatabase, queuePosts: true, queueInFuture: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(PostCount, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreQueuedForDifferentCollection_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateQueuedPostsForAnotherCollectionAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateCollectionWithoutPostsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestEntitiesAsync(userId, channelId, QueueId.Value);
            }
        }

        private async Task CreateCollectionWithPostsAsync(TestDatabaseContext testDatabase, bool queuePosts = false, bool queueInFuture = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestEntitiesAsync(userId, channelId, QueueId.Value);

                var posts = new List<Post>();
                for (var i = 0; i < PostCount; i++)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channelId;
                    post.QueueId = queuePosts ? QueueId.Value : (Guid?)null;
                    post.LiveDate = queueInFuture ? Now.AddDays(1) : Now.AddDays(-1);
                    posts.Add(post);
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }

        private async Task CreateQueuedPostsForAnotherCollectionAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                var entities = await databaseContext.CreateTestEntitiesAsync(userId, channelId, QueueId.Value);

                var otherQueue = QueueTests.UniqueEntity(Random);
                otherQueue.BlogId = entities.Blog.Id;
                await databaseContext.Database.Connection.InsertAsync(otherQueue);

                var posts = new List<Post>();

                for (var i = 0; i < PostCount; i++)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channelId;
                    post.QueueId = otherQueue.Id;
                    post.LiveDate = Now.AddDays(1);
                    posts.Add(post);
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }
    }
}