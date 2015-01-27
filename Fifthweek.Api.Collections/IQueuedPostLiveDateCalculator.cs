namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;

    public interface IQueuedPostLiveDateCalculator
    {
        DateTime GetNextLiveDate(
            DateTime exclusiveLowerBound,
            IReadOnlyList<Shared.HourOfWeek> ascendingWeeklyReleaseTimes);

        IReadOnlyList<DateTime> GetNextLiveDates(
            DateTime exclusiveLowerBound,
            IReadOnlyList<Shared.HourOfWeek> ascendingWeeklyReleaseTimes,
            int numberOfLiveDatesToReturn);
    }
}