namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
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
        private static readonly ValidPreviewText PreviewText = ValidPreviewText.Parse("preview-comment");
        private static readonly ValidComment Content = ValidComment.Parse("full-comment");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime Now = DateTime.UtcNow;

        private static readonly IReadOnlyList<FileId> UncommentedImageFileIds = new List<FileId> { ImageId };
        private static readonly IReadOnlyList<PostFile> UncommentedImagePostFiles = new List<PostFile> { new PostFile(PostId.Value, ImageId.Value) };
        private static readonly Post UncommentedImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                ImageId.Value,
                null,
                null,
                Content.Value,
                0,
                Content.Value.Length,
                1,
                0,
                0,
                default(DateTime),
                Now);

        private static readonly IReadOnlyList<FileId> CommentedImageFileIds = new List<FileId> { ImageId };
        private static readonly IReadOnlyList<PostFile> CommentedImagePostFiles = new List<PostFile> { new PostFile(PostId.Value, ImageId.Value) };
        private static readonly Post CommentedImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                ImageId.Value,
                null,
                PreviewText.Value,
                Content.Value,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                0,
                0,
                default(DateTime),
                Now);

        private static readonly IReadOnlyList<FileId> CommentedFileFileIds = new List<FileId> { FileId };
        private static readonly IReadOnlyList<PostFile> CommentedFilePostFiles = new List<PostFile> { new PostFile(PostId.Value, FileId.Value) };
        private static readonly Post CommentedFile = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                PreviewText.Value,
                Content.Value,
                PreviewText.Value.Length,
                Content.Value.Length,
                0,
                1,
                0,
                default(DateTime),
                Now);

        private static readonly IReadOnlyList<FileId> CommentedFileAndImageFileIds = new List<FileId> { ImageId, FileId };
        private static readonly IReadOnlyList<PostFile> CommentedFileAndImagePostFiles = new List<PostFile> { new PostFile(PostId.Value, ImageId.Value), new PostFile(PostId.Value, FileId.Value) };
        private static readonly Post CommentedFileAndImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                ImageId.Value,
                null,
                PreviewText.Value,
                Content.Value,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                1,
                1,
                default(DateTime),
                Now);

        private static readonly IReadOnlyList<FileId> CommentOnlyFileIds = new List<FileId> { };
        private static readonly IReadOnlyList<PostFile> CommentOnlyPostFiles = new List<PostFile> { };
        private static readonly Post CommentOnly = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                PreviewText.Value,
                Content.Value,
                PreviewText.Value.Length,
                Content.Value.Length,
                0,
                0,
                0,
                default(DateTime),
                Now);

        private static readonly IReadOnlyList<FileId> QueuedCommentedFileAndImageFileIds = new List<FileId> { ImageId, FileId };
        private static readonly IReadOnlyList<PostFile> QueuedCommentedFileAndImagePostFiles = new List<PostFile> { new PostFile(PostId.Value, ImageId.Value), new PostFile(PostId.Value, FileId.Value) };
        private static readonly Post QueuedCommentedFileAndImage = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                QueueId.Value,
                null,
                ImageId.Value,
                null,
                PreviewText.Value,
                Content.Value,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                1,
                1,
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
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedFile, CommentedFilePostFiles, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId, 
                ChannelId, 
                Content, 
                null, 
                null, 
                PreviewText, 
                null, 
                CommentedFileFileIds,
                PreviewText.Value.Length,
                Content.Value.Length, 
                0, 
                1, 
                0,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowImages()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, CommentedImagePostFiles, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId,
                ChannelId,
                Content, 
                null,
                null,
                PreviewText, 
                ImageId,
                CommentedImageFileIds,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                0,
                0,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowOptionalComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(UncommentedImage, UncommentedImagePostFiles, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId, 
                ChannelId,
                Content,
                null, 
                null,
                null,
                ImageId,
                UncommentedImageFileIds,
                0,
                Content.Value.Length,
                1,
                0,
                0,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowFilesAndImagesAndComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedFileAndImage, CommentedFileAndImagePostFiles, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId, 
                ChannelId,
                Content,
                null,
                null, 
                PreviewText, 
                ImageId,
                CommentedFileAndImageFileIds,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                1,
                1,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowOnlyComments()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentOnly, CommentOnlyPostFiles, null, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId, 
                ChannelId, 
                Content,
                null, 
                null, 
                PreviewText, 
                null,
                CommentOnlyFileIds,
                PreviewText.Value.Length,
                Content.Value.Length,
                0,
                0,
                0,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowScheduledPosts()
        {
            this.subStatements.Setup(_ => _.SchedulePostAsync(CommentedImage, CommentedImagePostFiles, TwoDaysFromNow, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId,
                ChannelId,
                Content, 
                TwoDaysFromNow,
                null, 
                PreviewText, 
                ImageId,
                CommentedImageFileIds,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                0,
                0,
                Now);

            this.subStatements.Verify();
        }

        [TestMethod]
        public async Task ItShouldAllowQueuedPosts()
        {
            this.subStatements.Setup(_ => _.QueuePostAsync(QueuedCommentedFileAndImage, QueuedCommentedFileAndImagePostFiles)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(
                PostId,
                ChannelId,
                Content,
                null,
                QueueId,
                PreviewText,
                ImageId,
                QueuedCommentedFileAndImageFileIds,
                PreviewText.Value.Length,
                Content.Value.Length,
                1,
                1,
                1,
                Now);

            this.subStatements.Verify();
        }
    }
}