namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetUserSubscriptionsQueryHandler : IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetUserSubscriptionsDbStatement getUserSubscriptions;
        
        public async Task<GetUserSubscriptionsResult> HandleAsync(GetUserSubscriptionsQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);
            var result = await this.getUserSubscriptions.ExecuteAsync(authenticatedUserId);

            return result;
        }
    }
}