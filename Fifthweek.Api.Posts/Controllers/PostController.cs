namespace Fifthweek.Api.Posts.Controllers
{
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
        private readonly ICommandHandler<PostNoteCommand> postNote;
        private readonly ICommandHandler<PostImageCommand> postImage;
        private readonly ICommandHandler<PostFileCommand> postFile;
        private readonly ICommandHandler<DeletePostCommand> deletePost;
        private readonly ICommandHandler<ReorderQueueCommand> reorderQueue;
        private readonly IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>> getCreatorBacklog;
        private readonly IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>> getCreatorNewsfeed;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route("notes")]
        public async Task<IHttpActionResult> PostNote(NewNoteData noteData)
        {
            noteData.AssertBodyProvided("noteData");
            var note = noteData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postNote.HandleAsync(new PostNoteCommand(
                requester,
                newPostId,
                note.ChannelId,
                note.Note,
                note.ScheduledPostDate));

            return this.Ok();
        }

        [Route("images")]
        public async Task<IHttpActionResult> PostImage(NewImageData imageData)
        {
            imageData.AssertBodyProvided("imageData");
            var image = imageData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                requester,
                newPostId,
                image.CollectionId,
                image.ImageFileId,
                image.Comment,
                image.ScheduledPostDate,
                image.IsQueued));

            return this.Ok();
        }

        [Route("files")]
        public async Task<IHttpActionResult> PostFile(NewFileData fileData)
        {
            fileData.AssertBodyProvided("fileData");
            var file = fileData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postFile.HandleAsync(new PostFileCommand(
                requester,
                newPostId,
                file.CollectionId,
                file.FileId,
                file.Comment,
                file.ScheduledPostDate,
                file.IsQueued));

            return this.Ok();
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
    }
}
