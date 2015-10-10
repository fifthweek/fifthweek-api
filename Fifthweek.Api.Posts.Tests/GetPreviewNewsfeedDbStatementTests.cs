namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class GetPreviewNewsfeedDbStatementTests : PersistenceTestsBase
    {
        // 1 in 3 chance of coincidental ordering being correct, yielding a false positive when implementation fails to order explicitly.
        const int ChannelsPerCreator = 3;
        const int CollectionsPerChannel = 3;
        const int Posts = 6;
        
        private static readonly UserId UnsubscribedUserId = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly List<UserId> UserIds = new List<UserId>
        {
            UnsubscribedUserId,
            CreatorId,
        };
        private static readonly List<ChannelId> ChannelIds;
        private static readonly List<List<QueueId>> CollectionIds;
        private static readonly NonNegativeInt StartIndex = NonNegativeInt.Parse(10);
        private static readonly PositiveInt Count = PositiveInt.Parse(5);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly Comment Comment = new Comment(new string(Enumerable.Repeat<char>('x', 1000).ToArray()));
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly string FileName = "FileName";
        private static readonly string FileExtension = "FileExtension";
        private static readonly long FileSize = 1024;
        private static readonly int FileWidth = 800;
        private static readonly int FileHeight = 600;
        private static readonly int ChannelPrice = 10;
        private static readonly IReadOnlyList<NewsfeedPost> SortedNewsfeedPosts;
        private static readonly IReadOnlyList<NewsfeedPost> SortedLiveNewsfeedPosts;

        static GetPreviewNewsfeedDbStatementTests()
        {
            ChannelIds = new List<ChannelId>();
            CollectionIds = new List<List<QueueId>>();
            for (int i = 0; i < ChannelsPerCreator; i++)
            {
                ChannelIds.Add(new ChannelId(Guid.NewGuid()));

                var collectionIds = new List<QueueId>();
                CollectionIds.Add(collectionIds);

                for (int j = 0; j < CollectionsPerChannel; j++)
                {
                    // Null colleciton is for notes.
                    collectionIds.Add(j == 0 ? null : new QueueId(Guid.NewGuid()));
                }
            }

            SortedNewsfeedPosts = GetSortedNewsfeedPosts().ToList();
            SortedLiveNewsfeedPosts = SortedNewsfeedPosts.Where(v => v.LiveDate <= Now).ToList();
        }

        private readonly Random random = new Random();
        
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;

        private GetPreviewNewsfeedDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new GetPreviewNewsfeedDbStatement(this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireStartIndex()
        {
            await this.target.ExecuteAsync(UnsubscribedUserId, CreatorId, ChannelIds, Now, Now, false, null, Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCount()
        {
            await this.target.ExecuteAsync(UnsubscribedUserId, CreatorId, ChannelIds, Now, Now, false, StartIndex, null);
        }

        [TestMethod]
        public async Task ItShouldReturnPostsWithLiveDateInPastOrNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);

                    foreach (var newsfeedPost in result.Posts)
                    {
                        Assert.IsTrue(newsfeedPost.LiveDate <= Now);
                    }
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnPostsForUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);

                    CollectionAssert.AreEquivalent(expectedPosts.ToList(), result.Posts.ToList());
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnPostsWithDatesAsUtc()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);

                    foreach (var item in result.Posts)
                    {
                        Assert.AreEqual(DateTimeKind.Utc, item.LiveDate.Kind);
                        Assert.AreEqual(DateTimeKind.Utc, item.CreationDate.Kind);
                    }
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldOrderPostsCorrectly()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: true, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);

                    Func<NewsfeedPost, NewsfeedPost> removeSortInsensitiveValues = post => post.Copy(_ =>
                    {
                        // Required fields. Set to a value that is equal across all elements.
                        _.PostId = new PostId(Guid.Empty);
                        _.ChannelId = new ChannelId(Guid.Empty);
                        _.CreatorId = new UserId(Guid.Empty);

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
                    });

                    var expectedOrder = expectedPosts.Select(removeSortInsensitiveValues).ToList();
                    var actualOrder = result.Posts.Select(removeSortInsensitiveValues).ToList();

                    CollectionAssert.AreEqual(expectedOrder, actualOrder);
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPosts()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: false, createFuturePosts: false);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);
                    Assert.AreEqual(result.Posts.Count, 0);
                });

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNothingWhenUserHasNoPostsWithLiveDateInPastOrNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetPreviewNewsfeedDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, createLivePosts: false, createFuturePosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.ParameterizedTestAsync(async (userId, creatorId, channelIds, collectionIds, origin, searchForwards, startIndex, count, expectedPosts, expectedAccountBalance) =>
                {
                    var result = await this.target.ExecuteAsync(userId, creatorId, channelIds, Now, origin, searchForwards, startIndex, count);
                    Assert.AreEqual(result.Posts.Count, 0);
                });

                return ExpectedSideEffects.None;
            });
        }

        private async Task ParameterizedTestAsync(
            Func<UserId, 
                 UserId, 
                 IReadOnlyList<ChannelId>,
                 IReadOnlyList<QueueId>, 
                 DateTime,
                 bool,
                 NonNegativeInt,
                 PositiveInt,
                 IReadOnlyList<NewsfeedPost>,
                 int,
                 Task> parameterizedTest)
        {
            var totalLivePosts = SortedLiveNewsfeedPosts.Count;

            NonNegativeInt noPaginationStart = NonNegativeInt.Parse(0);
            PositiveInt noPaginationCount = PositiveInt.Parse(int.MaxValue);

            var wrapper = new ParameterizedTestWrapper(parameterizedTest);
            parameterizedTest = wrapper.Execute;

            // No pagination.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts, 0);

            // Paginate from start.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(0), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Take(10).ToList(), 0);

            // Paginate from middle with different page size and page index.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(5), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Skip(5).Take(10).ToList(), 0);

            // Paginate from middle with same page size and page index.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(10), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Skip(10).Take(10).ToList(), 0);

            // Paginate from near end, requesting up to last post.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(totalLivePosts - 10), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Skip(totalLivePosts - 10).Take(10).ToList(), 0);

            // Paginate from near end, requesting beyond last post.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(totalLivePosts - 5), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Skip(totalLivePosts - 5).Take(10).ToList(), 0);

            // Paginate from end, requesting beyond last post.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(totalLivePosts), PositiveInt.Parse(1), new NewsfeedPost[0], 0);

            // Paginate from beyond end, requesting beyond last post.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, false, NonNegativeInt.Parse(totalLivePosts + 1), PositiveInt.Parse(1), new NewsfeedPost[0], 0);

            // Unsubscribed.
            await parameterizedTest(
                UnsubscribedUserId, CreatorId, null, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts, 0);

            // Logged out.
            await parameterizedTest(
                null, CreatorId, null, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts, 0);

            // Filter by channel for creator.
            await parameterizedTest(
                CreatorId, CreatorId, new[] { ChannelIds[0] }, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts.Where(v => v.ChannelId.Equals(ChannelIds[0])).ToList(), 0);

            // Filter by channel.
            await parameterizedTest(
                UnsubscribedUserId, null, new[] { ChannelIds[0] }, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts.Where(v => v.ChannelId.Equals(ChannelIds[0])).ToList(), 10);

            // Filter by channel 2.
            await parameterizedTest(
                UnsubscribedUserId, null, new[] { ChannelIds[1] }, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts.Where(v => v.ChannelId.Equals(ChannelIds[1])).ToList(), 10);

            // Filter by valid channel and creator combination.
            await parameterizedTest(
                UnsubscribedUserId, CreatorId, new[] { ChannelIds[0] }, null, Now, false, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts.Where(v => v.ChannelId.Equals(ChannelIds[0])).ToList(), 10);

            // Filter by invalid channel and creator combination.
            await parameterizedTest(
                UnsubscribedUserId, UserId.Random(), new[] { ChannelIds[0] }, null, Now, false, noPaginationStart, noPaginationCount, new NewsfeedPost[0], 10);

            // Search forwards from now.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, Now, true, noPaginationStart, noPaginationCount, new NewsfeedPost[0], 0);

            // Search forwards from beginning.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, SqlDateTime.MinValue.Value, true, noPaginationStart, noPaginationCount, SortedLiveNewsfeedPosts.Reverse().ToList(), 0);

            // Paginate forwards from middle with different page size and page index.
            await parameterizedTest(
                CreatorId, CreatorId, null, null, SqlDateTime.MinValue.Value, true, NonNegativeInt.Parse(5), PositiveInt.Parse(10), SortedLiveNewsfeedPosts.Reverse().Skip(5).Take(10).ToList(), 0);
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createLivePosts, bool createFuturePosts)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await this.CreateUserAsync(databaseContext, UnsubscribedUserId);

                var channelSubscriptions = new List<ChannelSubscription>();
                var calculatedAccountBalances = new List<CalculatedAccountBalance>();
                var freeAccessUsers = new List<FreeAccessUser>();
                
                var channels = new List<ChannelId>();
                var queues = new List<QueueId>();
                var files = new List<FileId>();
                var images = new List<FileId>();
                var channelEntities = new List<Channel>();
                var queueEntities = new List<Queue>();
                var postEntities = new List<Post>();
                var origins = new List<UserPaymentOrigin>();
                var likes = new List<Like>();
                var comments = new List<Persistence.Comment>();

                if (createLivePosts || createFuturePosts)
                {
                    foreach (var newsfeedPost in SortedNewsfeedPosts)
                    {
                        if (createLivePosts && !createFuturePosts && newsfeedPost.LiveDate > Now)
                        {
                            continue;
                        }

                        if (!createLivePosts && createFuturePosts && newsfeedPost.LiveDate <= Now)
                        {
                            continue;
                        }

                        if (newsfeedPost.FileId != null)
                        {
                            files.Add(newsfeedPost.FileId);
                        }

                        if (newsfeedPost.ImageId != null)
                        {
                            images.Add(newsfeedPost.ImageId);
                        }

                        if (!channels.Contains(newsfeedPost.ChannelId))
                        {
                            channels.Add(newsfeedPost.ChannelId);
                        }

                        postEntities.Add(new Post(
                            newsfeedPost.PostId.Value,
                            newsfeedPost.ChannelId.Value,
                            null,
                            (Guid?)null,
                            null,
                            newsfeedPost.FileId == null ? (Guid?)null : newsfeedPost.FileId.Value,
                            null,
                            newsfeedPost.ImageId == null ? (Guid?)null : newsfeedPost.ImageId.Value,
                            null,
                            newsfeedPost.Comment == null ? null : newsfeedPost.Comment.Value,
                            newsfeedPost.LiveDate,
                            newsfeedPost.CreationDate));

                        for (int i = 0; i < newsfeedPost.LikesCount; i++)
                        {
                            likes.Add(
                                new Like(
                                    newsfeedPost.PostId.Value,
                                    null,
                                    UserIds[i].Value,
                                    null,
                                    DateTime.UtcNow));
                        }

                        for (int i = 0; i < newsfeedPost.CommentsCount; i++)
                        {
                            comments.Add(
                                new Persistence.Comment(
                                    Guid.NewGuid(),
                                    newsfeedPost.PostId.Value,
                                    null,
                                    UserIds[i].Value,
                                    null,
                                    "Comment " + this.random.Next(),
                                    DateTime.UtcNow));
                        }
                    }
                }

                foreach (var channelId in channels)
                {
                    var channel = ChannelTests.UniqueEntity(Random);
                    channel.Id = channelId.Value;
                    channel.BlogId = BlogId.Value;
                    channel.Price = ChannelPrice;

                    channelEntities.Add(channel);
                }

                foreach (var queueId in queues)
                {
                    var queue = QueueTests.UniqueEntity(Random);
                    queue.Id = queueId.Value;
                    queue.BlogId = BlogId.Value;

                    queueEntities.Add(queue);
                }

                var fileEntities = files.Select(fileId =>
                {
                    var file = FileTests.UniqueEntity(Random);
                    file.Id = fileId.Value;
                    file.UserId = CreatorId.Value;
                    file.FileNameWithoutExtension = FileName;
                    file.FileExtension = FileExtension;
                    file.BlobSizeBytes = FileSize;
                    return file;
                });

                var imageEntities = images.Select(fileId =>
                {
                    var file = FileTests.UniqueEntity(Random);
                    file.Id = fileId.Value;
                    file.UserId = CreatorId.Value;
                    file.FileNameWithoutExtension = FileName;
                    file.FileExtension = FileExtension;
                    file.BlobSizeBytes = FileSize;
                    file.RenderWidth = FileWidth;
                    file.RenderHeight = FileHeight;
                    return file;
                });

                await databaseContext.CreateTestBlogAsync(CreatorId.Value, BlogId.Value, null, Random);
                await databaseContext.Database.Connection.InsertAsync(channelEntities);
                await databaseContext.Database.Connection.InsertAsync(queueEntities);
                await databaseContext.Database.Connection.InsertAsync(fileEntities);
                await databaseContext.Database.Connection.InsertAsync(imageEntities);
                await databaseContext.Database.Connection.InsertAsync(postEntities);
                await databaseContext.Database.Connection.InsertAsync(channelSubscriptions);
                await databaseContext.Database.Connection.InsertAsync(calculatedAccountBalances);
                await databaseContext.Database.Connection.InsertAsync(freeAccessUsers);
                await databaseContext.Database.Connection.InsertAsync(origins);
                await databaseContext.Database.Connection.InsertAsync(likes);
                await databaseContext.Database.Connection.InsertAsync(comments);
            }
        }

        private async Task CreateUserAsync(FifthweekDbContext databaseContext, UserId userId)
        {
            var user = UserTests.UniqueEntity(this.random);
            user.Id = userId.Value;
            user.Email = userId.Value + "@test.com";

            databaseContext.Users.Add(user);
            await databaseContext.SaveChangesAsync();
        }

        private static IEnumerable<NewsfeedPost> GetSortedNewsfeedPosts()
        {
            // Half the posts will be in the future relative to Now. Days move one day every two posts.
            var day = ChannelsPerCreator * CollectionsPerChannel * Posts / 2;

            var result = new List<NewsfeedPost>();
            for (var channelIndex = 0; channelIndex < ChannelsPerCreator; channelIndex++)
            {
                var channelId = ChannelIds[channelIndex];
                for (var collectionIndex = 0; collectionIndex < CollectionsPerChannel; collectionIndex++)
                {
                    var collectionId = CollectionIds[channelIndex][collectionIndex];
                    for (var i = 0; i < Posts * 2; i++)
                    {
                        DateTime liveDate;
                        DateTime creationDate;
                        if (i % 2 == 0)
                        {
                            // Ensure we have one post that is `now` (i.e. AddDays(0)).
                            liveDate = new SqlDateTime(Now.AddDays(day--)).Value;
                            creationDate = liveDate;
                        }
                        else
                        {
                            liveDate = new SqlDateTime(Now.AddDays(day)).Value;
                            creationDate = new SqlDateTime(liveDate.AddMinutes(-1)).Value;
                        }

                        result.Add(
                        new NewsfeedPost(
                            CreatorId,
                            new PostId(Guid.NewGuid()),
                            BlogId,
                            channelId,
                            i % 3 == 0 ? Comment : null,
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
                            i % 3 == 2 ? FileHeight : (int?)null,
                            1,
                            1,
                            false,
                            creationDate));
                    }
                }
            }

            return result.OrderByDescending(_ => _.LiveDate).ThenByDescending(_ => _.CreationDate);
        }

        private class ParameterizedTestWrapper
        {
            private readonly Func<UserId, UserId, IReadOnlyList<ChannelId>, IReadOnlyList<QueueId>, DateTime, bool, NonNegativeInt, PositiveInt, IReadOnlyList<NewsfeedPost>, int, Task> parameterizedTest;

            public ParameterizedTestWrapper(Func<UserId,
                 UserId,
                 IReadOnlyList<ChannelId>,
                 IReadOnlyList<QueueId>,
                 DateTime,
                 bool,
                 NonNegativeInt,
                 PositiveInt,
                 IReadOnlyList<NewsfeedPost>,
                 int,
                 Task> parameterizedTest)
            {
                this.parameterizedTest = parameterizedTest;
            }

            public Task Execute(
                UserId userId,
                UserId creatorId,
                IReadOnlyList<ChannelId> channelIds,
                IReadOnlyList<QueueId> collectionIds,
                DateTime origin,
                bool searchForwards,
                NonNegativeInt startIndex,
                PositiveInt count,
                IReadOnlyList<NewsfeedPost> expectedPosts,
                int expectedAccountBalance)
            {
                expectedPosts = this.GetPostsForUser(expectedPosts, userId);
                return this.parameterizedTest(
                    userId,
                    creatorId,
                    channelIds,
                    collectionIds,
                    origin,
                    searchForwards,
                    startIndex,
                    count,
                    expectedPosts,
                    expectedAccountBalance);
            }

            private IReadOnlyList<NewsfeedPost> GetPostsForUser(IReadOnlyList<NewsfeedPost> posts, UserId userId)
            {
                return posts.Select(v => new NewsfeedPost(
                    v.CreatorId,
                    v.PostId,
                    v.BlogId,
                    v.ChannelId,
                    v.Comment == null ? null : new Comment(v.Comment.Value.Substring(0, GetPreviewNewsfeedDbStatement.MaxCommentLength.Value)),
                    v.FileId,
                    v.ImageId,
                    v.LiveDate,
                    v.FileName,
                    v.FileExtension,
                    v.FileSize,
                    v.ImageName,
                    v.ImageExtension,
                    v.ImageSize,
                    v.ImageRenderWidth,
                    v.ImageRenderHeight,
                    v.LikesCount,
                    v.CommentsCount,
                    userId != null && UserIds.IndexOf(userId) < v.LikesCount,
                    v.CreationDate)).ToList();
            }
        }
    }
}