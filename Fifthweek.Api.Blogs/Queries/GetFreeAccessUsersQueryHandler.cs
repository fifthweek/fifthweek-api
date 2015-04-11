namespace Fifthweek.Api.Blogs.Queries
{
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetFreeAccessUsersQueryHandler : IQueryHandler<GetFreeAccessUsersQuery, GetFreeAccessUsersResult>
    {
        private readonly IBlogSecurity blogSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetFreeAccessUsersDbStatement getFreeAccessUsers;

        public async Task<GetFreeAccessUsersResult> HandleAsync(GetFreeAccessUsersQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);
            await this.blogSecurity.AssertWriteAllowedAsync(authenticatedUserId, query.BlogId);

            return await this.getFreeAccessUsers.ExecuteAsync(query.BlogId);
        }
    }
}