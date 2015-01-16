namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostImageCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime TwoDaysAgo = DateTime.UtcNow.AddDays(-2);
        private static readonly PostImageCommand ImmediatePostCommand = new PostImageCommand(UserId, PostId, CollectionId, FileId, Comment, null, false);
        private static readonly PostImageCommand Command = ImmediatePostCommand; // Treat this as our canonical testing command.
        private Mock<ICollectionSecurity> collectionSecurity;
        private PostImageCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostImageCommandHandler(this.collectionSecurity.Object, testDatabase.NewContext());
                this.collectionSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();
                await testDatabase.TakeSnapshotAsync();

                try
                {
                    await this.target.HandleAsync(Command);
                    Assert.Fail("Expected recoverable exception");
                }
                catch (UnauthorizedException)
                {
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostImageCommandHandler(this.collectionSecurity.Object, testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInFuture_ItShouldSchedulePostForLater()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostImageCommandHandler(this.collectionSecurity.Object, testDatabase.NewContext());
                var scheduledPostCommand = new PostImageCommand(UserId, PostId, CollectionId, FileId, Comment, TwoDaysFromNow, false);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(scheduledPostCommand);

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
        public async Task WhenDateIsProvidedAndIsInPast_ItShouldSchedulePostForNow()
        {
            var misscheduledPostCommand = new PostImageCommand(UserId, PostId, CollectionId, FileId, Comment, TwoDaysAgo, false);
            await this.ItShouldSchedulePostForNow(misscheduledPostCommand);
        }

        [TestMethod]
        public async Task WhenDateIsNotProvided_ItShouldSchedulePostForNow()
        {
            await this.ItShouldSchedulePostForNow(ImmediatePostCommand);
        }

        private async Task ItShouldSchedulePostForNow(PostImageCommand command)
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostImageCommandHandler(this.collectionSecurity.Object, testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(command);

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