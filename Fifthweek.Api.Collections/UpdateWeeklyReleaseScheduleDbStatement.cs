namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoConstructor]
    public partial class UpdateWeeklyReleaseScheduleDbStatement : IUpdateWeeklyReleaseScheduleDbStatement
    {
        private readonly IReplaceWeeklyReleaseTimesDbStatement replaceWeeklyReleaseTimes;
        private readonly IDefragmentQueueDbStatement defragmentQueue;

        public async Task ExecuteAsync(CollectionId collectionId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now)
        {
            collectionId.AssertNotNull("collectionId");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");
            now.AssertUtc("now");

            // Transaction required on the following, as we don't want user to see a queue that does not match the collection's release times.
            using (var transaction = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await this.replaceWeeklyReleaseTimes.ExecuteAsync(collectionId, weeklyReleaseSchedule);
                await this.defragmentQueue.ExecuteAsync(collectionId, weeklyReleaseSchedule, now);

                transaction.Complete();
            }
        }
    }
}