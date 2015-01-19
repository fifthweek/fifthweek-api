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
            var nextReleaseTimeIndex = 0; // Determined using start time.

            // each time we hit a new week (i.e. go back in time).

            throw new NotImplementedException();
        }
    }
}