namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostController : ApiController
    {
        private readonly ICommandHandler<DeletePostCommand> deletePost;
        private readonly ICommandHandler<ReorderQueueCommand> reorderQueue;
        private readonly ICommandHandler<RescheduleForNowCommand> rescheduleForNow;
        private readonly ICommandHandler<RescheduleForTimeCommand> rescheduleForTime;
        private readonly ICommandHandler<RescheduleWithQueueCommand> rescheduleWithQueue;
        private readonly IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>> getCreatorBacklog;
        private readonly IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>> getCreatorNewsfeed;
        private readonly IRequesterContext requesterContext;

        [Route("creatorBacklog/{creatorId}")]
        public async Task<IEnumerable<BacklogPost>> GetCreatorBacklog(string creatorId)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return await this.getCreatorBacklog.HandleAsync(new GetCreatorBacklogQuery(requester, creatorIdObject));
        }

        [Route("creatorNewsfeed/{creatorId}")]
        public async Task<IEnumerable<NewsfeedPost>> GetCreatorNewsfeed(string creatorId, [FromUri]CreatorNewsfeedPaginationData newsfeedPaginationData)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            newsfeedPaginationData.AssertUrlParameterProvided("newsfeedPaginationData");
            var newsfeedPagination = newsfeedPaginationData.Parse();

            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return await this.getCreatorNewsfeed.HandleAsync(new GetCreatorNewsfeedQuery(requester, creatorIdObject, newsfeedPagination.StartIndex, newsfeedPagination.Count));
        }

        [Route("{postId}")]
        public Task DeletePost(string postId)
        {
            postId.AssertUrlParameterProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.deletePost.HandleAsync(new DeletePostCommand(parsedPostId, requester));
        }

        [Route("queues/{collectionId}")]
        public async Task<IHttpActionResult> PostNewQueueOrder(string collectionId, [FromBody]IEnumerable<PostId> newQueueOrder)
        {
            collectionId.AssertUrlParameterProvided("collectionId");
            newQueueOrder.AssertBodyProvided("newQueueOrder");

            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            await this.reorderQueue.HandleAsync(new ReorderQueueCommand(requester, collectionIdObject, newQueueOrder.ToList()));

            return this.Ok();
        }

        [Route("queued")]
        public Task PostToQueue(string postId)
        {
            postId.AssertUrlParameterProvided("postId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleWithQueue.HandleAsync(new RescheduleWithQueueCommand(requester, parsedPostId));
        }

        [Route("live")]
        public Task PostToLive(string postId)
        {
            postId.AssertBodyProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleForNow.HandleAsync(new RescheduleForNowCommand(requester, parsedPostId));
        }

        [Route("{postId}/liveDate")]
        public Task PutLiveDate(string postId, [FromBody]DateTime newLiveDate)
        {
            postId.AssertUrlParameterProvided("postId");
            newLiveDate.AssertUtc("newLiveDate");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleForTime.HandleAsync(new RescheduleForTimeCommand(requester, parsedPostId, newLiveDate));
        }
    }
}
