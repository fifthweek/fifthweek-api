namespace Fifthweek.Api.Subscriptions.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Core;
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
        public async Task<IHttpActionResult> PostSubscription(NewSubscriptionData subscriptionData)
        {
            subscriptionData.AssertBodyProvided("subscriptionData");
            var subscription = subscriptionData.Parse();

            var requester = this.requesterContext.GetRequester();
            var newSubscriptionId = new SubscriptionId(this.guidCreator.CreateSqlSequential());

            await this.createSubscription.HandleAsync(new CreateSubscriptionCommand(
                requester,
                newSubscriptionId,
                subscription.SubscriptionName,
                subscription.Tagline,
                subscription.BasePrice));

            return this.Ok();
        }

        [Route("{subscriptionId}")]
        public async Task<IHttpActionResult> PutSubscription(string subscriptionId, [FromBody]UpdatedSubscriptionData subscriptionData)
        {
            subscriptionId.AssertUrlParameterProvided("subscriptionId");
            subscriptionData.AssertBodyProvided("subscriptionData");
            var subscription = subscriptionData.Parse();

            var requester = this.requesterContext.GetRequester();
            var subscriptionIdObject = new SubscriptionId(subscriptionId.DecodeGuid());

            await this.updateSubscription.HandleAsync(new UpdateSubscriptionCommand(
                requester,
                subscriptionIdObject,
                subscription.SubscriptionName,
                subscription.Tagline,
                subscription.Introduction,
                subscription.Description,
                subscription.HeaderImageFileId,
                subscription.Video));

            return this.Ok();
        }
    }
}
