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
        private readonly ICountQueuedPostsInCollectionDbStatement countQueuedPostsInCollection;
        private readonly IGetCollectionWeeklyReleaseTimesDbStatement getCollectionWeeklyReleaseTimes;
        private readonly IQueuedPostReleaseTimeCalculator queuedPostReleaseTimeCalculator;

        public async Task<DateTime> HandleAsync(GetLiveDateOfNewQueuedPostQuery query)
        {
            query.AssertNotNull("query");

            UserId authenticatedUserId;
            query.Requester.AssertAuthenticated(out authenticatedUserId);

            // This query is only raised when a user is about to post something, so request same privileges.
            await this.collectionSecurity.AssertPostingAllowedAsync(authenticatedUserId, query.CollectionId);

            return await this.GetLiveDateAsync(query.CollectionId);
        }

        private async Task<DateTime> GetLiveDateAsync(CollectionId collectionId)
        {
            var hypotheticalNewPostQueuePosition = await this.countQueuedPostsInCollection.ExecuteAsync(collectionId);
            var ascendingWeeklyReleaseTimes = await this.getCollectionWeeklyReleaseTimes.ExecuteAsync(collectionId);

            return this.queuedPostReleaseTimeCalculator.GetQueuedPostReleaseTime(
                DateTime.UtcNow,
                ascendingWeeklyReleaseTimes, 
                hypotheticalNewPostQueuePosition);
        }
    }
}