using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class ChannelPriceInUsCentsPerWeekTests : ValidatedPrimitiveEqualityTests<ChannelPriceInUsCentsPerWeek, int>
    {
        [TestMethod]
        public void ItShouldAllow1CentsPerWeekAndHigher()
        {
            this.GoodValue(1);
            this.GoodValue(25);
            this.GoodValue(int.MaxValue);
        }

        [TestMethod]
        public void ItShouldNotAllowLessThan1CentsPerWeek()
        {
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

        protected override ChannelPriceInUsCentsPerWeek Parse(int value, bool exact)
        {
            return ChannelPriceInUsCentsPerWeek.Parse(value);
        }

        protected override bool TryParse(int value, out ChannelPriceInUsCentsPerWeek parsedObject, bool exact)
        {
            return ChannelPriceInUsCentsPerWeek.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out ChannelPriceInUsCentsPerWeek parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ChannelPriceInUsCentsPerWeek.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(ChannelPriceInUsCentsPerWeek parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly int InvalidValue = 0;
    }
}