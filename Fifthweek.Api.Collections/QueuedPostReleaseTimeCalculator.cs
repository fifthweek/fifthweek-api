namespace Fifthweek.Api.Collections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Shared;

    public class QueuedPostReleaseTimeCalculator : IQueuedPostReleaseTimeCalculator
    {
        public DateTime GetQueuedPostReleaseTime(DateTime startTime, IReadOnlyList<WeeklyReleaseTime> ascendingWeeklyReleaseTimes, int zeroBasedQueuePosition)
        {
            ascendingWeeklyReleaseTimes.AssertNotNull("ascendingWeeklyReleaseTimes");

            if (startTime.Kind != DateTimeKind.Utc)
            {
                throw new ArgumentException("Must be UTC", "startTime");
            }

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
            var nextReleaseTimeAfterCurrentTime = ascendingWeeklyReleaseTimes.FirstOrDefault(_ => _.HourOfWeek >= currentHourOfWeek);
            var startFromNextWeek = nextReleaseTimeAfterCurrentTime == null;
            var nextReleaseTimeIndex = startFromNextWeek ? 0 : ascendingWeeklyReleaseTimes.IndexOf(nextReleaseTimeAfterCurrentTime);
            var releaseTimeIndex = (nextReleaseTimeIndex + zeroBasedQueuePosition) % releasesPerWeek;
            var releaseHourOfWeek = ascendingWeeklyReleaseTimes[releaseTimeIndex].HourOfWeek;

            var releaseWeek = startTimeWithWeekReset.AddDays(fullWeeksOfBacklog * 7);
            var releaseTime = releaseWeek.AddHours(releaseHourOfWeek);

            return releaseTime;
        }
    }
}