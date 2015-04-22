namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostController : IPostController
    {
        private readonly ICommandHandler<DeletePostCommand> deletePost;
        private readonly ICommandHandler<ReorderQueueCommand> reorderQueue;
        private readonly ICommandHandler<RescheduleForNowCommand> rescheduleForNow;
        private readonly ICommandHandler<RescheduleForTimeCommand> rescheduleForTime;
        private readonly ICommandHandler<RescheduleWithQueueCommand> rescheduleWithQueue;
        private readonly IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>> getCreatorBacklog;
        private readonly IQueryHandler<GetNewsfeedQuery, GetNewsfeedQueryResult> getNewsfeed;
        private readonly IRequesterContext requesterContext;

        public async Task<IEnumerable<GetCreatorBacklogQueryResult>> GetCreatorBacklog(string creatorId)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return await this.getCreatorBacklog.HandleAsync(new GetCreatorBacklogQuery(requester, creatorIdObject));
        }

        public async Task<IEnumerable<GetCreatorNewsfeedQueryResult>> GetCreatorNewsfeed(string creatorId, CreatorNewsfeedPaginationData newsfeedPaginationData)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            newsfeedPaginationData.AssertUrlParameterProvided("newsfeedPaginationData");
            var newsfeedPagination = newsfeedPaginationData.Parse();

            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            var result = await this.getNewsfeed.HandleAsync(new GetNewsfeedQuery(
                requester, creatorIdObject, null, null, null, false, newsfeedPagination.StartIndex, newsfeedPagination.Count));

            return result.Posts.Select(v => new GetCreatorNewsfeedQueryResult(
                v.PostId,
                v.ChannelId,
                v.CollectionId,
                v.Comment,
                v.File,
                v.FileSource,
                v.Image,
                v.ImageSource,
                v.LiveDate)).ToList();
        }

        public async Task<GetNewsfeedQueryResult> GetNewsfeed(NewsfeedFilter filterData)
        {
            filterData.AssertUrlParameterProvided("filter");
            var filter = filterData.Parse();

            UserId creatorId = null;
            if (!string.IsNullOrWhiteSpace(filterData.CreatorId))
            {
                creatorId = new UserId(filterData.CreatorId.DecodeGuid());
            }

            IReadOnlyList<ChannelId> channelIds = null;
            if (filter.ChannelIds != null && filter.ChannelIds.Count > 0)
            {
                channelIds = filter.ChannelIds.Select(v => new ChannelId(v.DecodeGuid())).ToList();
            }

            IReadOnlyList<CollectionId> collectionIds = null;
            if (filter.CollectionIds != null && filter.CollectionIds.Count > 0)
            {
                collectionIds = filter.CollectionIds.Select(v => new CollectionId(v.DecodeGuid())).ToList();
            }

            var requester = this.requesterContext.GetRequester();

            return await this.getNewsfeed.HandleAsync(new GetNewsfeedQuery(
                requester, creatorId, channelIds, collectionIds, filter.Origin, filter.SearchForwards, filter.StartIndex, filter.Count));
        }

        public Task DeletePost(string postId)
        {
            postId.AssertUrlParameterProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.deletePost.HandleAsync(new DeletePostCommand(parsedPostId, requester));
        }

        public async Task PostNewQueueOrder(string collectionId, IEnumerable<PostId> newQueueOrder)
        {
            collectionId.AssertUrlParameterProvided("collectionId");
            newQueueOrder.AssertBodyProvided("newQueueOrder");

            var collectionIdObject = new CollectionId(collectionId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            await this.reorderQueue.HandleAsync(new ReorderQueueCommand(requester, collectionIdObject, newQueueOrder.ToList()));
        }

        public Task PostToQueue(string postId)
        {
            postId.AssertUrlParameterProvided("postId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleWithQueue.HandleAsync(new RescheduleWithQueueCommand(requester, parsedPostId));
        }

        public Task PostToLive(string postId)
        {
            postId.AssertBodyProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleForNow.HandleAsync(new RescheduleForNowCommand(requester, parsedPostId));
        }

        public Task PutLiveDate(string postId, DateTime newLiveDate)
        {
            postId.AssertUrlParameterProvided("postId");
            newLiveDate.AssertUtc("newLiveDate");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            return this.rescheduleForTime.HandleAsync(new RescheduleForTimeCommand(requester, parsedPostId, newLiveDate));
        }
    }
}
