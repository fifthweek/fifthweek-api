namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Fifthweek.Api.Collections.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueuedPostLiveDateCalculatorTests
    {
        private static readonly HourOfWeek Sunday = HourOfWeek.Parse(0); // Minimum hour of week.
        private static readonly HourOfWeek Monday = HourOfWeek.Parse(24);
        private static readonly HourOfWeek Tuesday = HourOfWeek.Parse(48);
        private static readonly HourOfWeek Wednesday = HourOfWeek.Parse(72);
        private static readonly HourOfWeek Thursday = HourOfWeek.Parse(96);
        private static readonly HourOfWeek SaturdayNight = HourOfWeek.Parse(167); // Maximum hour of week.

        private static readonly IReadOnlyList<HourOfWeek> ScheduleStartOfWeek = new[]
        {
            Sunday
        };

        private static readonly IReadOnlyList<HourOfWeek> ScheduleEndOfWeek = new[]
        {
            SaturdayNight
        };

        private static readonly IReadOnlyList<HourOfWeek> ScheduleMidWeek = new[]
        {
            Wednesday
        };

        private static readonly IReadOnlyList<HourOfWeek> ScheduleForManyDays = new[]
        {
            Sunday,
            Monday,
            Tuesday,
            Wednesday,
            Thursday, 
            SaturdayNight
        };

        private static readonly IReadOnlyList<IReadOnlyList<HourOfWeek>> Schedules = new[]
        {
            ScheduleStartOfWeek,
            ScheduleEndOfWeek,
            ScheduleMidWeek,
            ScheduleForManyDays
        };

        private static readonly Random Random = new Random();
        private static readonly IEnumerable<DateTime> DaysInJanuary = DaysInMonth(Random, 1, 31, 2015);
        private static readonly IEnumerable<DateTime> DaysInFebruary = DaysInMonth(Random, 2, 28, 2015);
        private static readonly IEnumerable<DateTime> DaysInFebruaryLeap = DaysInMonth(Random, 2, 29, 2020);
        private static readonly IEnumerable<DateTime> DaysInApril = DaysInMonth(Random, 4, 30, 2015);
        private static readonly IEnumerable<DateTime> DaysInDecember = DaysInMonth(Random, 12, 31, 2015);
        private static readonly IReadOnlyList<DateTime> Dates =
            DaysInJanuary
            .Concat(DaysInFebruary)
            .Concat(DaysInFebruaryLeap)
            .Concat(DaysInApril)
            .Concat(DaysInDecember)
            .ToList();

        private QueuedPostLiveDateCalculator target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new QueuedPostLiveDateCalculator();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireUtcStartTime()
        {
            foreach (var schedule in Schedules)
            {
                this.target.GetNextLiveDates(DateTime.Now, schedule, 1);    
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireNonEmptyReleaseTimes()
        {
            foreach (var date in Dates)
            {
                this.target.GetNextLiveDates(date, new HourOfWeek[0], 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireAscendingReleaseTimes()
        {
            var nonAscendingReleaseSchedule = new[]
            {
                Tuesday,
                Monday,
                Wednesday,
                Thursday
            };

            foreach (var date in Dates)
            {
                this.target.GetNextLiveDates(date, nonAscendingReleaseSchedule, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireAscendingReleaseTimes2()
        {
            var nonAscendingReleaseSchedule = new[]
            {
                Monday,
                Tuesday,
                Thursday,
                Wednesday
            };

            foreach (var date in Dates)
            {
                this.target.GetNextLiveDates(date, nonAscendingReleaseSchedule, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireDistinctReleaseTimes()
        {
            var indistinctReleaseSchedule = new[]
            {
                Monday,
                Monday,
                Tuesday,
                Wednesday,
                Thursday
            };

            foreach (var date in Dates)
            {
                this.target.GetNextLiveDates(date, indistinctReleaseSchedule, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireDistinctReleaseTimes2()
        {
            var indistinctReleaseSchedule = new[]
            {
                Monday,
                Tuesday,
                Wednesday,
                Thursday,
                Thursday
            };

            foreach (var date in Dates)
            {
                this.target.GetNextLiveDates(date, indistinctReleaseSchedule, 1);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItShouldRequireNonNegativeCount()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    this.target.GetNextLiveDates(date, schedule, -1);
                }
            }
        }

        [TestMethod]
        public void ItShouldAllowZeroCount()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    this.target.GetNextLiveDates(date, schedule, 0);
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnListWithSameSizeAsCount()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    Assert.AreEqual(0, this.target.GetNextLiveDates(date, schedule, 0).Count);
                    Assert.AreEqual(1, this.target.GetNextLiveDates(date, schedule, 1).Count);
                    Assert.AreEqual(10, this.target.GetNextLiveDates(date, schedule, 10).Count);
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnDatesInUtc()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    foreach (var result in this.target.GetNextLiveDates(date, schedule, 10))
                    {
                        Assert.AreEqual(result.Kind, DateTimeKind.Utc);
                    }
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnDatesClippedToTheHour()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    foreach (var result in this.target.GetNextLiveDates(date, schedule, 10))
                    {
                        var timeWithHourPrecision = new DateTime(
                            result.Year,
                            result.Month,
                            result.Day,
                            result.Hour,
                            minute: 0,
                            second: 0,
                            kind: DateTimeKind.Utc);

                        Assert.AreEqual(result, timeWithHourPrecision);
                    }
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnDatesGreaterThanTheLowerBound()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    foreach (var result in this.target.GetNextLiveDates(date, schedule, 10))
                    {
                        Assert.IsTrue(result > date);
                    }
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnDatesInAnIncreasingSequence()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    var result = this.target.GetNextLiveDates(date, schedule, 10);

                    result.Aggregate(
                        date,
                        (previous, current) =>
                            {
                                Assert.IsTrue(current > previous);
                                return current;
                            });
                }
            }
        }

        [TestMethod]
        public void ItShouldReturnDatesThatAlignWithReleaseTimes()
        {
            foreach (var schedule in Schedules)
            {
                foreach (var date in Dates)
                {
                    var results = this.target.GetNextLiveDates(date, schedule, 10);

                    var expectedFirstReleaseTime = 0;
                    var currentHourOfWeek = HourOfWeek.Parse(date);
                    for (var i = 0; i < schedule.Count; i++)
                    {
                        // If this never evaluates, it means the current date is beyond any scheduled date for this week,
                        // so we wrap around to the start of next week, which is 0 (initialized above).
                        if (currentHourOfWeek.Value < schedule[i].Value)
                        {
                            expectedFirstReleaseTime = i;
                            break;
                        }
                    }

                    for (var i = 0; i < results.Count; i++)
                    {
                        var expectedWeeklyReleaseTime = schedule[(i + expectedFirstReleaseTime) % schedule.Count];
                        var actualWeeklyReleaseTime = HourOfWeek.Parse(results[i]);

                        Assert.AreEqual(expectedWeeklyReleaseTime, actualWeeklyReleaseTime);
                    }
                }
            }
        }

        private static IEnumerable<DateTime> DaysInMonth(Random random, int oneBasedMonthIndex, int daysInMonth, int year)
        {
            for (var i = 1; i <= daysInMonth; i++)
            {
                // No precision. Also represents start of day / week / month / year (when month index is 1)
                yield return new DateTime(year, oneBasedMonthIndex, i, 0, 0, 0, DateTimeKind.Utc);

                // With precision.
                yield return new DateTime(year, oneBasedMonthIndex, i, random.Next(24), random.Next(60), random.Next(60), DateTimeKind.Utc);

                // Ensure we hit end of the year (when month index is 12).
                yield return new DateTime(year, oneBasedMonthIndex, i, 23, 59, 59, DateTimeKind.Utc);
            }
        }
    }
}