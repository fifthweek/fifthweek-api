namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PostToCollectionDbSubStatementTests : PersistenceTestsBase
    {
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
                    default(DateTime),
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        AreEqual = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            expectedPost.LiveDate = actualPost.CreationDate; // Assumes creation date is UtcNow (haven't actually been testing this so far).
                            return Equals(expectedPost, actualPost);
                        }
                    }
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
                    default(DateTime),
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        AreEqual = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            expectedPost.LiveDate = actualPost.CreationDate; // Assumes creation date is UtcNow (haven't actually been testing this so far).
                            return Equals(expectedPost, actualPost);
                        }
                    }
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
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        AreEqual = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return Equals(expectedPost, actualPost);
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenQueuingInEmptyQueue_ItShouldTakeZeroPosition()
        {
            // Assert.Fail(); TODO
        }

        [TestMethod]
        public async Task WhenQueuingInNonEmptyQueue_ItShouldTakeMaxPosition()
        {
            // Assert.Fail(); TODO
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

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, CollectionId.Value);
                await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);
            }
        }
    }
}