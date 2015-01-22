namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetUserStateQueryHandler : IQueryHandler<GetUserStateQuery, UserState>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;
        private readonly IQueryHandler<GetCreatedChannelsAndCollectionsQuery, ChannelsAndCollections> getCreatedChannelsAndCollections;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");
            
            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);

            CreatorStatus creatorStatus = null;
            ChannelsAndCollections createdChannelsAndCollections = null;
            if (query.IsCreator)
            {
                creatorStatus = await this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(query.Requester, query.RequestedUserId));
                createdChannelsAndCollections = await this.getCreatedChannelsAndCollections.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(query.Requester, query.RequestedUserId));
            }

            return new UserState(creatorStatus, createdChannelsAndCollections);
        }
    }
}