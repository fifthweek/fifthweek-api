namespace Fifthweek.Api.Aggregations.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetUserStateQueryHandler : IQueryHandler<GetUserStateQuery, UserState>
    {
        private readonly IQueryHandler<GetCreatorStatusQuery, CreatorStatus> getCreatorStatus;

        public async Task<UserState> HandleAsync(GetUserStateQuery query)
        {
            query.AssertNotNull("query");
            query.Requester.AssertAuthorizedFor(query.RequestedUserId);

            CreatorStatus creatorStatus = null;
            if (query.IsCreator)
            {
                creatorStatus = await this.getCreatorStatus.HandleAsync(new GetCreatorStatusQuery(query.Requester));
            }

            return new UserState(creatorStatus);
        }
    }
}