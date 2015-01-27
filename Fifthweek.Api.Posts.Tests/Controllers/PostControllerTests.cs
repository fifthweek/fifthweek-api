namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;
    using CollectionId = Fifthweek.Api.Collections.Shared.CollectionId;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class PostControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private Mock<ICommandHandler<PostNoteCommand>> postNote;
        private Mock<ICommandHandler<PostImageCommand>> postImage;
        private Mock<ICommandHandler<PostFileCommand>> postFile;
        private Mock<ICommandHandler<DeletePostCommand>> deletePost;
        private Mock<ICommandHandler<ReorderQueueCommand>> reorderQueue;
        private Mock<IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>>> getCreatorBacklog;
        private Mock<IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>>> getCreatorNewsfeed;
        private Mock<IUserContext> userContext;
        private Mock<IGuidCreator> guidCreator;
        private PostController target;

        [TestInitialize]
        public void Initialize()
        {
            this.postNote = new Mock<ICommandHandler<PostNoteCommand>>();
            this.postImage = new Mock<ICommandHandler<PostImageCommand>>();
            this.postFile = new Mock<ICommandHandler<PostFileCommand>>();
            this.deletePost = new Mock<ICommandHandler<DeletePostCommand>>();
            this.reorderQueue = new Mock<ICommandHandler<ReorderQueueCommand>>();
            this.getCreatorBacklog = new Mock<IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>>>();
            this.getCreatorNewsfeed = new Mock<IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>>>();
            this.userContext = new Mock<IUserContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new PostController(
                this.postNote.Object,
                this.postImage.Object,
                this.postFile.Object,
                this.deletePost.Object,
                this.reorderQueue.Object,
                this.getCreatorBacklog.Object,
                this.getCreatorNewsfeed.Object,
                this.userContext.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingNote_ItShouldIssuePostNoteCommand()
        {
            var data = NewNoteData();
            var command = NewPostNoteCommand(UserId, PostId, data);

            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);
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

            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);
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

            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(PostId.Value);
            this.postFile.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostFile(data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.postNote.Verify();
        }

        [TestMethod]
        public async Task WhenGettingCreatorBacklog_ItShouldReturnResultFromCreatorBacklogQuery()
        {
            var query = new GetCreatorBacklogQuery(Requester, UserId);
            var queryResult = new[] { new BacklogPost(PostId, ChannelId, CollectionId, new Comment(""), null, null, false, DateTime.UtcNow) };

            this.userContext.Setup(_ => _.GetRequester()).Returns(Requester);
            this.getCreatorBacklog.Setup(_ => _.HandleAsync(query)).ReturnsAsync(queryResult);

            var result = await this.target.GetCreatorBacklog(UserId.Value.EncodeGuid());

            Assert.AreEqual(result, queryResult);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingCreatorBacklogWithoutSpecifyingCreatorId_ItShouldThrowBadRequestException()
        {
            await this.target.GetCreatorBacklog(string.Empty);
        }

        [TestMethod]
        public async Task WhenGettingCreatorNewsfeed_ItShouldReturnResultFromCreatorBacklogQuery()
        {
            var query = new GetCreatorNewsfeedQuery(Requester, UserId, NonNegativeInt.Parse(10), PositiveInt.Parse(5));
            var requestData = new CreatorNewsfeedRequestData { Count = 5, StartIndex = 10 };
            var queryResult = new[] { new NewsfeedPost(PostId, ChannelId, CollectionId, new Comment(""), null, null, DateTime.UtcNow) };

            this.userContext.Setup(_ => _.GetRequester()).Returns(Requester);
            this.getCreatorNewsfeed.Setup(_ => _.HandleAsync(query)).ReturnsAsync(queryResult);

            var result = await this.target.GetCreatorNewsfeed(UserId.Value.EncodeGuid(), requestData);

            Assert.AreEqual(result, queryResult);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingCreatorNewsfeedWithoutSpecifyingCreatorId_ItShouldThrowBadRequestException()
        {
            await this.target.GetCreatorNewsfeed(string.Empty, new CreatorNewsfeedRequestData());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingCreatorNewsfeedWithoutSpecifyingPagination_ItShouldThrowBadRequestException()
        {
            await this.target.GetCreatorNewsfeed(UserId.Value.EncodeGuid(), null);
        }

        [TestMethod]
        public async Task WhenDeletingPost_ItShouldIssueDeletePostCommand()
        {
            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.deletePost.Setup(v => v.HandleAsync(new DeletePostCommand(PostId, Requester)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.DeletePost(PostId.Value.EncodeGuid());

            this.deletePost.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenDeletingPostWithoutSpecifyingPostId_ItShouldThrowBadRequestException()
        {
            await this.target.DeletePost(string.Empty);
        }

        [TestMethod]
        public async Task WhenReorderingQueue_ItShouldIssueReorderQueueCommand()
        {
            var newQueueOrder = new[] { new PostId(Guid.NewGuid()) };

            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.reorderQueue.Setup(v => v.HandleAsync(new ReorderQueueCommand(Requester, CollectionId, newQueueOrder)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostNewQueueOrder(CollectionId.Value.EncodeGuid(), newQueueOrder);

            this.reorderQueue.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReorderingQueueWithoutSpecifyingCollectionId_ItShouldThrowBadRequestException()
        {
            await this.target.PostNewQueueOrder(string.Empty, Enumerable.Empty<PostId>());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReorderingQueueWithoutSpecifyingNewOrder_ItShouldThrowBadRequestException()
        {
            await this.target.PostNewQueueOrder(CollectionId.Value.EncodeGuid(), null);
        }

        public static NewNoteData NewNoteData()
        {
            return new NewNoteData
            {
                ChannelId = ChannelId,
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
                Requester.Authenticated(userId),
                postId,
                data.ChannelId,
                ValidNote.Parse(data.Note),
                data.ScheduledPostDate);
        }

        public static NewImageData NewImageData()
        {
            return new NewImageData
            {
                CollectionId = CollectionId,
                ImageFileId = FileId,
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
                Requester.Authenticated(userId),
                postId,
                data.CollectionId, 
                data.ImageFileId,
                data.Comment == null ? null : ValidComment.Parse(data.Comment),
                data.ScheduledPostDate,
                data.IsQueued);
        }

        public static NewFileData NewFileData()
        {
            return new NewFileData
            {
                CollectionId = CollectionId,
                FileId = FileId,
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
                Requester.Authenticated(userId),
                postId,
                data.CollectionId,
                data.FileId,
                data.Comment == null ? null : ValidComment.Parse(data.Comment),
                data.ScheduledPostDate,
                data.IsQueued);
        }
    }
}