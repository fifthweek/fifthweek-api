namespace Fifthweek.Api.Posts.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostsController : ApiController, IPostController, INotePostController, IImagePostController, IFilePostController
    {
        private readonly IPostController postController;
        private readonly INotePostController notePostController;
        private readonly IImagePostController imagePostController;
        private readonly IFilePostController filePostController;

        [Route("creatorBacklog/{creatorId}")]
        public Task<IEnumerable<GetCreatorBacklogQueryResult>> GetCreatorBacklog(string creatorId)
        {
            return this.postController.GetCreatorBacklog(creatorId);
        }

        [Route("creatorNewsfeed/{creatorId}")]
        public Task<IEnumerable<GetCreatorNewsfeedQueryResult>> GetCreatorNewsfeed(string creatorId, [FromUri]CreatorNewsfeedPaginationData newsfeedPaginationData)
        {
            return this.postController.GetCreatorNewsfeed(creatorId, newsfeedPaginationData);
        }

        [Route("newsfeed")]
        public Task<GetNewsfeedQueryResult> GetNewsfeed([FromBody]NewsfeedFilter filter)
        {
            return this.postController.GetNewsfeed(filter);
        }

        [Route("{postId}")]
        public Task DeletePost(string postId)
        {
            return this.postController.DeletePost(postId);
        }

        [Route("queues/{collectionId}")]
        public Task PostNewQueueOrder(string collectionId, [FromBody]IEnumerable<PostId> newQueueOrder)
        {
            return this.postController.PostNewQueueOrder(collectionId, newQueueOrder);
        }

        [Route("queued")]
        public Task PostToQueue([FromBody]string postId)
        {
            return this.postController.PostToQueue(postId);
        }

        [Route("live")]
        public Task PostToLive([FromBody]string postId)
        {
            return this.postController.PostToLive(postId);
        }

        [Route("{postId}/liveDate")]
        public Task PutLiveDate(string postId, [FromBody]DateTime newLiveDate)
        {
            return this.postController.PutLiveDate(postId, newLiveDate);
        }

        [Route("notes")]
        public Task PostNote([FromBody]NewNoteData noteData)
        {
            return this.notePostController.PostNote(noteData);
        }

        [Route("notes/{postId}")]
        public Task PutNote(string postId, [FromBody]RevisedNoteData noteData)
        {
            return this.notePostController.PutNote(postId, noteData);
        }

        [Route("images")]
        public Task PostImage([FromBody]NewImageData imageData)
        {
            return this.imagePostController.PostImage(imageData);
        }

        [Route("images/{postId}")]
        public Task PutImage(string postId, [FromBody]RevisedImageData imageData)
        {
            return this.imagePostController.PutImage(postId, imageData);
        }

        [Route("files")]
        public Task PostFile([FromBody]NewFileData fileData)
        {
            return this.filePostController.PostFile(fileData);
        }

        [Route("files/{postId}")]
        public Task PutFile(string postId, [FromBody]RevisedFileData fileData)
        {
            return this.filePostController.PutFile(postId, fileData);
        }
    }
}