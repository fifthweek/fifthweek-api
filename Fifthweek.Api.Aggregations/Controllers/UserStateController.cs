namespace Fifthweek.Api.Aggregations.Controllers
{
    using System.Linq;
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
            
            UserStateResponse.CreatorStatusResponse creatorStatus = null;
            if (userState.CreatorStatus != null)
            {
                creatorStatus = new UserStateResponse.CreatorStatusResponse(
                    userState.CreatorStatus.SubscriptionId == null
                        ? null
                        : userState.CreatorStatus.SubscriptionId.Value.EncodeGuid(),
                    userState.CreatorStatus.MustWriteFirstPost);
            }

            UserStateResponse.ChannelsAndCollectionsResponse createdChannelsAndCollections = null;
            if (userState.CreatedChannelsAndCollections != null)
            {
                createdChannelsAndCollections = new UserStateResponse.ChannelsAndCollectionsResponse(
                    userState.CreatedChannelsAndCollections.Channels.Select(v => new UserStateResponse.ChannelsAndCollectionsResponse.ChannelResponse(
                        v.ChannelId.Value.EncodeGuid(),
                        v.Name,
                        v.Collections.Select(w => new UserStateResponse.ChannelsAndCollectionsResponse.CollectionResponse(
                            w.CollectionId.Value.EncodeGuid(),
                            w.Name)).ToList())).ToList());
            }

            return new UserStateResponse(creatorStatus, createdChannelsAndCollections);
        }
    }
}