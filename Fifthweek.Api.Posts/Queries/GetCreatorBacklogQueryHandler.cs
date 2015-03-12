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
    public partial class GetCreatorBacklogQueryHandler : IQueryHandler<GetCreatorBacklogQuery, IReadOnlyList<BacklogPost>>
    {

        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetCreatorBacklogDbStatement getCreatorBacklogDbStatement;

        public async Task<IReadOnlyList<BacklogPost>> HandleAsync(GetCreatorBacklogQuery query)
        {
            query.AssertNotNull("query");

            await this.requesterSecurity.AuthenticateAsAsync(query.Requester, query.RequestedUserId);
            await this.requesterSecurity.AssertInRoleAsync(query.Requester, FifthweekRole.Creator);

            return await this.getCreatorBacklogDbStatement.ExecuteAsync(query.RequestedUserId, DateTime.UtcNow);
        }
    }
}