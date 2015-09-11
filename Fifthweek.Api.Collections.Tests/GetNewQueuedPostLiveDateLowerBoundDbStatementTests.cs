namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetNewQueuedPostLiveDateLowerBoundDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.SpecifyKind(new SqlDateTime(DateTime.UtcNow).Value, DateTimeKind.Utc);
        private static readonly DateTime LatestQueuedPostTime = DateTime.SpecifyKind(new SqlDateTime(Now.AddDays(100)).Value, DateTimeKind.Utc);

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private GetNewQueuedPostLiveDateLowerBoundDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potential side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDateTime()
        {
            await this.target.ExecuteAsync(QueueId, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, DateTime.UtcNow);
        }

        [TestMethod]
        public async Task WhenNoPostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenLivePostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    createLivePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenScheduledPostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    createLivePosts: true,
                    createScheduledPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenAllQueuedPostAreLive_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenQueuedPostsExist_ItShouldReturnLiveDateOfLatestQueuedPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true,
                    createQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(QueueId, Now);

                Assert.AreEqual(result, LatestQueuedPostTime);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(
            TestDatabaseContext testDatabase,
            bool createLivePosts = false,
            bool createScheduledPosts = false,
            bool createLiveQueuedPosts = false,
            bool createQueuedPosts = false)
        {
            const int PostsToCreateForEachCategory = 5;

            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                await databaseContext.CreateTestEntitiesAsync(
                    UserId.Value,
                    ChannelId.Value,
                    QueueId.Value);

                var posts = new List<Post>();
                if (createLivePosts)
                {
                    for (var i = 0; i < PostsToCreateForEachCategory; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.QueueId = null;
                        post.LiveDate = Now.AddDays(random.Next(30) * -1);
                        posts.Add(post);
                    }
                }

                if (createScheduledPosts)
                {
                    for (var i = 0; i < PostsToCreateForEachCategory; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.QueueId = null;
                        post.LiveDate = Now.AddDays(random.Next(30));
                        posts.Add(post);
                    }
                }

                if (createLiveQueuedPosts)
                {
                    for (var i = 0; i < PostsToCreateForEachCategory; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.QueueId = QueueId.Value;
                        post.LiveDate = Now.AddDays(random.Next(30) * -1);
                        posts.Add(post);
                    }
                }

                if (createQueuedPosts)
                {
                    for (var i = 0; i < PostsToCreateForEachCategory; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.QueueId = QueueId.Value;
                        post.LiveDate = i == 0 ? LatestQueuedPostTime : Now.AddDays(random.Next(30));
                        posts.Add(post);
                    }
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }
    }
}