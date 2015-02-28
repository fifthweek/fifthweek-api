namespace Fifthweek.Api.Posts.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class NotePostController : INotePostController
    {
        private readonly ICommandHandler<PostNoteCommand> postNote;
        private readonly ICommandHandler<ReviseNoteCommand> reviseNote;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        public async Task PostNote(NewNoteData noteData)
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
        }

        public async Task PutNote(string postId, RevisedNoteData noteData)
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
        }
    }
}
