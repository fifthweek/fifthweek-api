namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;
    using System.Web.Http.Description;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Queries;

    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<CreateSubscriptionCommand> createSubscription;
        private readonly ICommandHandler<UpdateSubscriptionCommand> updateSubscription;
        private readonly ICommandHandler<CreateNoteCommand> createNote; 
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<IHttpActionResult> PostSubscription(NewSubscriptionData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var newSubscriptionId = new SubscriptionId(this.guidCreator.CreateSqlSequential());

            await this.createSubscription.HandleAsync(new CreateSubscriptionCommand(
                authenticatedUserId,
                newSubscriptionId,
                fields.SubscriptionNameObject,
                fields.TaglineObject,
                fields.BasePriceObject));

            return this.Ok();
        }

        [Route("{subscriptionId}")]
        public async Task<IHttpActionResult> PutSubscription(string subscriptionId, [FromBody]UpdatedSubscriptionData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var subscriptionIdObject = new SubscriptionId(subscriptionId.DecodeGuid());

            await this.updateSubscription.HandleAsync(new UpdateSubscriptionCommand(
                authenticatedUserId,
                subscriptionIdObject,
                fields.SubscriptionNameObject,
                fields.TaglineObject,
                fields.IntroductionObject,
                fields.DescriptionObject,
                fields.HeaderImageFileIdObject,
                fields.VideoObject));

            return this.Ok();
        }

        [Route("{subscriptionId}")]
        public async Task<IHttpActionResult> PostNote(string subscriptionId, [FromBody]NewNoteData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var subscriptionIdObject = new SubscriptionId(subscriptionId.DecodeGuid());

            await this.createNote.HandleAsync(new CreateNoteCommand(
                authenticatedUserId,
                subscriptionIdObject,
                fields.ChannelIdObject,
                fields.NoteObject,
                fields.ScheduledPostDate));

            return this.Ok();
        }

        [Authorize]
        [ResponseType(typeof(CreatorStatusData))]
        [Route("currentCreatorStatus")]
        public async Task<CreatorStatusData> GetCurrentCreatorStatus()
        {
            var authenticatedUserId = this.userContext.GetUserId();
            var creatorStatus = await this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(authenticatedUserId));
            return new CreatorStatusData(
                creatorStatus.SubscriptionId == null ? null : creatorStatus.SubscriptionId.Value.EncodeGuid(), 
                creatorStatus.MustWriteFirstPost);
        }
    }
}
