namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private Mock<ICommandHandler<PostNoteCommand>> postNote;
        private Mock<ICommandHandler<PostImageCommand>> postImage;
        private Mock<ICommandHandler<PostFileCommand>> postFile;
        private Mock<IUserContext> userContext;
        private Mock<IGuidCreator> guidCreator;
        private PostController target;

        [TestInitialize]
        public void Initialize()
        {
            this.postNote = new Mock<ICommandHandler<PostNoteCommand>>();
            this.postImage = new Mock<ICommandHandler<PostImageCommand>>();
            this.postFile = new Mock<ICommandHandler<PostFileCommand>>();
            this.userContext = new Mock<IUserContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new PostController(
                this.postNote.Object,
                this.postImage.Object,
                this.postFile.Object,
                this.userContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingNote_ItShouldIssuePostNoteCommand()
        {
            var data = NewNoteData();
            var command = NewPostNoteCommand(UserId, PostId, data);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postNote.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostNote(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        [TestMethod]
        public async Task WhenPostingImage_ItShouldIssuePostImageCommand()
        {
            var data = NewImageData();
            var command = NewPostImageCommand(UserId, PostId, data);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postImage.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostImage(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        [TestMethod]
        public async Task WhenPostingFile_ItShouldIssuePostFileCommand()
        {
            var data = NewFileData();
            var command = NewPostFileCommand(UserId, PostId, data);

            this.userContext.Setup(v => v.GetUserId()).Returns(UserId);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postFile.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostFile(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        public static NewNoteData NewNoteData()
        {
            return new NewNoteData
            {
                ChannelId = ChannelId.Value.EncodeGuid(),
                Note = "Hey peeps ;)",
                ScheduledPostDate = TwoDaysFromNow
            };
        }

        public static PostNoteCommand NewPostNoteCommand(
            UserId userId,
            PostId postId,
            NewNoteData data)
        {
            return new PostNoteCommand(
                userId,
                postId,
                new ChannelId(data.ChannelId.DecodeGuid()),
                ValidNote.Parse(data.Note),
                data.ScheduledPostDate);
        }

        public static NewImageData NewImageData()
        {
            return new NewImageData
            {
                CollectionId = CollectionId.Value.EncodeGuid(),
                ImageFileId = FileId.Value.EncodeGuid(),
                Comment = null,
                ScheduledPostDate = null,
                IsQueued = true
            };
        }

        public static PostImageCommand NewPostImageCommand(
            UserId userId,
            PostId postId,
            NewImageData data)
        {
            return new PostImageCommand(
                userId,
                postId,
                new CollectionId(data.CollectionId.DecodeGuid()), 
                new FileId(data.ImageFileId.DecodeGuid()),
                data.Comment == null ? null : ValidComment.Parse(data.Comment),
                data.ScheduledPostDate,
                data.IsQueued);
        }

        public static NewFileData NewFileData()
        {
            return new NewFileData
            {
                CollectionId = CollectionId.Value.EncodeGuid(),
                FileId = FileId.Value.EncodeGuid(),
                Comment = null,
                ScheduledPostDate = null,
                IsQueued = true
            };
        }

        public static PostFileCommand NewPostFileCommand(
            UserId userId,
            PostId postId,
            NewFileData data)
        {
            return new PostFileCommand(
                userId,
                postId,
                new CollectionId(data.CollectionId.DecodeGuid()),
                new FileId(data.FileId.DecodeGuid()),
                data.Comment == null ? null : ValidComment.Parse(data.Comment),
                data.ScheduledPostDate,
                data.IsQueued);
        }
    }
}