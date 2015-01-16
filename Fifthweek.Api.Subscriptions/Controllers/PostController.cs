namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Shared;

    [RoutePrefix("posts"), AutoConstructor]
    public partial class PostController : ApiController
    {
        private readonly ICommandHandler<CreateNoteCommand> createNote; 
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<IHttpActionResult> PostNote([FromBody]NewNoteData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newPostId = new PostId(this.guidCreator.CreateSqlSequential());

            await this.createNote.HandleAsync(new CreateNoteCommand(
                authenticatedUserId,
                fields.ChannelIdObject,
                newPostId,
                fields.NoteObject,
                fields.ScheduledPostDate));

            return this.Ok();
        }
    }
}
