namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;

    [TestClass]
    public class PostToCollectionDbStatementTests
    {
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Post UncommentedImage = new Post(
                PostId.Value,
                default(Guid),
                null,
                CollectionId.Value,
                null,
                null,
                null,
                FileId.Value,
                null,
                null,
                false,
                default(DateTime),
                Now);

        private static readonly Post CommentedImage = new Post(
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
                Now);

        private static readonly Post CommentedFile = new Post(
                PostId.Value,
                default(Guid),
                null,
                CollectionId.Value,
                null,
                FileId.Value,
                null,
                null,
                null,
                Comment.Value,
                false,
                default(DateTime),
                Now);

        private Mock<IPostToCollectionDbSubStatements> subStatements;
        private PostToCollectionDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.subStatements = new Mock<IPostToCollectionDbSubStatements>(MockBehavior.Strict);

            this.target = new PostToCollectionDbStatement(this.subStatements.Object);
        }

        [TestMethod]
        public async Task ItShouldAllowFiles()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(CommentedFile)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, Comment, null, true, FileId, false, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowImages()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(CommentedImage)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, Comment, null, true, FileId, true, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(UncommentedImage)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, null, null, true, FileId, true, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowQueuedPosts()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(CommentedImage)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, Comment, null, true, FileId, true, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowScheduledPosts()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, TwoDaysFromNow, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, Comment, TwoDaysFromNow, false, FileId, true, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowImmediatePosts()
        {
            this.subStatements.Setup(_ => _.PostNowAsync(CommentedImage, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, CollectionId, Comment, null, false, FileId, true, Now);

            this.subStatements.Verify();
        }
    }
}