namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CountQueuedPostsInCollectionDbStatementTests : PersistenceTestsBase
    {
        private const int QueuedPostCount = 5;
        private const int UnqueuedPostCount = 8;

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());

        private CountQueuedPostsInCollectionDbStatement target;

        [TestMethod]
        public async Task WhenPostsHaveBeenQueued_ItShouldReturnNumberOfPostsQueued()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new CountQueuedPostsInCollectionDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, createQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId);

                Assert.AreEqual(result, QueuedPostCount);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoPostsHaveBeenQueued_ItShouldReturnZero()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new CountQueuedPostsInCollectionDbStatement(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, createQueuedPosts: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId);

                Assert.AreEqual(result, 0);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenNoPostsExist_ItShouldReturnZero()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new CountQueuedPostsInCollectionDbStatement(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CollectionId);

                Assert.AreEqual(result, 0);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createQueuedPosts)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var random = new Random();
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);

                var posts = new List<Post>();
                for (var i = 0; i < UnqueuedPostCount; i++)
                {
                    var post = PostTests.UniqueFileOrImage(random);
                    post.ChannelId = ChannelId.Value;
                    post.CollectionId = CollectionId.Value;
                    post.QueuePosition = null;
                    posts.Add(post);
                }

                if (createQueuedPosts)
                {
                    for (var i = 0; i < QueuedPostCount; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.CollectionId = CollectionId.Value;
                        post.QueuePosition = random.Next(100);
                        posts.Add(post);
                    }
                }

                await databaseContext.Database.Connection.InsertAsync(posts);
            }
        }
    }
}