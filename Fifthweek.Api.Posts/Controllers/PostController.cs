namespace Fifthweek.Api.Posts.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Queries;
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
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Route("notes")]
        public async Task<IHttpActionResult> PostNote(NewNoteData note)
        {
            note.AssertBodyProvided("note");
            note.Parse();

            var requester = this.userContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postNote.HandleAsync(new PostNoteCommand(
                requester,
                newPostId,
                note.ChannelId,
                note.NoteObject,
                note.ScheduledPostDate));

            return this.Ok();
        }

        [Route("images")]
        public async Task<IHttpActionResult> PostImage(NewImageData image)
        {
            image.AssertBodyProvided("image");
            image.Parse();

            var requester = this.userContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                requester,
                newPostId,
                image.CollectionId,
                image.ImageFileId,
                image.CommentObject,
                image.ScheduledPostDate,
                image.IsQueued));

            return this.Ok();
        }

        [Route("files")]
        public async Task<IHttpActionResult> PostFile(NewFileData file)
        {
            file.AssertBodyProvided("file");
            file.Parse();

            var requester = this.userContext.GetRequester();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postFile.HandleAsync(new PostFileCommand(
                requester,
                newPostId,
                file.CollectionId,
                file.FileId,
                file.CommentObject,
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
            var requester = this.userContext.GetRequester();

            await this.reorderQueue.HandleAsync(new ReorderQueueCommand(requester, collectionIdObject, newQueueOrder.ToList()));

            return this.Ok();
        }

        [Route("creatorBacklog/{creatorId}")]
        public async Task<IEnumerable<BacklogPost>> GetCreatorBacklog(string creatorId)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.userContext.GetRequester();

            return await this.getCreatorBacklog.HandleAsync(new GetCreatorBacklogQuery(requester, creatorIdObject));
        }

        [Route("creatorNewsfeed/{creatorId}")]
        public async Task<IEnumerable<NewsfeedPost>> GetCreatorNewsfeed(string creatorId, [FromUri]CreatorNewsfeedRequestData requestData)
        {
            creatorId.AssertUrlParameterProvided("creatorId");
            requestData.AssertUrlParameterProvided("requestData");
            requestData.Parse();

            var creatorIdObject = new UserId(creatorId.DecodeGuid());
            var requester = this.userContext.GetRequester();

            return await this.getCreatorNewsfeed.HandleAsync(new GetCreatorNewsfeedQuery(requester, creatorIdObject, requestData.StartIndexObject, requestData.CountObject));
        }

        [Route("{postId}")]
        public Task DeletePost(string postId)
        {
            postId.AssertUrlParameterProvided("postId");
            var parsedPostId = new PostId(postId.DecodeGuid());
            var requester = this.userContext.GetRequester();

            return this.deletePost.HandleAsync(new DeletePostCommand(parsedPostId, requester));
        }
    }
}
