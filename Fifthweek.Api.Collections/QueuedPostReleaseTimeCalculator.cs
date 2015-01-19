namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;

    public class QueuedPostReleaseTimeCalculator : IQueuedPostReleaseTimeCalculator
    {
        public DateTime GetQueuedPostReleaseTime(DateTime startTime, IReadOnlyList<WeeklyReleaseTime> ascendingWeeklyReleaseTimes, int zeroBasedQueuePosition)
        {
            ascendingWeeklyReleaseTimes.AssertNotNull("ascendingWeeklyReleaseTimes");

            if (zeroBasedQueuePosition < 0)
            {
                throw new ArgumentOutOfRangeException("zeroBasedQueuePosition");
            }

            var releasesPerWeek = ascendingWeeklyReleaseTimes.Count;
            var weeklyReleaseIndex = zeroBasedQueuePosition % releasesPerWeek;
            var weeklyReleaseTime = ascendingWeeklyReleaseTimes[weeklyReleaseIndex];

            throw new NotImplementedException();
        }
    }
}