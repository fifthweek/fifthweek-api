namespace Fifthweek.Api.Collections.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

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
            await this.collectionSecurity.AssertWriteAllowedAsync(authenticatedUserId, query.CollectionId);

            return await this.getLiveDateOfNewQueuedPost.ExecuteAsync(query.CollectionId);
        }
    }
}