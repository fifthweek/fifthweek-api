namespace Fifthweek.Api.Posts.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private Mock<ICommandHandler<DeletePostCommand>> deletePost;
        private Mock<ICommandHandler<ReorderQueueCommand>> reorderQueue;
        private Mock<ICommandHandler<RescheduleForNowCommand>> rescheduleForNow;
        private Mock<ICommandHandler<RescheduleForTimeCommand>> rescheduleForTime;
        private Mock<ICommandHandler<RescheduleWithQueueCommand>> rescheduleWithQueue;
        private Mock<IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>>> getCreatorBacklog;
        private Mock<IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<GetCreatorNewsfeedQueryResult>>> getCreatorNewsfeed;
        private Mock<IRequesterContext> requesterContext;
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
            this.getCreatorNewsfeed = new Mock<IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<GetCreatorNewsfeedQueryResult>>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.target = new PostController(
                this.deletePost.Object,
                this.reorderQueue.Object,
                this.rescheduleForNow.Object,
                this.rescheduleForTime.Object,
                this.rescheduleWithQueue.Object,
                this.getCreatorBacklog.Object,
                this.getCreatorNewsfeed.Object,
                this.requesterContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingCreatorBacklog_ItShouldReturnResultFromCreatorBacklogQuery()
        {
            var query = new GetCreatorBacklogQuery(Requester, UserId);
            var queryResult = new[] { new GetCreatorBacklogQueryResult(PostId, ChannelId, CollectionId, new Comment(""), null, null, false, DateTime.UtcNow) };

            this.requesterContext.Setup(_ => _.GetRequester()).Returns(Requester);
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
            var requestData = new CreatorNewsfeedPaginationData { Count = 5, StartIndex = 10 };
            var queryResult = new[] { new GetCreatorNewsfeedQueryResult(PostId, ChannelId, CollectionId, new Comment(""), null, null, DateTime.UtcNow) };

            this.requesterContext.Setup(_ => _.GetRequester()).Returns(Requester);
            this.getCreatorNewsfeed.Setup(_ => _.HandleAsync(query)).ReturnsAsync(queryResult);

            var result = await this.target.GetCreatorNewsfeed(UserId.Value.EncodeGuid(), requestData);

            Assert.AreEqual(result, queryResult);
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
        public async Task WhenDeletingPost_ItShouldIssueDeletePostCommand()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
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

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
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
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
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
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
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

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
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
    }
}