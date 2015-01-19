namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Persistence;

    public interface IQueuedPostReleaseTimeCalculator
    {
        DateTime GetQueuedPostReleaseTime(
            DateTime startTime,
            IReadOnlyList<WeeklyReleaseTime> ascendingWeeklyReleaseTimes,
            int zeroBasedQueuePosition);
    }
}