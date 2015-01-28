﻿namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class HourOfWeekTests : ValidatedPrimitiveTests<HourOfWeek, byte>
    {
        protected override byte ValueA
        {
            get { return 50; }
        }

        protected override byte ValueB
        {
            get { return 75; }
        }

        [TestMethod]
        public void ItShouldAllowUtcDateTimes()
        {
            var firstSundayInFeb = new DateTime(2015, 2, 1, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(HourOfWeek.Parse(firstSundayInFeb).Value, (byte)0);

            var thirdSundayInFeb = new DateTime(2015, 2, 15, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(HourOfWeek.Parse(thirdSundayInFeb).Value, (byte)0);

            var secondWednesdayInFeb = new DateTime(2015, 2, 11, 0, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(HourOfWeek.Parse(secondWednesdayInFeb).Value, (byte)72);

            var secondWednesdayInFebMidday = new DateTime(2015, 2, 11, 12, 0, 0, DateTimeKind.Utc);
            Assert.AreEqual(HourOfWeek.Parse(secondWednesdayInFebMidday).Value, (byte)84);
        }

        [TestMethod]
        public void ItShouldAllowValuesUnder168()
        {
            this.GoodValue(0);
            this.GoodValue(167);
        }

        [TestMethod]
        public void ItShouldNotAllowValuesEqualTo168()
        {
            this.BadValue(168);
        }

        [TestMethod]
        public void ItShouldNotAllowValuesOver168()
        {
            this.BadValue(169);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ItShouldNotAllowNonUtcDateTimes()
        {
            HourOfWeek.Parse(DateTime.Now);
        }

        protected override HourOfWeek Parse(byte value, bool exact)
        {
            return HourOfWeek.Parse(value);
        }

        protected override bool TryParse(byte value, out HourOfWeek parsedObject, bool exact)
        {
            return HourOfWeek.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(byte value, out HourOfWeek parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return HourOfWeek.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override byte GetValue(HourOfWeek parsedObject)
        {
            return parsedObject.Value;
        }
    }
}