namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostToChannelDbSubStatementTests : PersistenceTestsBase
    {
        private const int QueuedPostCount = 3;

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly QueueId DifferentQueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly Random Random = new Random();
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IGetLiveDateOfNewQueuedPostDbStatement> getLiveDateOfNewQueuedPost;
        private Mock<IScheduledDateClippingFunction> scheduledDateClipping;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private PostToChannelDbSubStatements target;

        [TestInitialize]
        public void Initialize()
        {
            this.getLiveDateOfNewQueuedPost = new Mock<IGetLiveDateOfNewQueuedPostDbStatement>();
            this.scheduledDateClipping = new Mock<IScheduledDateClippingFunction>();

            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new PostToChannelDbSubStatements(connectionFactory, this.getLiveDateOfNewQueuedPost.Object, this.scheduledDateClipping.Object);
        }

        // We test for this as nullable strings are not explicitly defined by the language, so this is a good way of checking we've
        // ensured the entity does not require a comment.
        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();
                this.scheduledDateClipping.Setup(_ => _.Apply(Now, Now)).Returns(Now);

                var givenPost = UnscheduledPost();
                givenPost.Comment = null;

                await this.target.SchedulePostAsync(givenPost, Now, Now);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(Now).Value;
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

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPost();

                await this.target.SchedulePostAsync(givenPost, scheduleDate, Now);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(clippedDate).Value;
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
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(uniqueLiveDate);

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPost();
                givenPost.QueueId = QueueId.Value;
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(uniqueLiveDate).Value;
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
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(sharedLiveDate);

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: true, differentCollection: true);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPost();
                givenPost.QueueId = QueueId.Value;
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(sharedLiveDate).Value;
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
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(sharedLiveDate);

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: false, differentCollection: false);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPost();
                givenPost.QueueId = QueueId.Value;
                await this.target.QueuePostAsync(givenPost);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(sharedLiveDate).Value;
                });

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueueing_AndCalculatedLiveDateMatchesAnotherQueuedPostInTheCollection_ItShouldThrowOptimisticConcurrencyException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var sharedLiveDate = DateTime.UtcNow.AddDays(42);
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(sharedLiveDate);

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.CreatePostAsync(testDatabase, sharedLiveDate, scheduledByQueue: true, differentCollection: false);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPost();
                givenPost.QueueId = QueueId.Value;
                await ExpectedException.AssertExceptionAsync<OptimisticConcurrencyException>(() =>
                {
                    return this.target.QueuePostAsync(givenPost);            
                });
                
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
                this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsInOrderAsync(firstUniqueDate, secondUniqueDate);

                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
          
                var givenPost = UnscheduledPost();
                givenPost.QueueId = QueueId.Value;
         
                await this.target.QueuePostAsync(givenPost);
                await testDatabase.TakeSnapshotAsync();
                await this.target.QueuePostAsync(givenPost);

                return ExpectedSideEffects.None;
            });
        }

        private static Post UnscheduledPost()
        {
            return new Post(
                PostId.Value,
                ChannelId.Value, 
                null,
                null,
                null,
                null,
                null,
                FileId.Value,
                null,
                Comment.Value,
                default(DateTime),
                new SqlDateTime(Now).Value); 
        }

        private async Task CreatePostAsync(TestDatabaseContext testDatabase, DateTime liveDate, bool scheduledByQueue, bool differentCollection)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                if (differentCollection)
                {
                    var collection = QueueTests.UniqueEntity(Random);
                    collection.Id = DifferentQueueId.Value;
                    collection.BlogId = BlogId.Value;
                    await databaseContext.Database.Connection.InsertAsync(collection);
                }

                var post = PostTests.UniqueFileOrImage(Random);
                post.ChannelId = ChannelId.Value;
                post.QueueId = scheduledByQueue ? (differentCollection ? DifferentQueueId.Value : QueueId.Value) : (Guid?)null;
                post.FileId = FileId.Value; // Reuse same file across each post. Not realistic, but doesn't matter for this test.
                post.LiveDate = liveDate;
                await databaseContext.Database.Connection.InsertAsync(post);
            }
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createQueuedPosts = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestEntitiesAsync(UserId.Value, ChannelId.Value, QueueId.Value, BlogId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                if (createQueuedPosts)
                {
                    for (var i = 0; i <= QueuedPostCount; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(Random);
                        post.ChannelId = ChannelId.Value;
                        post.QueueId = QueueId.Value;
                        post.FileId = FileId.Value; // Reuse same file across each post. Not realistic, but doesn't matter for this test.
                        post.LiveDate = DateTime.UtcNow.AddDays(i);
                        await databaseContext.Database.Connection.InsertAsync(post);
                    }
                }
            }
        }
    }
}