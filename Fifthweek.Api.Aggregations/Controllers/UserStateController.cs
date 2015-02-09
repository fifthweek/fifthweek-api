namespace Fifthweek.Api.Aggregations.Controllers
{
    using System.Threading.Tasks;
    using System.Web.Http;

    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [RoutePrefix("userState")]
    public partial class UserStateController : ApiController
    {
        private readonly IQueryHandler<GetUserStateQuery, UserState> getUserState;

        private readonly IRequesterContext requesterContext;

        [Route("{userId}")]
        public async Task<UserState> GetUserState(string userId)
        {
            var requestedUserId = new UserId(userId.DecodeGuid());
            var requester = this.requesterContext.GetRequester();

            var userState = await this.getUserState.HandleAsync(new GetUserStateQuery(requester, requestedUserId));

            return userState;
        }

        [Route("")]
        public async Task<UserState> GetVisitorState()
        {
            var requester = this.requesterContext.GetRequester();

            var userState = await this.getUserState.HandleAsync(new GetUserStateQuery(requester, null));

            return userState;
        }
    }
}