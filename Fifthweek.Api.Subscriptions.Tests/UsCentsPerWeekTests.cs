using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class UsCentsPerWeekTests : ValidatedPrimitiveEqualityTests<UsCentsPerWeek, int>
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldAllow25CentsPerWeekAndHigher()
        {
            this.GoodValue(25);
            this.GoodValue(int.MaxValue);
        }

        [TestMethod]
        public void ItShouldNotAllowLessThan25CentsPerWeek()
        {
            this.BadValue(24);
            this.BadValue(0);
            this.BadValue(-1);
            this.BadValue(int.MinValue);
        }

        protected override int ValueA
        {
            get { return 50; }
        }

        protected override int ValueB
        {
            get { return 75; }
        }

        protected override UsCentsPerWeek Parse(int value)
        {
            return UsCentsPerWeek.Parse(value);
        }

        protected override bool TryParse(int value, out UsCentsPerWeek parsedObject)
        {
            return UsCentsPerWeek.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out UsCentsPerWeek parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return UsCentsPerWeek.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(UsCentsPerWeek parsedObject)
        {
            return parsedObject.Value;
        }
    }
}