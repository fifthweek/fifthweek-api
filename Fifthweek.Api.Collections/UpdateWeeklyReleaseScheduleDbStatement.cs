namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;
    using System.Transactions;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class UpdateWeeklyReleaseScheduleDbStatement : IUpdateWeeklyReleaseScheduleDbStatement
    {
        private readonly IReplaceWeeklyReleaseTimesDbStatement replaceWeeklyReleaseTimes;
        private readonly IDefragmentQueueDbStatement defragmentQueue;

        public async Task ExecuteAsync(QueueId queueId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now)
        {
            queueId.AssertNotNull("queueId");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");
            now.AssertUtc("now");

            // Transaction required on the following, as we don't want user to see a queue that does not match the collection's release times.
            using (var transaction = TransactionScopeBuilder.CreateAsync())
            {
                await this.replaceWeeklyReleaseTimes.ExecuteAsync(queueId, weeklyReleaseSchedule);
                await this.defragmentQueue.ExecuteAsync(queueId, weeklyReleaseSchedule, now);

                transaction.Complete();
            }
        }
    }
}