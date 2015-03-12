namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Api.Subscriptions.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    public class GetCreatorBacklogDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly IReadOnlyList<BacklogPost> SortedBacklogPosts = GetSortedBacklogPosts().ToList();

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        private GetCreatorBacklogDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetCreatorBacklogDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCreatorId()
        {
            await this.target.ExecuteAsync(null, Now);
        }

        [TestMethod]
        public async Task ItShouldReturnPostsWithLiveDateInFuture()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorBacklogDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, Now);

                foreach (var backlogPost in result)
                {
                    Assert.IsTrue(backlogPost.LiveDate > Now);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnPostsForUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorBacklogDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, Now);

                CollectionAssert.AreEquivalent(SortedBacklogPosts.ToList(), result.ToList());

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldOrderPostsByLiveDateDescending_ThenByQueueMechanism()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorBacklogDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, Now);

                Func<BacklogPost, BacklogPost> removeSortInsensitiveValues = post => post.Copy(_ =>
                {
                    // Required fields. Set to a value that is equal across all elements.
                    _.PostId = new PostId(Guid.Empty);
                    _.ChannelId = new ChannelId(Guid.Empty);

                    // Non required fields.
                    _.Comment = null;
                    _.FileId = null;
                    _.ImageId = null;
                    _.CollectionId = null;
                });

                var expectedOrder = SortedBacklogPosts.Select(removeSortInsensitiveValues).ToList();
                var actualOrder = result.Select(removeSortInsensitiveValues).ToList();

                CollectionAssert.AreEqual(expectedOrder, actualOrder);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPosts()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorBacklogDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: false, createFuturePosts: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, Now);

                Assert.AreEqual(result.Count, 0);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPostsWithLiveDateInFuture()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorBacklogDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, Now);

                Assert.AreEqual(result.Count, 0);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createLivePosts, bool createFuturePosts)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var channels = new Dictionary<ChannelId, List<CollectionId>>();
                var files = new List<FileId>();
                var channelEntities = new List<Channel>();
                var collectionEntities = new List<Collection>();
                var postEntities = new List<Post>();

                if (createLivePosts)
                {
                    var channelId = new ChannelId(Guid.NewGuid());
                    var collectionId = new CollectionId(Guid.NewGuid());
                    channels.Add(channelId, new List<CollectionId>(new[] { collectionId }));

                    for (var i = 1; i <= 10; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(Random);
                        post.ChannelId = channelId.Value;
                        post.CollectionId = collectionId.Value;
                        post.LiveDate = DateTime.UtcNow.AddDays(i * -1);
                        postEntities.Add(post);
                    }
                }

                if (createFuturePosts)
                {
                    foreach (var backlogPost in SortedBacklogPosts)
                    {
                        if (backlogPost.FileId != null)
                        {
                            files.Add(backlogPost.FileId);
                        }

                        if (backlogPost.ImageId != null)
                        {
                            files.Add(backlogPost.ImageId);
                        }

                        if (!channels.ContainsKey(backlogPost.ChannelId))
                        {
                            channels.Add(backlogPost.ChannelId, new List<CollectionId>());
                        }

                        if (backlogPost.CollectionId != null)
                        {
                            channels[backlogPost.ChannelId].Add(backlogPost.CollectionId);
                        }

                        postEntities.Add(new Post(
                            backlogPost.PostId.Value,
                            backlogPost.ChannelId.Value,
                            null,
                            backlogPost.CollectionId == null ? (Guid?)null : backlogPost.CollectionId.Value,
                            null,
                            backlogPost.FileId == null ? (Guid?)null : backlogPost.FileId.Value,
                            null,
                            backlogPost.ImageId == null ? (Guid?)null : backlogPost.ImageId.Value,
                            null,
                            backlogPost.Comment == null ? null : backlogPost.Comment.Value,
                            backlogPost.ScheduledByQueue,
                            backlogPost.LiveDate,
                            Now));
                    }
                }

                foreach (var channelKvp in channels)
                {
                    var channelId = channelKvp.Key;

                    var channel = ChannelTests.UniqueEntity(Random);
                    channel.Id = channelId.Value;
                    channel.SubscriptionId = SubscriptionId.Value;

                    channelEntities.Add(channel);

                    foreach (var collectionId in channelKvp.Value)
                    {
                        var collection = CollectionTests.UniqueEntity(Random);
                        collection.Id = collectionId.Value;
                        collection.ChannelId = channelId.Value;

                        collectionEntities.Add(collection);
                    }
                }

                var fileEntities = files.Select(fileId =>
                {
                    var file = FileTests.UniqueEntity(Random);
                    file.Id = fileId.Value;
                    file.UserId = UserId.Value;
                    return file;
                });

                await databaseContext.CreateTestSubscriptionAsync(UserId.Value, SubscriptionId.Value);
                await databaseContext.Database.Connection.InsertAsync(channelEntities);
                await databaseContext.Database.Connection.InsertAsync(collectionEntities);
                await databaseContext.Database.Connection.InsertAsync(fileEntities);
                await databaseContext.Database.Connection.InsertAsync(postEntities);
            }
        }

        private static IEnumerable<BacklogPost> GetSortedBacklogPosts()
        {
            // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
            const int Days = 3;
            const int ChannelCount = 3;
            const int CollectionsPerChannel = 3;
            const int Posts = 6;

            var result = new List<BacklogPost>();
            for (var day = 0; day < Days; day++)
            {
                var liveDate = new SqlDateTime(Now.AddDays(1 + day)).Value;
                for (var channelIndex = 0; channelIndex < ChannelCount; channelIndex++)
                {
                    var channelId = new ChannelId(Guid.NewGuid());
                    for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
                    {
                        var collectionId = collectionIndex == 0 ? null : new CollectionId(Guid.NewGuid());
                        for (var i = 0; i < Posts; i++)
                        {
                            result.Add(
                            new BacklogPost(
                                new PostId(Guid.NewGuid()),
                                channelId,
                                collectionId,
                                i % 2 == 0 ? Comment : null,
                                i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                                i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                                i % 2 == 0,
                                liveDate));
                        }
                    }
                }
            }

            return result.OrderByDescending(_ => _.LiveDate).ThenByDescending(_ => _.ScheduledByQueue);
        }
    }
}