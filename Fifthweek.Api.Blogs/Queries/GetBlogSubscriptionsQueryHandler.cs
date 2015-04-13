namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogSubscriptionsQueryHandler : IQueryHandler<GetBlogSubscriptionsQuery, GetBlogSubscriptionsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetBlogSubscriptionsDbStatement getBlogSubscriptions;
        
        public async Task<GetBlogSubscriptionsResult> HandleAsync(GetBlogSubscriptionsQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);
            var result = await this.getBlogSubscriptions.ExecuteAsync(authenticatedUserId);

            return result;
        }
    }
}