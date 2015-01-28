namespace Fifthweek.Api.Collections
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class GetLiveDateOfNewQueuedPostDbStatement : IGetLiveDateOfNewQueuedPostDbStatement
    {
        private readonly IGetNewQueuedPostLiveDateLowerBoundDbStatement getNewQueuedPostLiveDateLowerBound;
        private readonly IGetCollectionWeeklyReleaseTimesDbStatement getCollectionWeeklyReleaseTimes;
        private readonly IQueuedPostLiveDateCalculator queuedPostLiveDateCalculator;

        public async Task<DateTime> ExecuteAsync(CollectionId collectionId)
        {
            collectionId.AssertNotNull("collectionId");

            var exclusiveLowerBound = await this.getNewQueuedPostLiveDateLowerBound.ExecuteAsync(collectionId, DateTime.UtcNow);
            var ascendingWeeklyReleaseTimes = await this.getCollectionWeeklyReleaseTimes.ExecuteAsync(collectionId);
            var ascendingHoursOfWeek = ascendingWeeklyReleaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToList();

            return this.queuedPostLiveDateCalculator.GetNextLiveDate(
                exclusiveLowerBound,
                ascendingHoursOfWeek);
        }
    }
}