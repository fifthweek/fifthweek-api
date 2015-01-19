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
            var hourOfWeek = (byte)random.Next(DaysInWeek * HoursInDay);
            return new WeeklyReleaseTime(collectionId, null, hourOfWeek);
        }

        /// <summary>
        /// Create a sorted list of release times.
        /// </summary>
        public static IReadOnlyList<WeeklyReleaseTime> GenerateSortedWeeklyReleaseTimes(Guid collectionId, int count)
        {
            var releaseTimes = new List<WeeklyReleaseTime>();
            for (byte hourOfWeek = 0; hourOfWeek < count; hourOfWeek++)
            {
                releaseTimes.Add(new WeeklyReleaseTime(collectionId, null, hourOfWeek));
            }

            return releaseTimes;
        }
    }
}