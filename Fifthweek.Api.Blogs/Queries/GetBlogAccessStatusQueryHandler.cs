namespace Fifthweek.Api.Blogs.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetBlogAccessStatusQueryHandler : IQueryHandler<GetBlogAccessStatusQuery, GetBlogAccessStatusResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IDoesUserHaveFreeAccessDbStatement doesUserHaveFreeAccess;
        
        public async Task<GetBlogAccessStatusResult> HandleAsync(GetBlogAccessStatusQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);
            var userHasFreeAccess = await this.doesUserHaveFreeAccess.ExecuteAsync(query.BlogId, authenticatedUserId);

            return new GetBlogAccessStatusResult(userHasFreeAccess);
        }
    }
}