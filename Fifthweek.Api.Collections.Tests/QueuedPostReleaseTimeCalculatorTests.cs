namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class QueuedPostReleaseTimeCalculatorTests
    {
        public static readonly DateTime TuesdayNight = new DateTime(2015, 1, 20, 18, 36, 12, DateTimeKind.Utc);
        public static readonly HourOfWeek Monday = new HourOfWeek(24);
        public static readonly HourOfWeek Tuesday = new HourOfWeek(48);
        public static readonly HourOfWeek Wednesday = new HourOfWeek(72);
        public static readonly HourOfWeek Thursday = new HourOfWeek(96);
        public static readonly IReadOnlyList<HourOfWeek> ReleaseSchedule = new[]
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday
        };

        public QueuedPostReleaseTimeCalculator target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new QueuedPostReleaseTimeCalculator();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireUtcStartTime()
        {
            this.target.GetQueuedPostReleaseTime(DateTime.Now, ReleaseSchedule, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldRequireNonEmptyReleaseTimes()
        {
            this.target.GetQueuedPostReleaseTime(TuesdayNight, new HourOfWeek[0], 0);
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

            this.target.GetQueuedPostReleaseTime(TuesdayNight, nonAscendingReleaseSchedule, 0);
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

            this.target.GetQueuedPostReleaseTime(TuesdayNight, nonAscendingReleaseSchedule, 0);
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

            this.target.GetQueuedPostReleaseTime(TuesdayNight, indistinctReleaseSchedule, 0);
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

            this.target.GetQueuedPostReleaseTime(TuesdayNight, indistinctReleaseSchedule, 0);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItShouldRequireNonZeroQueuePosition()
        {
            this.target.GetQueuedPostReleaseTime(TuesdayNight, ReleaseSchedule, -1);
        }

        [TestMethod]
        public void ItShouldReturnDateInUtc()
        {
            var result = this.target.GetQueuedPostReleaseTime(TuesdayNight, ReleaseSchedule, 0);

            Assert.AreEqual(result.Kind, DateTimeKind.Utc);
        }
    }
}