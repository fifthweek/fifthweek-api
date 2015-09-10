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

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostController : ApiController
    {
        private readonly ICommandHandler<DeletePostCommand> deletePost;
        private readonly ICommandHandler<ReorderQueueCommand> reorderQueue;
        private readonly ICommandHandler<RescheduleForNowCommand> rescheduleForNow;
        private readonly ICommandHandler<RescheduleForTimeCommand> rescheduleForTime;
        private readonly ICommandHandler<RescheduleWithQueueCommand> rescheduleWithQueue;
        private readonly IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<GetCreatorBacklogQueryResult>> getCreatorBacklog;
        private readonly IQueryHandler<GetNewsfeedQuery, GetNewsfeedQueryResult> getNewsfeed;
        private readonly ICommandHandler<CommentOnPostCommand> postComment;
        private readonly IQueryHandler<GetCommentsQuery, CommentsResult> getComments;
        private readonly ICommandHandler<LikePostCommand> postLike;
        private readonly ICommandHandler<DeleteLikeCommand> deleteLike;
        private readonly ICommandHandler<PostToChannelCommand> postPost;
        private readonly ICommandHandler<RevisePostCommand> revisePost;
        private readonly IRequesterContext requesterContext;
        private readonly ITimestampCreator timestampCreator;
        private readonly IGuidCreator guidCreator;

        [Route("creatorBacklog/{creatorId}")]
        public async Task<IEnumerable<GetCreatorBacklogQueryResult>> GetCreatorBacklog(string creatorId)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getCreatorBacklog.HandleAsync(new GetCreatorBacklogQuery(requester, creatorIdObject));
        }

        [Route("newsfeed")]
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
            if (!string.IsNullOrWhiteSpace(filterData.ChannelId))
            {
                channelIds = new List<ChannelId> { new ChannelId(filterData.ChannelId.DecodeGuid()) };
            }

            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getNewsfeed.HandleAsync(new GetNewsfeedQuery(
                requester, creatorId, channelIds, filter.Origin, filter.SearchForwards, filter.StartIndex, filter.Count));
        }

        [Route("")]
        public async Task PostPost(NewPostData postData)
        {
            postData.AssertBodyProvided("postData");
            var post = postData.Parse();

            var requester = await this.requesterContext.GetRequesterAsync();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());
            var timestamp = this.timestampCreator.Now();

            await this.postPost.HandleAsync(new PostToChannelCommand(
                requester,
                newPostId,
                post.ChannelId,
                post.FileId,
                post.ImageId,
                post.Comment,
                post.ScheduledPostTime,
                post.QueueId,
                timestamp));
        }

        [Route("{postId}")]
        public async Task PutPost(string postId, RevisedPostData postData)
        {
            postId.AssertUrlParameterProvided("postId");
            postData.AssertBodyProvided("postData");

            var post = postData.Parse();
            var postIdObject = new PostId(postId.DecodeGuid());

            var requester = await this.requesterContext.GetRequesterAsync();

            await this.revisePost.HandleAsync(new RevisePostCommand(
                requester,
                postIdObject,
                post.FileId,
                post.ImageId,
                post.Comment));
        }

        [Route("{postId}")]
        public async Task DeletePost(string postId)
        {
            postId.AssertUrlParameterProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.deletePost.HandleAsync(new DeletePostCommand(parsedPostId, requester));
        }

        [Route("queues/{queueId}")]
        public async Task PostNewQueueOrder(string queueId, IEnumerable<PostId> newQueueOrder)
        {
            queueId.AssertUrlParameterProvided("queueId");
            newQueueOrder.AssertBodyProvided("newQueueOrder");

            var queueIdObject = new QueueId(queueId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.reorderQueue.HandleAsync(new ReorderQueueCommand(requester, queueIdObject, newQueueOrder.ToList()));
        }

        [Route("queued")]
        public async Task PostToQueue(string postId, string queueId)
        {
            postId.AssertUrlParameterProvided("postId");
            queueId.AssertBodyProvided("queueId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var parsedQueueId = new QueueId(queueId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.rescheduleWithQueue.HandleAsync(new RescheduleWithQueueCommand(requester, parsedPostId, parsedQueueId));
        }

        [Route("live")]
        public async Task PostToLive(string postId)
        {
            postId.AssertBodyProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.rescheduleForNow.HandleAsync(new RescheduleForNowCommand(requester, parsedPostId));
        }

        [Route("{postId}/liveDate")]
        public async Task PutLiveDate(string postId, DateTime newLiveDate)
        {
            postId.AssertUrlParameterProvided("postId");
            newLiveDate.AssertUtc("newLiveDate");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.rescheduleForTime.HandleAsync(new RescheduleForTimeCommand(requester, parsedPostId, newLiveDate));
        }

        [Route("{postId}/comments")]
        public async Task PostComment(string postId, CommentData comment)
        {
            postId.AssertUrlParameterProvided("postId");
            comment.AssertBodyProvided("comment");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var parsedComment = comment.Parse();
            var requester = await this.requesterContext.GetRequesterAsync();

            var timestamp = this.timestampCreator.Now();
            var commentId = this.guidCreator.CreateSqlSequential();

            await this.postComment.HandleAsync(new CommentOnPostCommand(
                requester, 
                parsedPostId,
                new CommentId(commentId),
                parsedComment.Content,
                timestamp));
        }

        [Route("{postId}/comments")]
        public async Task<CommentsResult> GetComments(string postId)
        {
            postId.AssertUrlParameterProvided("postId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            return await this.getComments.HandleAsync(new GetCommentsQuery(requester, parsedPostId));
        }

        [Route("{postId}/likes")]
        public async Task PostLike(string postId)
        {
            postId.AssertUrlParameterProvided("postId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            var timestamp = this.timestampCreator.Now();

            await this.postLike.HandleAsync(new LikePostCommand(requester, parsedPostId, timestamp));
        }

        [Route("{postId}/likes")]
        public async Task DeleteLike(string postId)
        {
            postId.AssertUrlParameterProvided("postId");

            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = await this.requesterContext.GetRequesterAsync();

            await this.deleteLike.HandleAsync(new DeleteLikeCommand(requester, parsedPostId));
        }
    }
}
