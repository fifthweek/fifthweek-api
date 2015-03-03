namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    [Decorator(OmitDefaultDecorators = true)]
    public partial class GetUserStateQueryHandler : IQueryHandler<GetUserStateQuery, UserState>
    {
        private readonly IRequesterSecurity requesterSecurity;

        private readonly IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures> getUserAccessSignatures;
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;
        private readonly IQueryHandler<GetCreatedChannelsAndCollectionsQuery, ChannelsAndCollections> getCreatedChannelsAndCollections;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");

            if (query.RequestedUserId != null)
            {
                await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            }

            bool isCreator = await this.requesterSecurity.IsInRoleAsync(query.Requester, FifthweekRole.Creator);

            var userAccessSignatures = await this.getUserAccessSignatures.HandleAsync(new GetUserAccessSignaturesQuery(query.Requester, query.RequestedUserId));

            CreatorStatus creatorStatus = null;
            ChannelsAndCollections createdChannelsAndCollections = null;
            if (isCreator)
            {
                creatorStatus = await this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(query.Requester, query.RequestedUserId));
                createdChannelsAndCollections = await this.getCreatedChannelsAndCollections.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(query.Requester, query.RequestedUserId));
            }

            return new UserState(
                userAccessSignatures, 
                creatorStatus, 
                createdChannelsAndCollections);
        }
    }
}