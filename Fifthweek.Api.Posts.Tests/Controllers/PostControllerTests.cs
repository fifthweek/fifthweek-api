namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime? Origin = DateTime.UtcNow;
        private static readonly bool SearchForwards = true;
        private Mock<ICommandHandler<DeletePostCommand>> deletePost;
        private Mock<ICommandHandler<ReorderQueueCommand>> reorderQueue;
        private Mock<ICommandHandler<RescheduleForNowCommand>> rescheduleForNow;
        private Mock<ICommandHandler<RescheduleForTimeCommand>> rescheduleForTime;
        private Mock<ICommandHandler<RescheduleWithQueueCommand>> rescheduleWithQueue;
        private Mock<IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>>> getCreatorBacklog;
        private Mock<IQueryHandler<GetNewsfeedQuery, GetNewsfeedQueryResult>> getNewsfeed;
        private Mock<ICommandHandler<CommentOnPostCommand>> postComment;
        private Mock<IQueryHandler<GetCommentsQuery, CommentsResult>> getComments;
        private Mock<ICommandHandler<LikePostCommand>> likePost;
        private Mock<ICommandHandler<DeleteLikeCommand>> deleteLike;
        private Mock<IRequesterContext> requesterContext;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGuidCreator> guidCreator;
        private PostController target;

        [TestInitialize]
        public void Initialize()
        {
            this.deletePost = new Mock<ICommandHandler<DeletePostCommand>>();
            this.reorderQueue = new Mock<ICommandHandler<ReorderQueueCommand>>();
            this.rescheduleForNow = new Mock<ICommandHandler<RescheduleForNowCommand>>();
            this.rescheduleForTime = new Mock<ICommandHandler<RescheduleForTimeCommand>>();
            this.rescheduleWithQueue = new Mock<ICommandHandler<RescheduleWithQueueCommand>>();
            this.getCreatorBacklog = new Mock<IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>>>();
            this.getNewsfeed = new Mock<IQueryHandler<GetNewsfeedQuery, GetNewsfeedQueryResult>>();
            this.postComment = new Mock<ICommandHandler<CommentOnPostCommand>>();
            this.getComments = new Mock<IQueryHandler<GetCommentsQuery, CommentsResult>>();
            this.likePost = new Mock<ICommandHandler<LikePostCommand>>();
            this.deleteLike = new Mock<ICommandHandler<DeleteLikeCommand>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new PostController(
                this.deletePost.Object,
                this.reorderQueue.Object,
                this.rescheduleForNow.Object,
                this.rescheduleForTime.Object,
                this.rescheduleWithQueue.Object,
                this.getCreatorBacklog.Object,
                this.getNewsfeed.Object,
                this.postComment.Object,
                this.getComments.Object,
                this.likePost.Object,
                this.deleteLike.Object,
                this.requesterContext.Object,
                this.timestampCreator.Object,
                this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenGettingCreatorBacklog_ItShouldReturnResultFromCreatorBacklogQuery()
        {
            var query = new GetCreatorBacklogQuery(Requester, UserId);
            var queryResult = new[] { new GetCreatorBacklogQueryResult(PostId, ChannelId, CollectionId, new Comment(""), null, null, null, null, false, DateTime.UtcNow) };

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
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
        public async Task WhenGettingCreatorNewsfeed_ItShouldReturnResultFromNewsfeedQuery()
        {
            var query = new GetNewsfeedQuery(Requester, UserId, null, null, null, false, NonNegativeInt.Parse(10), PositiveInt.Parse(5));
            var requestData = new CreatorNewsfeedPaginationData { Count = 5, StartIndex = 10 };

            var now = DateTime.UtcNow;
            var queryResult = new GetNewsfeedQueryResult(new[] { new GetNewsfeedQueryResult.Post(UserId, PostId, BlogId, ChannelId, CollectionId, new Comment(string.Empty), null, null, null, null, now) }, 10);
            var expectedResult = new[] { new GetCreatorNewsfeedQueryResult(PostId, ChannelId, CollectionId, new Comment(string.Empty), null, null, null, null, now) };

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.getNewsfeed.Setup(_ => _.HandleAsync(query)).ReturnsAsync(queryResult);

            var result = await this.target.GetCreatorNewsfeed(UserId.Value.EncodeGuid(), requestData);

            CollectionAssert.AreEqual(expectedResult, result.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingCreatorNewsfeed_WithoutSpecifyingCreatorId_ItShouldThrowBadRequestException()
        {
            await this.target.GetCreatorNewsfeed(string.Empty, new CreatorNewsfeedPaginationData());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingCreatorNewsfeed_WithoutSpecifyingPagination_ItShouldThrowBadRequestException()
        {
            await this.target.GetCreatorNewsfeed(UserId.Value.EncodeGuid(), null);
        }

        [TestMethod]
        public async Task WhenGettingNewsfeed_ItShouldReturnResultFromNewsfeedQuery()
        {
            var query = new GetNewsfeedQuery(Requester, UserId, new[] { ChannelId }, new[] { CollectionId }, Origin, SearchForwards, NonNegativeInt.Parse(10), PositiveInt.Parse(5));
            var requestData = new NewsfeedFilter 
            { 
                CreatorId = UserId.Value.EncodeGuid(), 
                ChannelId = ChannelId.Value.EncodeGuid(),
                CollectionId = CollectionId.Value.EncodeGuid(),
                Origin = Origin,
                SearchForwards = SearchForwards,
                Count = 5, 
                StartIndex = 10 
            };

            var queryResult = new GetNewsfeedQueryResult(new[] { new GetNewsfeedQueryResult.Post(UserId, PostId, BlogId, ChannelId, CollectionId, new Comment(string.Empty), null, null, null, null, DateTime.UtcNow) }, 10);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.getNewsfeed.Setup(_ => _.HandleAsync(query)).ReturnsAsync(queryResult);

            var result = await this.target.GetNewsfeed(requestData);

            Assert.AreEqual(queryResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingNewsfeed_WithoutSpecifyingFilter_ItShouldThrowBadRequestException()
        {
            await this.target.GetNewsfeed(null);
        }

        [TestMethod]
        public async Task WhenDeletingPost_ItShouldIssueDeletePostCommand()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.deletePost.Setup(v => v.HandleAsync(new DeletePostCommand(PostId, Requester)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.DeletePost(PostId.Value.EncodeGuid());

            this.deletePost.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenDeletingPost_WithoutSpecifyingPostId_ItShouldThrowBadRequestException()
        {
            await this.target.DeletePost(string.Empty);
        }

        [TestMethod]
        public async Task WhenReorderingQueue_ItShouldIssueReorderQueueCommand()
        {
            var newQueueOrder = new[] { new PostId(Guid.NewGuid()) };

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.reorderQueue.Setup(v => v.HandleAsync(new ReorderQueueCommand(Requester, CollectionId, newQueueOrder)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostNewQueueOrder(CollectionId.Value.EncodeGuid(), newQueueOrder);

            this.reorderQueue.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReorderingQueue_WithoutSpecifyingCollectionId_ItShouldThrowBadRequestException()
        {
            await this.target.PostNewQueueOrder(string.Empty, Enumerable.Empty<PostId>());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReorderingQueue_WithoutSpecifyingNewOrder_ItShouldThrowBadRequestException()
        {
            await this.target.PostNewQueueOrder(CollectionId.Value.EncodeGuid(), null);
        }

        [TestMethod]
        public async Task WhenReschedulingWithQueue_ItShouldIssueRescheduleWithQueueCommand()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.rescheduleWithQueue.Setup(_ => _.HandleAsync(new RescheduleWithQueueCommand(Requester, PostId)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostToQueue(PostId.Value.EncodeGuid());

            this.rescheduleForNow.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReschedulingWithQueue_WithoutSpecifyingPostId_ItShouldThrowBadRequestException()
        {
            await this.target.PostToQueue(string.Empty);
        }

        [TestMethod]
        public async Task WhenReschedulingForNow_ItShouldIssueRescheduleForNowCommand()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.rescheduleForNow.Setup(_ => _.HandleAsync(new RescheduleForNowCommand(Requester, PostId)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostToLive(PostId.Value.EncodeGuid());

            this.rescheduleForNow.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReschedulingForNow_WithoutSpecifyingPostId_ItShouldThrowBadRequestException()
        {
            await this.target.PostToLive(string.Empty);
        }

        [TestMethod]
        public async Task WhenReschedulingForTime_ItShouldIssueRescheduleForTimeCommand()
        {
            var newLiveDate = DateTime.UtcNow;

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.rescheduleForTime.Setup(_ => _.HandleAsync(new RescheduleForTimeCommand(Requester, PostId, newLiveDate)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PutLiveDate(PostId.Value.EncodeGuid(), newLiveDate);

            this.rescheduleForTime.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenReschedulingForTime_WithoutSpecifyingPostId_ItShouldThrowBadRequestException()
        {
            await this.target.PutLiveDate(string.Empty, DateTime.UtcNow);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task WhenReschedulingForTime_AndNewDateIsNonUtc_ItShouldThrowBadRequestException()
        {
            await this.target.PutLiveDate(PostId.Value.EncodeGuid(), DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingComment_AndPostIdIsNotSpecified_ItShouldThrowAnException()
        {
            await this.target.PostComment(null, new CommentData());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingComment_AndCommentDataIsNotSpecified_ItShouldThrowAnException()
        {
            await this.target.PostComment(PostId.Value.ToString(), null);
        }

        [TestMethod]
        public async Task WhenPostingComment_ItShouldIssuePostCommentCommand()
        {
            var commentId = CommentId.Random();
            var content = ValidComment.Parse("This is a valid comment");
            var timestamp = DateTime.UtcNow;

            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(commentId.Value);
            this.timestampCreator.Setup(v => v.Now()).Returns(timestamp);
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.postComment.Setup(v => v.HandleAsync(new CommentOnPostCommand(Requester, PostId, commentId, content, timestamp)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostComment(PostId.Value.EncodeGuid(), new CommentData(content.Value));

            this.postComment.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingComments_AndPostIdIsNotSpecified_ItShouldThrowAnException()
        {
            await this.target.GetComments(null);
        }

        [TestMethod]
        public async Task WhenGettingComments_ItShouldIssueGetCommentsCommand()
        {
            var expectedResult = new CommentsResult(
                new List<CommentsResult.Item>
                {
                    new CommentsResult.Item(CommentId.Random(), PostId.Random(), UserId.Random(), new Username("blah"), new Comment("comment"), DateTime.UtcNow),
                });

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.getComments.Setup(v => v.HandleAsync(new GetCommentsQuery(Requester, PostId)))
                .Returns(Task.FromResult(expectedResult))
                .Verifiable();

            var result = await this.target.GetComments(PostId.Value.EncodeGuid());

            Assert.AreEqual(expectedResult, result);
            this.getComments.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingLike_AndPostIdIsNotSpecified_ItShouldThrowAnException()
        {
            await this.target.PostLike(null);
        }

        [TestMethod]
        public async Task WhenPostingLike_ItShouldIssuePostLikeCommand()
        {
            var timestamp = DateTime.UtcNow;

            this.timestampCreator.Setup(v => v.Now()).Returns(timestamp);
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.likePost.Setup(v => v.HandleAsync(new LikePostCommand(Requester, PostId, timestamp)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.PostLike(PostId.Value.EncodeGuid());

            this.likePost.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenDeletingLike_AndPostIdIsNotSpecified_ItShouldThrowAnException()
        {
            await this.target.DeleteLike(null);
        }

        [TestMethod]
        public async Task WhenDeletingLike_ItShouldIssueDeleteLikeCommand()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.deleteLike.Setup(v => v.HandleAsync(new DeleteLikeCommand(Requester, PostId)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.DeleteLike(PostId.Value.EncodeGuid());

            this.deleteLike.Verify();
        }
    }
}