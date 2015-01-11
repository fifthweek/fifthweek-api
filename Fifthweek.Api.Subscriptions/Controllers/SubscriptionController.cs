using System;
using System.Threading.Tasks;
using System.Web.Http;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.OAuth;
using Fifthweek.Api.Subscriptions.Commands;

namespace Fifthweek.Api.Subscriptions.Controllers
{
    [RoutePrefix("subscriptions"), AutoConstructor]
    public partial class SubscriptionController : ApiController
    {
        private readonly ICommandHandler<CreateSubscriptionCommand> setMandatorySubscriptionFields;
        private readonly IUserContext userContext;

        [Route("{subscriptionId}/mandatoryFields")]
        public async Task<IHttpActionResult> PutMandatorySubscriptionFields(Guid subscriptionId, MandatorySubscriptionData fields)
        {
            fields.Parse();

            var authenticatedUserId = this.userContext.GetUserId();
            var subscriptionIdObject = new SubscriptionId(subscriptionId);

            await this.setMandatorySubscriptionFields.HandleAsync(new CreateSubscriptionCommand(
                authenticatedUserId,
                subscriptionIdObject,
                fields.SubscriptionNameObject,
                fields.TaglineObject,
                fields.BasePriceObject));

            return this.Ok();
        }
    }
}
