namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetLiveDateOfNewQueuedPostDbStatement : IGetLiveDateOfNewQueuedPostDbStatement
    {
        private readonly IGetNewQueuedPostLiveDateLowerBoundDbStatement getNewQueuedPostLiveDateLowerBound;
        private readonly IGetWeeklyReleaseScheduleDbStatement getWeeklyReleaseSchedule;
        private readonly IQueuedPostLiveDateCalculator queuedPostLiveDateCalculator;

        public async Task<DateTime> ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");

            var exclusiveLowerBound = await this.getNewQueuedPostLiveDateLowerBound.ExecuteAsync(collectionId, DateTime.UtcNow);
            var weeklyReleaseSchedule = await this.getWeeklyReleaseSchedule.ExecuteAsync(collectionId);

            return this.queuedPostLiveDateCalculator.GetNextLiveDate(
                exclusiveLowerBound,
                weeklyReleaseSchedule);
        }
    }
}