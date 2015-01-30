namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostToCollectionDbSubStatementTests : PersistenceTestsBase
    {
        private const int QueuedPostCount = 3;

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly CollectionId DifferentCollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IGetLiveDateOfNewQueuedPostDbStatement> getLiveDateOfNewQueuedPost;
        private Mock<IScheduledDateClippingFunction> scheduledDateClipping;
        private Mock<IFifthweekDbContext> databaseContext;
        private PostToCollectionDbSubStatements target;

        [TestInitialize]
        public void Initialize()
        {
            this.getLiveDateOfNewQueuedPost = new Mock<IGetLiveDateOfNewQueuedPostDbStatement>();
            this.scheduledDateClipping = new Mock<IScheduledDateClippingFunction>();

            // Give potentially side-effecting components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new PostToCollectionDbSubStatements(databaseContext, this.getLiveDateOfNewQueuedPost.Object, this.scheduledDateClipping.Object);
        }

        // We test for this as nullable strings are not explicitly defined by the language, so this is a good way of checking we've
        // ensured the entity does not require a comment.
        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();
                this.scheduledDateClipping.Setup(_ => _.Apply(Now, Now)).Returns(Now);

                var givenPost = UnscheduledPostWithoutChannel();
                givenPost.Comment = null;

                await this.target.SchedulePostAsync(givenPost, Now, Now);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(Now).Value;
                    _.ScheduledByQueue = false;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenScheduling_ItShouldSchedulePostUsingDateClippingFunction()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var scheduleDate = Now.AddDays(-2);
                var clippedDate = Now.AddDays(1);
                this.scheduledDateClipping.Setup(_ => _.Apply(Now, scheduleDate)).Returns(clippedDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();

                await this.target.SchedulePostAsync(givenPost, scheduleDate, Now);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(clippedDate).Value;
                    _.ScheduledByQueue = false;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_AndCalculatedLiveDateIsUnique_ItShouldQueueThePost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var uniqueLiveDate = DateTime.UtcNow.AddDays(42);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(uniqueLiveDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(uniqueLiveDate).Value;
                    _.ScheduledByQueue = true;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_AndCalculatedLiveDateMatchesAnotherQueuedPostInDifferentCollection_ItShouldQueueThePost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var sharedLiveDate = DateTime.UtcNow.AddDays(42);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(sharedLiveDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: true, differentCollection: true);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(sharedLiveDate).Value;
                    _.ScheduledByQueue = true;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_AndCalculatedLiveDateMatchesAnotherScheduledPostInTheCollection_ItShouldQueueThePost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var sharedLiveDate = DateTime.UtcNow.AddDays(42);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(sharedLiveDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: false, differentCollection: false);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(sharedLiveDate).Value;
                    _.ScheduledByQueue = true;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_AndCalculatedLiveDateMatchesAnotherQueuedPostInTheCollection_ItShouldRetryWithDifferentDate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var sharedLiveDate = DateTime.UtcNow.AddDays(42);
                var uniqueLiveDate = DateTime.UtcNow.AddDays(43);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsInOrderAsync(sharedLiveDate, uniqueLiveDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: true, differentCollection: false);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(uniqueLiveDate).Value;
                    _.ScheduledByQueue = true;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_ItShouldRetryForMaximumOf3TimesBeforeThrowingException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                const int MaxNumberOfRetries = 3;

                // We keep returning the same live date to simulate a recurring collision. Under a normal race condition, the date would increment
                // forward. In the unlikely event that a new post is independently queued for each of the 3 times we calculate the next queue date, 
                // the operation should fail. Using the same date and a single contending post is a simpler way of recreating this condition.
                var sharedLiveDate = DateTime.UtcNow.AddDays(42);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(sharedLiveDate).Verifiable();

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: true, differentCollection: false);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();

                await ExpectedException.AssertExceptionAsync<Exception>(() =>
                {
                    return this.target.QueuePostAsync(givenPost);
                });

                this.getLiveDateOfNewQueuedPost.Verify(_ => _.ExecuteAsync(CollectionId), Times.Exactly(MaxNumberOfRetries));

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenQueueing_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var firstUniqueDate = DateTime.UtcNow.AddDays(42);
                var secondUniqueDate = DateTime.UtcNow.AddDays(43);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsInOrderAsync(firstUniqueDate, secondUniqueDate);

                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.QueuePostAsync(UnscheduledPostWithoutChannel());
                await testDatabase.TakeSnapshotAsync();

                await this.target.QueuePostAsync(UnscheduledPostWithoutChannel());

                return ExpectedSideEffects.None;
            });
        }

        private static Post UnscheduledPostWithoutChannel()
        {
            return new Post(
                PostId.Value,
                default(Guid), 
                null,
                CollectionId.Value,
                null,
                null,
                null,
                FileId.Value,
                null,
                Comment.Value,
                false,
                default(DateTime),
                new SqlDateTime(Now).Value); 
        }

        private async Task CreatePostAsync(TestDatabaseContext testDatabase, DateTime liveDate, bool scheduledByQueue, bool differentCollection)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                if (differentCollection)
                {
                    var collection = CollectionTests.UniqueEntity(Random);
                    collection.Id = DifferentCollectionId.Value;
                    collection.ChannelId = ChannelId.Value;
                    await databaseContext.Database.Connection.InsertAsync(collection);
                }

                var post = PostTests.UniqueFileOrImage(Random);
                post.ChannelId = ChannelId.Value;
                post.CollectionId = differentCollection ? DifferentCollectionId.Value : CollectionId.Value;
                post.FileId = FileId.Value; // Reuse same file across each post. Not realistic, but doesn't matter for this test.
                post.ScheduledByQueue = scheduledByQueue;
                post.LiveDate = liveDate;
                await databaseContext.Database.Connection.InsertAsync(post);
            }
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createQueuedPosts = false)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                if (createQueuedPosts)
                {
                    for (var i = 0; i <= QueuedPostCount; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(Random);
                        post.ChannelId = ChannelId.Value;
                        post.CollectionId = CollectionId.Value;
                        post.FileId = FileId.Value; // Reuse same file across each post. Not realistic, but doesn't matter for this test.
                        post.ScheduledByQueue = true;
                        post.LiveDate = DateTime.UtcNow.AddDays(i);
                        await databaseContext.Database.Connection.InsertAsync(post);
                    }
                }
            }
        }
    }
}