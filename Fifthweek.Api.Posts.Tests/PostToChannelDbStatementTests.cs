namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostToChannelDbStatementTests
    {
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly FileId ImageId = new FileId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey guys!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly Post UncommentedImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                ImageId.Value,
                null,
                null,
                default(DateTime),
                Now);

        private static readonly Post CommentedImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                ImageId.Value,
                null,
                Comment.Value,
                default(DateTime),
                Now);

        private static readonly Post CommentedFile = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                FileId.Value,
                null,
                null,
                null,
                Comment.Value,
                default(DateTime),
                Now);

        private static readonly Post CommentedFileAndImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                FileId.Value,
                null,
                ImageId.Value,
                null,
                Comment.Value,
                default(DateTime),
                Now);

        private static readonly Post CommentOnly = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                Comment.Value,
                default(DateTime),
                Now);

        private static readonly Post QueuedCommentedFileAndImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                QueueId.Value,
                null,
                FileId.Value,
                null,
                ImageId.Value,
                null,
                Comment.Value,
                default(DateTime),
                Now);

        private Mock<IPostToChannelDbSubStatements> subStatements;
        private PostToChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give side-effecting components strict mock behaviour.
            this.subStatements = new Mock<IPostToChannelDbSubStatements>(MockBehavior.Strict);

            this.target = new PostToChannelDbStatement(this.subStatements.Object);
        }

        [TestMethod]
        public async Task ItShouldAllowFiles()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedFile, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, null, null, FileId, null, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowImages()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, null, null, null, ImageId, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(UncommentedImage, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, null, null, null, null, ImageId, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowFilesAndImagesAndComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, null, null, FileId, ImageId, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowOnlyComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentOnly, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, null, null, null, null, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowScheduledPosts()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, TwoDaysFromNow, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, TwoDaysFromNow, null, null, ImageId, Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowQueuedPosts()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(QueuedCommentedFileAndImage)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(PostId, ChannelId, Comment, null, QueueId, FileId, ImageId, Now);

            this.subStatements.Verify();
        }
    }
}