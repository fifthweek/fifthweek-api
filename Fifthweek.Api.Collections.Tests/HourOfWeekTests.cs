namespace Fifthweek.Api.Collections.Tests
{
    using System;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HourOfWeekTests : PrimitiveTests<HourOfWeek>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldAllowUtcDateTimes()
        {
            const int HoursIntoWeek = 42;

            var firstWeekOfFeb = new DateTime(2015, 2, 1, 0, 0, 0, DateTimeKind.Utc).AddHours(HoursIntoWeek);
            Assert.AreEqual(new HourOfWeek(firstWeekOfFeb).Value, (byte)HoursIntoWeek);

            var thirdWeekOfFeb = new DateTime(2015, 2, 15, 0, 0, 0, DateTimeKind.Utc).AddHours(HoursIntoWeek);
            Assert.AreEqual(new HourOfWeek(thirdWeekOfFeb).Value, (byte)HoursIntoWeek);
        }

        [TestMethod]
        public void ItShouldAllowValuesUnder168()
        {
            Assert.AreEqual(new HourOfWeek(0).Value, (byte)0);
            Assert.AreEqual(new HourOfWeek(167).Value, (byte)167);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldNotAllowNonUtcDateTimes()
        {
            new HourOfWeek(DateTime.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItShouldNotAllowValuesEqualTo168()
        {
            new HourOfWeek(168);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void ItShouldNotAllowValuesOver168()
        {
            new HourOfWeek(169);
        }

        protected override HourOfWeek NewInstanceOfObjectA()
        {
            return new HourOfWeek(0);
        }

        protected override HourOfWeek NewInstanceOfObjectB()
        {
            return new HourOfWeek(1);
        }
    }
}