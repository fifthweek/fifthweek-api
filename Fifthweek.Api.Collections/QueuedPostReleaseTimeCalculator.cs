namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Core;
    using Fifthweek.Shared;

    public class QueuedPostReleaseTimeCalculator : IQueuedPostReleaseTimeCalculator
    {
        public DateTime GetQueuedPostReleaseTime(DateTime startTime, IReadOnlyList<HourOfWeek> ascendingWeeklyReleaseTimes, int zeroBasedQueuePosition)
        {
            ascendingWeeklyReleaseTimes.AssertNotNull("ascendingWeeklyReleaseTimes");

            if (startTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Must be UTC", "startTime");
            }

            if (ascendingWeeklyReleaseTimes.Count == 0)
            {
                throw new ArgumentException("Must be non-empty", "ascendingWeeklyReleaseTimes");
            }

            ascendingWeeklyReleaseTimes.Aggregate((previous, current) =>
            {
                if (previous.Value >= current.Value)
                {
                    throw new ArgumentException("Must be in ascending order with no duplicates", "ascendingWeeklyReleaseTimes");
                }

                return current;
            });

            if (zeroBasedQueuePosition < 0)
            {
                throw new ArgumentOutOfRangeException("zeroBasedQueuePosition");
            }

            var startTimeWithWeekReset = new DateTime(
                startTime.Year,
                startTime.Month,
                (startTime.Day / 7) * 7,
                0,
                0,
                0,
                DateTimeKind.Utc);

            var releasesPerWeek = ascendingWeeklyReleaseTimes.Count;
            var fullWeeksOfBacklog = zeroBasedQueuePosition / releasesPerWeek;

            var currentHourOfWeek = new HourOfWeek(startTime).Value;
            var nextReleaseTimeAfterCurrentTime = ascendingWeeklyReleaseTimes.FirstOrDefault(_ => _.Value >= currentHourOfWeek);
            var startFromNextWeek = nextReleaseTimeAfterCurrentTime == null;
            var nextReleaseTimeIndex = startFromNextWeek ? 0 : ascendingWeeklyReleaseTimes.IndexOf(nextReleaseTimeAfterCurrentTime);
            var releaseTimeIndex = (nextReleaseTimeIndex + zeroBasedQueuePosition) % releasesPerWeek;
            var releaseHourOfWeek = ascendingWeeklyReleaseTimes[releaseTimeIndex].Value;

            var releaseWeek = startTimeWithWeekReset.AddDays(fullWeeksOfBacklog * 7);
            var releaseTime = releaseWeek.AddHours(releaseHourOfWeek);

            return releaseTime;
        }
    }
}