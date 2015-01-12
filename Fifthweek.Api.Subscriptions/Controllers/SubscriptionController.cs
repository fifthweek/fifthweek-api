using System.Threading.Tasks;
using System.Web.Http;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.OAuth;
using Fifthweek.Api.Subscriptions.Commands;
using Fifthweek.Api.Subscriptions.Queries;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<CreateSubscriptionCommand> setMandatorySubscriptionFields;
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;
        private readonly IUserContext userContext;
        private readonly IGuidCreator guidCreator;

        [Route("")]
        public async Task<IHttpActionResult> PostNewSubscription(NewSubscriptionData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var subscriptionId = new SubscriptionId(this.guidCreator.CreateSqlSequential());

            await this.setMandatorySubscriptionFields.HandleAsync(new CreateSubscriptionCommand(
                authenticatedUserId,
                subscriptionId,
                fields.SubscriptionNameObject,
                fields.TaglineObject,
                fields.BasePriceObject));

            return this.Ok();
        }
    }
}
