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
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorNewsfeedDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly NonNegativeInt StartIndex = NonNegativeInt.Parse(10);
        private static readonly PositiveInt Count = PositiveInt.Parse(5);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly Comment Comment = new Comment("Hey guys!");
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly string FileName = "FileName";
        private static readonly string FileExtension = "FileExtension";
        private static readonly long FileSize = 1024;
        private static readonly int FileWidth = 800;
        private static readonly int FileHeight = 600;
        private static readonly IReadOnlyList<NewsfeedPost> SortedNewsfeedPosts = GetSortedNewsfeedPosts().ToList();

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        private GetCreatorNewsfeedDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetCreatorNewsfeedDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCreatorId()
        {
            await this.target.ExecuteAsync(null, Now, StartIndex, Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireStartIndex()
        {
            await this.target.ExecuteAsync(UserId, Now, null, Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCount()
        {
            await this.target.ExecuteAsync(UserId, Now, StartIndex, null);
        }

        [TestMethod]
        public async Task ItShouldReturnPostsWithLiveDateInPastOrNow()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    foreach (var newsfeedPost in result)
                    {
                        Assert.IsTrue(newsfeedPost.LiveDate <= Now);
                    }

                    return ExpectedSideEffects.None;
                });
            });
        }

        [TestMethod]
        public async Task ItShouldReturnPostsForUser()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    CollectionAssert.AreEquivalent(expectedPosts.ToList(), result.ToList());

                    return ExpectedSideEffects.None;
                });
            });
        }

        [TestMethod]
        public async Task ItShouldReturnPostsWithLiveDatesAsUtc()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    foreach (var item in result)
                    {
                        Assert.AreEqual(DateTimeKind.Utc, item.LiveDate.Kind);
                    }

                    return ExpectedSideEffects.None;
                });
            });
        }

        [TestMethod]
        public async Task ItShouldOrderPostsByLiveDateDescending()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    Func<NewsfeedPost, NewsfeedPost> removeSortInsensitiveValues = post => post.Copy(_ =>
                    {
                        // Required fields. Set to a value that is equal across all elements.
                        _.PostId = new PostId(Guid.Empty);
                        _.ChannelId = new ChannelId(Guid.Empty);

                        // Non required fields.
                        _.Comment = null;
                        _.FileId = null;
                        _.FileName = null;
                        _.FileExtension = null;
                        _.FileSize = null;
                        _.ImageId = null;
                        _.ImageName = null;
                        _.ImageExtension = null;
                        _.ImageSize = null;
                        _.ImageRenderWidth = null;
                        _.ImageRenderHeight = null;
                        _.CollectionId = null;
                    });

                    var expectedOrder = expectedPosts.Select(removeSortInsensitiveValues).ToList();
                    var actualOrder = result.Select(removeSortInsensitiveValues).ToList();

                    CollectionAssert.AreEqual(expectedOrder, actualOrder);

                    return ExpectedSideEffects.None;
                });
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPosts()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: false, createFuturePosts: false);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    Assert.AreEqual(result.Count, 0);

                    return ExpectedSideEffects.None;
                });
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPostsWithLiveDateInPastOrNow()
        {
            await this.ParameterizedTestAsync(async (creatorId, startIndex, count, expectedPosts) =>
            {
                await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetCreatorNewsfeedDbStatement(testDatabase);
                    await this.CreateEntitiesAsync(testDatabase, createLivePosts: false, createFuturePosts: true);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(creatorId, Now, startIndex, count);

                    Assert.AreEqual(result.Count, 0);

                    return ExpectedSideEffects.None;
                });
            });
        }

        private async Task ParameterizedTestAsync(Func<UserId, NonNegativeInt, PositiveInt, IReadOnlyList<NewsfeedPost>, Task> parameterizedTest)
        {
            var totalPosts = SortedNewsfeedPosts.Count;

            // No pagination.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(0), PositiveInt.Parse(int.MaxValue), SortedNewsfeedPosts);

            // Paginate from start.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(0), PositiveInt.Parse(10), SortedNewsfeedPosts.Take(10).ToList());

            // Paginate from middle with different page size and page index.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(5), PositiveInt.Parse(10), SortedNewsfeedPosts.Skip(5).Take(10).ToList());

            // Paginate from middle with same page size and page index.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(10), PositiveInt.Parse(10), SortedNewsfeedPosts.Skip(10).Take(10).ToList());

            // Paginate from near end, requesting up to last post.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(totalPosts - 10), PositiveInt.Parse(10), SortedNewsfeedPosts.Skip(totalPosts - 10).Take(10).ToList());

            // Paginate from near end, requesting beyond last post.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(totalPosts - 5), PositiveInt.Parse(10), SortedNewsfeedPosts.Skip(totalPosts - 5).Take(10).ToList());

            // Paginate from end, requesting beyond last post.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(totalPosts), PositiveInt.Parse(1), new NewsfeedPost[0]);

            // Paginate from beyond end, requesting beyond last post.
            await parameterizedTest(
                UserId, NonNegativeInt.Parse(totalPosts + 1), PositiveInt.Parse(1), new NewsfeedPost[0]);
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createLivePosts, bool createFuturePosts)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var channels = new Dictionary<ChannelId, List<CollectionId>>();
                var files = new List<FileId>();
                var images = new List<FileId>();
                var channelEntities = new List<Channel>();
                var collectionEntities = new List<Collection>();
                var postEntities = new List<Post>();

                if (createFuturePosts)
                {
                    var channelId = new ChannelId(Guid.NewGuid());
                    var collectionId = new CollectionId(Guid.NewGuid());
                    channels.Add(channelId, new List<CollectionId>(new[] { collectionId }));

                    for (var i = 1; i <= 10; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(Random);
                        post.ChannelId = channelId.Value;
                        post.CollectionId = collectionId.Value;
                        post.LiveDate = Now.AddDays(i + 1);
                        postEntities.Add(post);
                    }
                }

                if (createLivePosts)
                {
                    foreach (var newsfeedPost in SortedNewsfeedPosts)
                    {
                        if (newsfeedPost.FileId != null)
                        {
                            files.Add(newsfeedPost.FileId);
                        }

                        if (newsfeedPost.ImageId != null)
                        {
                            images.Add(newsfeedPost.ImageId);
                        }

                        if (!channels.ContainsKey(newsfeedPost.ChannelId))
                        {
                            channels.Add(newsfeedPost.ChannelId, new List<CollectionId>());
                        }

                        if (newsfeedPost.CollectionId != null)
                        {
                            channels[newsfeedPost.ChannelId].Add(newsfeedPost.CollectionId);
                        }

                        postEntities.Add(new Post(
                            newsfeedPost.PostId.Value,
                            newsfeedPost.ChannelId.Value,
                            null,
                            newsfeedPost.CollectionId == null ? (Guid?)null : newsfeedPost.CollectionId.Value,
                            null,
                            newsfeedPost.FileId == null ? (Guid?)null : newsfeedPost.FileId.Value,
                            null,
                            newsfeedPost.ImageId == null ? (Guid?)null : newsfeedPost.ImageId.Value,
                            null,
                            newsfeedPost.Comment == null ? null : newsfeedPost.Comment.Value,
                            Random.Next(2) == 0,
                            newsfeedPost.LiveDate,
                            Now));
                    }
                }

                foreach (var channelKvp in channels)
                {
                    var channelId = channelKvp.Key;

                    var channel = ChannelTests.UniqueEntity(Random);
                    channel.Id = channelId.Value;
                    channel.BlogId = BlogId.Value;

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
                    file.FileNameWithoutExtension = FileName;
                    file.FileExtension = FileExtension;
                    file.BlobSizeBytes = FileSize;
                    return file;
                });

                var imageEntities = images.Select(fileId =>
                {
                    var file = FileTests.UniqueEntity(Random);
                    file.Id = fileId.Value;
                    file.UserId = UserId.Value;
                    file.FileNameWithoutExtension = FileName;
                    file.FileExtension = FileExtension;
                    file.BlobSizeBytes = FileSize;
                    file.RenderWidth = FileWidth;
                    file.RenderHeight = FileHeight;
                    return file;
                });

                await databaseContext.CreateTestSubscriptionAsync(UserId.Value, BlogId.Value);
                await databaseContext.Database.Connection.InsertAsync(channelEntities);
                await databaseContext.Database.Connection.InsertAsync(collectionEntities);
                await databaseContext.Database.Connection.InsertAsync(fileEntities);
                await databaseContext.Database.Connection.InsertAsync(imageEntities);
                await databaseContext.Database.Connection.InsertAsync(postEntities);
            }
        }

        private static IEnumerable<NewsfeedPost> GetSortedNewsfeedPosts()
        {
            // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
            const int ChannelCount = 3;
            const int CollectionsPerChannel = 3;
            const int Posts = 6;

            var day = 0;
            var result = new List<NewsfeedPost>();
            for (var channelIndex = 0; channelIndex < ChannelCount; channelIndex++)
            {
                var channelId = new ChannelId(Guid.NewGuid());
                for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
                {
                    var collectionId = collectionIndex == 0 ? null : new CollectionId(Guid.NewGuid());
                    for (var i = 0; i < Posts; i++)
                    {
                        // Ensure we have one post that is `now` (i.e. AddDays(0)).
                        var liveDate = new SqlDateTime(Now.AddDays(day--)).Value;

                        result.Add(
                        new NewsfeedPost(
                            new PostId(Guid.NewGuid()),
                            channelId,
                            collectionId,
                            i % 2 == 0 ? Comment : null,
                            i % 3 == 1 ? new FileId(Guid.NewGuid()) : null,
                            i % 3 == 2 ? new FileId(Guid.NewGuid()) : null,
                            liveDate,
                            i % 3 == 1 ? FileName : null,
                            i % 3 == 1 ? FileExtension : null,
                            i % 3 == 1 ? FileSize : (long?)null,
                            i % 3 == 2 ? FileName : null,
                            i % 3 == 2 ? FileExtension : null,
                            i % 3 == 2 ? FileSize : (long?)null,
                            i % 3 == 2 ? FileWidth : (int?)null,
                            i % 3 == 2 ? FileHeight : (int?)null));
                    }
                }
            }

            return result.OrderByDescending(_ => _.LiveDate);
        }
    }
}