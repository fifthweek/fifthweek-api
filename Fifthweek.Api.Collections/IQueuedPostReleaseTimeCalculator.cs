namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;

    public interface IQueuedPostReleaseTimeCalculator
    {
        DateTime GetQueuedPostReleaseTime(
            DateTime startTime,
            IReadOnlyList<HourOfWeek> ascendingWeeklyReleaseTimes,
            int zeroBasedQueuePosition);
    }
}