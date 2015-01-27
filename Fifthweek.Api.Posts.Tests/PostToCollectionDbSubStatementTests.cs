namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

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
        private Mock<IFifthweekDbContext> databaseContext;
        private PostToCollectionDbSubStatements target;

        [TestInitialize]
        public void Initialize()
        {
            this.getLiveDateOfNewQueuedPost =  new Mock<IGetLiveDateOfNewQueuedPostDbStatement>();
            
            // Give potentially side-effecting components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.target = new PostToCollectionDbSubStatements(this.databaseContext.Object, this.getLiveDateOfNewQueuedPost.Object);
        }

        // We test for this as nullable strings are not explicitly defined by the language, so this is a good way of checking we've
        // ensured the entity does not require a comment.
        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                givenPost.Comment = null;
                await this.target.PostNowAsync(givenPost, Now);

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
        public async Task WhenPostingNow_ItShouldSchedulePostForNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.PostNowAsync(givenPost, Now);

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
        public async Task WhenPostingNow_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var givenPost = UnscheduledPostWithoutChannel();

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.PostNowAsync(givenPost, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.PostNowAsync(givenPost, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenSchedulingWithDateInPast_ItShouldSchedulePostForNow()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var pastScheduleDate = DateTime.UtcNow.AddDays(-2);

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.SchedulePostAsync(givenPost, pastScheduleDate, Now);

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
        public async Task WhenSchedulingWithDateInFuture_ItShouldSchedulePostForFuture()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var futureScheduleDate = DateTime.UtcNow.AddDays(2);

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var givenPost = UnscheduledPostWithoutChannel();
                await this.target.SchedulePostAsync(givenPost, futureScheduleDate, Now);

                var expectedPost = givenPost.Copy(_ =>
                {
                    _.ChannelId = ChannelId.Value;
                    _.LiveDate = new SqlDateTime(futureScheduleDate).Value;
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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

                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext(), this.getLiveDateOfNewQueuedPost.Object);
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