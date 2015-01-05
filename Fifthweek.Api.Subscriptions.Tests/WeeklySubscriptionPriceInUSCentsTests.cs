using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class WeeklySubscriptionPriceInUSCentsTests : ValidatedCustomPrimitiveTypeTests<WeeklySubscriptionPriceInUSCents, int>
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

        protected override WeeklySubscriptionPriceInUSCents Parse(int value)
        {
            return WeeklySubscriptionPriceInUSCents.Parse(value);
        }

        protected override bool TryParse(int value, out WeeklySubscriptionPriceInUSCents parsedObject)
        {
            return WeeklySubscriptionPriceInUSCents.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out WeeklySubscriptionPriceInUSCents parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return WeeklySubscriptionPriceInUSCents.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(WeeklySubscriptionPriceInUSCents parsedObject)
        {
            return parsedObject.Value;
        }
    }
}