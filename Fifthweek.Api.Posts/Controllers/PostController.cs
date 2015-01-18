namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostController : ApiController
    {
        private readonly ICommandHandler<PostNoteCommand> postNote;
        private readonly ICommandHandler<PostImageCommand> postImage;
        private readonly ICommandHandler<PostFileCommand> postFile; 
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Authorize]
        [Route("notes")]
        public async Task<IHttpActionResult> PostNote(NewNoteData note)
        {
            note.AssertBodyProvided("note");
            note.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postNote.HandleAsync(new PostNoteCommand(
                authenticatedUserId,
                newPostId,
                note.ChannelIdObject,
                note.NoteObject,
                note.ScheduledPostDate));

            return this.Ok();
        }

        [Authorize]
        [Route("images")]
        public async Task<IHttpActionResult> PostImage(NewImageData image)
        {
            image.AssertBodyProvided("image");
            image.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                authenticatedUserId,
                newPostId,
                image.CollectionIdObject,
                image.ImageFileIdObject,
                image.CommentObject,
                image.ScheduledPostDate,
                image.IsQueued));

            return this.Ok();
        }

        [Authorize]
        [Route("files")]
        public async Task<IHttpActionResult> PostFile(NewFileData file)
        {
            file.AssertBodyProvided("file");
            file.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postFile.HandleAsync(new PostFileCommand(
                authenticatedUserId,
                newPostId,
                file.CollectionIdObject,
                file.FileIdObject,
                file.CommentObject,
                file.ScheduledPostDate,
                file.IsQueued));

            return this.Ok();
        }
    }
}
