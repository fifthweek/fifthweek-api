namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;

    public interface IQueuedPostLiveDateCalculator
    {
        DateTime GetNextLiveDate(
            DateTime exclusiveLowerBound,
            WeeklyReleaseSchedule weeklyReleaseSchedule);

        IReadOnlyList<DateTime> GetNextLiveDates(
            DateTime exclusiveLowerBound,
            WeeklyReleaseSchedule weeklyReleaseSchedule,
            int numberOfLiveDatesToReturn);
    }
}