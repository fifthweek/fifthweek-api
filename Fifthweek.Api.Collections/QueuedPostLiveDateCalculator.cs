namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    public class QueuedPostLiveDateCalculator : IQueuedPostLiveDateCalculator
    {
        public DateTime GetNextLiveDate(DateTime exclusiveLowerBound, IReadOnlyList<Shared.HourOfWeek> ascendingWeeklyReleaseTimes)
        {
            return this.GetNextLiveDates(exclusiveLowerBound, ascendingWeeklyReleaseTimes, 1)[0];
        }

        public IReadOnlyList<DateTime> GetNextLiveDates(
            DateTime exclusiveLowerBound,
            IReadOnlyList<Shared.HourOfWeek> ascendingWeeklyReleaseTimes,
            int numberOfLiveDatesToReturn)
        {
            exclusiveLowerBound.AssertUtc("exclusiveLowerBound");
            ascendingWeeklyReleaseTimes.AssertNotNull("ascendingWeeklyReleaseTimes");
            ascendingWeeklyReleaseTimes.AssertNonEmpty("ascendingWeeklyReleaseTimes");
            numberOfLiveDatesToReturn.AssertNonNegative("numberOfLiveDatesToReturn");

            ascendingWeeklyReleaseTimes.Aggregate((previous, current) =>
            {
                if (previous.Value >= current.Value)
                {
                    throw new ArgumentException("Must be in ascending order with no duplicates", "ascendingWeeklyReleaseTimes");
                }

                return current;
            });

            var startTimeWithWeekReset = new DateTime(
                exclusiveLowerBound.Year,
                exclusiveLowerBound.Month,
                exclusiveLowerBound.Day,
                0,
                0,
                0,
                DateTimeKind.Utc)
                .AddDays(-1 * (int)exclusiveLowerBound.DayOfWeek);

            var releasesPerWeek = ascendingWeeklyReleaseTimes.Count;
            var currentHourOfWeek = new Shared.HourOfWeek(exclusiveLowerBound).Value;
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