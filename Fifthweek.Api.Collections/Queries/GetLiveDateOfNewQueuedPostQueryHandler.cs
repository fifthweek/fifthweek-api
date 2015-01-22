namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetLiveDateOfNewQueuedPostQueryHandler : IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime>
    {
        private readonly ICollectionSecurity collectionSecurity;
        private readonly IRequesterSecurity requesterSecurity;
        private readonly IGetLiveDateOfNewQueuedPostDbStatement getLiveDateOfNewQueuedPost;

        public async Task<DateTime> HandleAsync(GetLiveDateOfNewQueuedPostQuery query)
        {
            query.AssertNotNull("query");

            var authenticatedUserId = await this.requesterSecurity.AuthenticateAsync(query.Requester);

            // This query is only raised when a user is about to post something, so request same privileges.
            await this.collectionSecurity.AssertPostingAllowedAsync(authenticatedUserId, query.CollectionId);

            return await this.getLiveDateOfNewQueuedPost.ExecuteAsync(query.CollectionId);
        }
    }
}