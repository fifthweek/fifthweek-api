namespace Fifthweek.Api.Collections
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class DefragmentQueueDbStatement : IDefragmentQueueDbStatement
    {
        private readonly IGetQueueSizeDbStatement getQueueSize;
        private readonly IQueuedPostLiveDateCalculator liveDateCalculator;
        private readonly IUpdateAllLiveDatesInQueueDbStatement updateAllLiveDatesInQueue;

        public async Task ExecuteAsync(QueueId queueId, WeeklyReleaseSchedule weeklyReleaseSchedule, DateTime now)
        {
            queueId.AssertNotNull("queueId");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");
            now.AssertUtc("now");

            var queueSize = await this.getQueueSize.ExecuteAsync(queueId, now);

            if (queueSize == 0)
            {
                return;
            }

            var unfragmentedLiveDates = this.liveDateCalculator.GetNextLiveDates(now, weeklyReleaseSchedule, queueSize);
            
            await this.updateAllLiveDatesInQueue.ExecuteAsync(queueId, unfragmentedLiveDates, now);
        }
    }
}