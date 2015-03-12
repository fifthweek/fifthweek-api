namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetCreatorNewsfeedQueryHandler : IQueryHandler<GetCreatorNewsfeedQuery, IReadOnlyList<NewsfeedPost>>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetCreatorNewsfeedDbStatement getCreatorNewsfeedDbStatement;

        public async Task<IReadOnlyList<NewsfeedPost>> HandleAsync(GetCreatorNewsfeedQuery query)
        {
            query.AssertNotNull("query");

            // In future, this query will need to allow users who are subscribed to this creator too. This will require a 
            // IsAuthenticatedAs || IsSubscribedTo authorization.
            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            return await this.getCreatorNewsfeedDbStatement.ExecuteAsync(
              query.RequestedUserId,
              DateTime.UtcNow,
              query.StartIndex,
              query.Count);
        }
    }
}