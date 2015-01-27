namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Commands;
    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.CodeGeneration;

    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<CreateSubscriptionCommand> createSubscription;
        private readonly ICommandHandler<UpdateSubscriptionCommand> updateSubscription;
        private readonly IRequesterContext requesterContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<IHttpActionResult> PostSubscription(NewSubscriptionData subscription)
        {
            subscription.AssertBodyProvided("subscription");
            subscription.Parse();

            var requester = this.requesterContext.GetRequester();
            var newSubscriptionId = new SubscriptionId(this.guidCreator.CreateSqlSequential());

            await this.createSubscription.HandleAsync(new CreateSubscriptionCommand(
                requester,
                newSubscriptionId,
                subscription.SubscriptionNameObject,
                subscription.TaglineObject,
                subscription.BasePriceObject));

            return this.Ok();
        }

        [Route("{subscriptionId}")]
        public async Task<IHttpActionResult> PutSubscription(string subscriptionId, [FromBody]UpdatedSubscriptionData subscription)
        {
            subscriptionId.AssertUrlParameterProvided("subscriptionId");
            subscription.AssertBodyProvided("subscription");
            subscription.Parse();

            var requester = this.requesterContext.GetRequester();
            var subscriptionIdObject = new SubscriptionId(subscriptionId.DecodeGuid());

            await this.updateSubscription.HandleAsync(new UpdateSubscriptionCommand(
                requester,
                subscriptionIdObject,
                subscription.SubscriptionNameObject,
                subscription.TaglineObject,
                subscription.IntroductionObject,
                subscription.DescriptionObject,
                subscription.HeaderImageFileId,
                subscription.VideoObject));

            return this.Ok();
        }
    }
}
