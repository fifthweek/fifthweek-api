namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("posts/notes"), AutoConstructor]
    public partial class NotePostController : ApiController
    {
        private readonly ICommandHandler<PostNoteCommand> postNote;
        private readonly ICommandHandler<ReviseNoteCommand> reviseNote;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

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
                note.ScheduledPostTime));

            return this.Ok();
        }

        [Route("{postId}")]
        public async Task<IHttpActionResult> PutNote(string postId, [FromBody]RevisedNoteData noteData)
        {
            postId.AssertUrlParameterProvided("postId");
            noteData.AssertBodyProvided("noteData");
            var note = noteData.Parse();
            var postIdObject = new PostId(postId.DecodeGuid());

            var requester = this.requesterContext.GetRequester();

            await this.reviseNote.HandleAsync(new ReviseNoteCommand(
                requester,
                postIdObject,
                note.ChannelId,
                note.Note));

            return this.Ok();
        }
    }
}
