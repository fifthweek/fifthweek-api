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
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IFifthweekDbContext> databaseContext;
        private GetQueueSizeDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effecting components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new GetQueueSizeDbStatement(databaseContext);
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
            await this.target.ExecuteAsync(CollectionId, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenNoPostsExist_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateCollectionWithoutPostsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreNotQueued_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateCollectionWithPostsAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndWereQueuedButNowLive_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateCollectionWithPostsAsync(testDatabase, queuePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreQueued_ItShouldReturnPositiveCount()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateCollectionWithPostsAsync(testDatabase, queuePosts: true, queueInFuture: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(PostCount, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostsExistAndAreQueuedForDifferentCollection_ItShouldReturnZero()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateQueuedPostsForAnotherCollectionAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(0, result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateCollectionWithoutPostsAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(userId, channelId, CollectionId.Value);
            }
        }

        private async Task CreateCollectionWithPostsAsync(TestDatabaseContext testDatabase, bool queuePosts = false, bool queueInFuture = false)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(userId, channelId, CollectionId.Value);

                var posts = new List<Post>();
                for (var i = 0; i < PostCount; i++)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channelId;
                    post.CollectionId = CollectionId.Value;
                    post.ScheduledByQueue = queuePosts;
                    post.LiveDate = queueInFuture ? Now.AddDays(1) : Now.AddDays(-1);
                    posts.Add(post);
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }

        private async Task CreateQueuedPostsForAnotherCollectionAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var userId = Guid.NewGuid();
                var channelId = Guid.NewGuid();
                await databaseContext.CreateTestCollectionAsync(userId, channelId, CollectionId.Value);

                var otherCollection = CollectionTests.UniqueEntity(Random);
                otherCollection.ChannelId = channelId;
                await databaseContext.Database.Connection.InsertAsync(otherCollection);

                var posts = new List<Post>();

                for (var i = 0; i < PostCount; i++)
                {
                    var post = PostTests.UniqueFileOrImage(Random);
                    post.ChannelId = channelId;
                    post.CollectionId = otherCollection.Id;
                    post.ScheduledByQueue = true;
                    post.LiveDate = Now.AddDays(1);
                    posts.Add(post);
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }
    }
}