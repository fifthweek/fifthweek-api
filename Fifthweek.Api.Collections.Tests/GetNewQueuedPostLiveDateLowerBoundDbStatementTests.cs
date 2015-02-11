namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

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
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.SpecifyKind(new SqlDateTime(DateTime.UtcNow).Value, DateTimeKind.Utc);
        private static readonly DateTime LatestQueuedPostTime = DateTime.SpecifyKind(new SqlDateTime(Now.AddDays(100)).Value, DateTimeKind.Utc);
        private static readonly DateTime CollectionLowerBound = DateTime.SpecifyKind(new SqlDateTime(Now.AddDays(50)).Value, DateTimeKind.Utc);

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
            await this.target.ExecuteAsync(CollectionId, DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, DateTime.UtcNow);
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundHasPassed_AndNoPostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundHasPassed_AndLivePostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: false,
                    createLivePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundHasPassed_AndScheduledPostsExist_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: false,
                    createLivePosts: true,
                    createScheduledPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundHasPassed_AndAllQueuedPostAreLive_ItShouldReturnNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: false,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, Now);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundIsInFuture_AndNoPostsExist_ItShouldReturnCollectionLowerBound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, CollectionLowerBound);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundIsInFuture_AndLivePostsExist_ItShouldReturnCollectionLowerBound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: true,
                    createLivePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, CollectionLowerBound);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundIsInFuture_AndScheduledPostsExist_ItShouldReturnCollectionLowerBound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: true,
                    createLivePosts: true,
                    createScheduledPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, CollectionLowerBound);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCollectionLowerBoundIsInFuture_AndAllQueuedPostAreLive_ItShouldReturnCollectionLowerBound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: true,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, CollectionLowerBound);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenQueuedPostsExist_AndLatestPostExceedsCollectionLowerBound_ItShouldReturnLiveDateOfLatestQueuedPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: false,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true,
                    createQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, LatestQueuedPostTime);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        /// <summary>
        /// This decision was made lightly. It's a scenario that could potentially occur, so just wanted to write a test for the
        /// sake of specifying some predictable and deterministic behaviour.
        /// </summary>
        [TestMethod]
        public async Task WhenQueuedPostsExist_AndLatestPostPreceedsCollectionLowerBound_ItShouldReturnLiveDateOfLatestQueuedPost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetNewQueuedPostLiveDateLowerBoundDbStatement(testDatabase);
                await this.CreateEntitiesAsync(
                    testDatabase,
                    setCollectionLowerBoundInFuture: true,
                    createLivePosts: true,
                    createScheduledPosts: true,
                    createLiveQueuedPosts: true,
                    createQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId, Now);

                Assert.AreEqual(result, LatestQueuedPostTime);
                Assert.AreEqual(result.Kind, DateTimeKind.Utc);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(
            TestDatabaseContext testDatabase,
            bool setCollectionLowerBoundInFuture,
            bool createLivePosts = false,
            bool createScheduledPosts = false,
            bool createLiveQueuedPosts = false,
            bool createQueuedPosts = false)
        {
            const int PostsToCreateForEachCategory = 5;

            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var collection = CollectionTests.UniqueEntityWithForeignEntities(
                    UserId.Value,
                    ChannelId.Value,
                    CollectionId.Value);

                collection.QueueExclusiveLowerBound = setCollectionLowerBoundInFuture 
                    ? CollectionLowerBound
                    : Now.AddDays(random.Next(30) * -1);

                databaseContext.Collections.Add(collection);
                await databaseContext.SaveChangesAsync();

                var posts = new List<Post>();
                if (createLivePosts)
                {
                    for (var i = 0; i < PostsToCreateForEachCategory; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.CollectionId = CollectionId.Value;
                        post.ScheduledByQueue = false;
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
                        post.CollectionId = CollectionId.Value;
                        post.ScheduledByQueue = false;
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
                        post.CollectionId = CollectionId.Value;
                        post.ScheduledByQueue = true;
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
                        post.CollectionId = CollectionId.Value;
                        post.ScheduledByQueue = true;
                        post.LiveDate = i == 0 ? LatestQueuedPostTime : Now.AddDays(random.Next(30));
                        posts.Add(post);
                    }
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }
    }
}