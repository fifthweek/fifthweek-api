namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PostToCollectionDbSubStatementTests : PersistenceTestsBase
    {
        private const int MaxPositionInQueue = 3;

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime TwoDaysAgo = DateTime.UtcNow.AddDays(-2);
        private static readonly DateTime Now = DateTime.UtcNow;
        private PostToCollectionDbSubStatements target;

        // We test for this as nullable strings are not explicitly defined by the language, so this is a good way of checking we've
        // ensured the entity does not require a comment.
        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var inputPost = UnscheduledPostWithoutChannel();
                inputPost.Comment = null;
                await this.target.PostNowAsync(inputPost, Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    null,
                    null,
                    new SqlDateTime(Now).Value,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenPostingNow_ItShouldSchedulePostForNow()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.PostNowAsync(UnscheduledPostWithoutChannel(), Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    Comment.Value,
                    null,
                    new SqlDateTime(Now).Value,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenSchedulingWithDateInPast_ItShouldSchedulePostForNow()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.SchedulePostAsync(UnscheduledPostWithoutChannel(), TwoDaysAgo, Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    Comment.Value,
                    null,
                    new SqlDateTime(Now).Value,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenSchedulingWithDateInFuture_ItShouldSchedulePostForFuture()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.SchedulePostAsync(UnscheduledPostWithoutChannel(), TwoDaysFromNow, Now);

                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    Comment.Value,
                    null,
                    new SqlDateTime(TwoDaysFromNow).Value,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueuingInEmptyQueue_ItShouldTakeZeroPosition()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.QueuePostAsync(UnscheduledPostWithoutChannel());

                const int ExpectedQueuePosition = 0;
                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    Comment.Value,
                    ExpectedQueuePosition,
                    null,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
            });
        }

        [TestMethod]
        public async Task WhenQueuingInNonEmptyQueue_ItShouldTakeNewMaxPosition()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostToCollectionDbSubStatements(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase, createQueuedPosts: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.QueuePostAsync(UnscheduledPostWithoutChannel());

                const int ExpectedQueuePosition = MaxPositionInQueue + 1;
                var expectedPost = new Post(
                    PostId.Value,
                    ChannelId.Value,
                    null,
                    CollectionId.Value,
                    null,
                    null,
                    null,
                    FileId.Value,
                    null,
                    Comment.Value,
                    ExpectedQueuePosition,
                    null,
                    new SqlDateTime(Now).Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedPost
                };
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
                null,
                null,
                Now); 
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool createQueuedPosts = false)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                if (createQueuedPosts)
                {
                    var random = new Random();
                    for (var i = 0; i <= MaxPositionInQueue; i++)
                    {
                        var post = PostTests.UniqueFileOrImage(random);
                        post.ChannelId = ChannelId.Value;
                        post.CollectionId = CollectionId.Value;
                        post.FileId = FileId.Value; // Reuse same file across each post. Not realistic, but doesn't matter for this test.
                        post.QueuePosition = i;
                        post.LiveDate = null;
                        await databaseContext.Database.Connection.InsertAsync(post);
                    }
                }
            }
        }
    }
}