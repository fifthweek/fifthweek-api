namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Shared;

    public class QueuedPostLiveDateCalculator : IQueuedPostLiveDateCalculator
    {
        public DateTime GetNextLiveDate(DateTime exclusiveLowerBound, WeeklyReleaseSchedule weeklyReleaseSchedule)
        {
            return this.GetNextLiveDates(exclusiveLowerBound, weeklyReleaseSchedule, 1)[0];
        }

        public IReadOnlyList<DateTime> GetNextLiveDates(
            DateTime exclusiveLowerBound,
            WeeklyReleaseSchedule weeklyReleaseSchedule,
            int numberOfLiveDatesToReturn)
        {
            exclusiveLowerBound.AssertUtc("exclusiveLowerBound");
            weeklyReleaseSchedule.AssertNotNull("weeklyReleaseSchedule");
            numberOfLiveDatesToReturn.AssertNonNegative("numberOfLiveDatesToReturn");

            var startTimeWithWeekReset = new DateTime(
                exclusiveLowerBound.Year,
                exclusiveLowerBound.Month,
                exclusiveLowerBound.Day,
                0,
                0,
                0,
                DateTimeKind.Utc)
                .AddDays(-1 * (int)exclusiveLowerBound.DayOfWeek);

            var ascendingWeeklyReleaseTimes = weeklyReleaseSchedule.Value;
            var releasesPerWeek = ascendingWeeklyReleaseTimes.Count;
            var currentHourOfWeek = HourOfWeek.Parse(exclusiveLowerBound).Value;
            var nextReleaseTimeAfterCurrentTime = ascendingWeeklyReleaseTimes.FirstOrDefault(_ => _.Value > currentHourOfWeek);
            var startFromNextWeek = nextReleaseTimeAfterCurrentTime == null;
            var releaseTimeIndexOffset = startFromNextWeek ? releasesPerWeek : ascendingWeeklyReleaseTimes.IndexOf(nextReleaseTimeAfterCurrentTime);

            var result = new List<DateTime>();
            for (var i = releaseTimeIndexOffset; i < numberOfLiveDatesToReturn + releaseTimeIndexOffset; i++)
            {
                var weeksFilled = i / releasesPerWeek;
                var releaseTimeIndex = i % releasesPerWeek;
                var releaseHourOfWeek = ascendingWeeklyReleaseTimes[releaseTimeIndex].Value;

                var releaseWeek = startTimeWithWeekReset.AddDays(weeksFilled * 7);
                var releaseTime = releaseWeek.AddHours(releaseHourOfWeek);

                result.Add(releaseTime);
            }

            return result;
        }
    }
}