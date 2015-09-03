namespace Fifthweek.Api.Posts.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class GetCommentsQueryHandler : IQueryHandler<GetCommentsQuery, CommentsResult>
    {
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IPostSecurity postSecurity;
        private readonly IGetCommentsDbStatement getComments;
        
        public async Task<CommentsResult> HandleAsync(GetCommentsQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            // The assumption here is you can only read comments if you can also write them.
            await this.postSecurity.AssertCommentOrLikeAllowedAsync(authenticatedUserId, query.PostId);

            return await this.getComments.ExecuteAsync(query.PostId);
        }
    }
}