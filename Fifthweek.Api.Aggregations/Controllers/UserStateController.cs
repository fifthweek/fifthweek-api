namespace Fifthweek.Api.Aggregations.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Controllers;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [RoutePrefix("userState")]
    public partial class UserStateController : ApiController
    {
        private readonly IQueryHandler<GetUserStateQuery, UserState> getUserState;

        private readonly IUserContext userContext;

        [Route("{userId}")]
        public async Task<UserStateResponse> Get(string userId)
        {
            userId.AssertUrlParameterProvided("userId");

            var requestedUserId = new UserId(userId.DecodeGuid());
            var requester = this.userContext.GetRequester();
            bool isCreator = this.userContext.IsUserInRole(FifthweekRole.Creator);

            var userState = await this.getUserState.HandleAsync(new GetUserStateQuery(requestedUserId, requester, isCreator));
            
            CreatorStatusData creatorStatus = null;
            if (userState.CreatorStatus != null)
            {
                creatorStatus = new CreatorStatusData(
                    userState.CreatorStatus.SubscriptionId == null
                        ? null
                        : userState.CreatorStatus.SubscriptionId.Value.EncodeGuid(),
                    userState.CreatorStatus.MustWriteFirstPost);
            }

            return new UserStateResponse(creatorStatus);
        }
    }
}