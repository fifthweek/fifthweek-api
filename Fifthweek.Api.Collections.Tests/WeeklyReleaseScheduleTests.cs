namespace Fifthweek.Api.Collections.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class WeeklyReleaseScheduleTests : ValidatedPrimitiveTests<WeeklyReleaseSchedule, IReadOnlyList<HourOfWeek>>
    {
        protected override IReadOnlyList<HourOfWeek> ValueA
        {
            get { return new[] { HourOfWeek.Parse(0), HourOfWeek.Parse(24) }; }
        }

        protected override IReadOnlyList<HourOfWeek> ValueB
        {
            get { return new[] { HourOfWeek.Parse(24), HourOfWeek.Parse(48) }; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = WeeklyReleaseSchedule.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyListAsEmpty()
        {
            var result = WeeklyReleaseSchedule.IsEmpty(new HourOfWeek[0]);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatListWithAtLeast1ElementAsNonEmpty()
        {
            var result = WeeklyReleaseSchedule.IsEmpty(new HourOfWeek[1]);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowUpTo168Elements()
        {
            var buffer = new List<HourOfWeek>();
            for (var i = 0; i < 168; i++)
            {
                buffer.Add(HourOfWeek.Parse((byte)i));
                this.GoodValue(buffer);
            }
        }

        // Redundant combined with unique element check, but run for completeness.
        [TestMethod]
        public void ItShouldNotAllowOver168Elements()
        {
            var buffer = new List<HourOfWeek>();
            for (var i = 0; i < 169; i++)
            {
                buffer.Add(HourOfWeek.Parse((byte)(i % HourOfWeek.MaxValue)));
            }

            this.BadValue(buffer);
        }

        [TestMethod]
        public void ItShouldNotAllowEmptyValues()
        {
            this.BadValue(new HourOfWeek[0]);
        }

        [TestMethod]
        public void ItShouldNotAllowDuplicateValues()
        {
            this.BadValue(new[] { HourOfWeek.Parse(24), HourOfWeek.Parse(24) });
            this.BadValue(new[] { HourOfWeek.Parse(0), HourOfWeek.Parse(24), HourOfWeek.Parse(25), HourOfWeek.Parse(24) });
            this.BadValue(new[] { HourOfWeek.Parse(0), HourOfWeek.Parse(24), HourOfWeek.Parse(25), HourOfWeek.Parse(24), HourOfWeek.Parse(26) });
        }

        protected override WeeklyReleaseSchedule Parse(IReadOnlyList<HourOfWeek> value)
        {
            return WeeklyReleaseSchedule.Parse(value);
        }

        protected override bool TryParse(IReadOnlyList<HourOfWeek> value, out WeeklyReleaseSchedule parsedObject)
        {
            return WeeklyReleaseSchedule.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(IReadOnlyList<HourOfWeek> value, out WeeklyReleaseSchedule parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return WeeklyReleaseSchedule.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override IReadOnlyList<HourOfWeek> GetValue(WeeklyReleaseSchedule parsedObject)
        {
            return parsedObject.Value;
        }
    }
}