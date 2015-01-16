namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostController : ApiController
    {
        private readonly ICommandHandler<PostNoteCommand> postNote;
        private readonly ICommandHandler<PostImageCommand> postImage;
        private readonly ICommandHandler<PostFileCommand> postFile; 
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Route("notes")]
        public async Task<IHttpActionResult> PostNote(NewNoteData note)
        {
            note.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postNote.HandleAsync(new PostNoteCommand(
                authenticatedUserId,
                note.ChannelIdObject,
                newPostId,
                note.NoteObject,
                note.ScheduledPostDate));

            return this.Ok();
        }

        [Route("images")]
        public async Task<IHttpActionResult> PostImage(NewImageData image)
        {
            image.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                authenticatedUserId,
                image.CollectionIdObject,
                newPostId,
                image.ImageFileIdObject,
                image.ScheduledPostDate,
                image.IsQueued));

            return this.Ok();
        }

        [Route("files")]
        public async Task<IHttpActionResult> PostFile(NewFileData file)
        {
            file.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.postImage.HandleAsync(new PostImageCommand(
                authenticatedUserId,
                file.CollectionIdObject,
                newPostId,
                file.FileIdObject,
                file.ScheduledPostDate,
                file.IsQueued));

            return this.Ok();
        }
    }
}
