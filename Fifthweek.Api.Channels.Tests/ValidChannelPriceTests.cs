namespace Fifthweek.Api.Channels.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidChannelPriceTests : ValidatedPrimitiveTests<ValidChannelPrice, int>
    {
        protected override int ValueA
        {
            get { return 50; }
        }

        protected override int ValueB
        {
            get { return 75; }
        }

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

        protected override ValidChannelPrice Parse(int value)
        {
            return ValidChannelPrice.Parse(value);
        }

        protected override bool TryParse(int value, out ValidChannelPrice parsedObject)
        {
            return ValidChannelPrice.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out ValidChannelPrice parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidChannelPrice.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(ValidChannelPrice parsedObject)
        {
            return parsedObject.Value;
        }
    }
}