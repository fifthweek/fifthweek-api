namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Collections.Generic;

    public class WeeklyReleaseTimeTests
    {
        private const int DaysInWeek = 7;
        private const int HoursInDay = 24;

        public static WeeklyReleaseTime UniqueEntity(Random random, Guid collectionId)
        {
            var dayOfWeek = (byte)random.Next(7);
            var timeOfDay = new TimeSpan(random.Next(24), random.Next(60), random.Next(60));
            return new WeeklyReleaseTime(collectionId, null, dayOfWeek, timeOfDay);
        }

        /// <summary>
        /// Create a sorted list of 36 release times using the most basic method possible.
        /// </summary>
        public static IReadOnlyList<WeeklyReleaseTime> GenerateSortedWeeklyReleaseTimes(Guid collectionId)
        {
            var releaseTimes = new List<WeeklyReleaseTime>();
            for (var dayOfWeek = 0; dayOfWeek < DaysInWeek; dayOfWeek++)
            {
                if (dayOfWeek % 2 == 1)
                {
                    // 3 days (1, 3, 5)
                    continue;
                }

                for (var hourOfDay = 0; hourOfDay < HoursInDay; hourOfDay++)
                {
                    if (hourOfDay % 2 == 1)
                    {
                        // 12 hours (1, 3, ..., 23)
                        continue;
                    }

                    releaseTimes.Add(new WeeklyReleaseTime(collectionId, null, (byte)dayOfWeek, new TimeSpan(hourOfDay, 0, 0)));
                }
            }

            return releaseTimes;
        }
    }
}