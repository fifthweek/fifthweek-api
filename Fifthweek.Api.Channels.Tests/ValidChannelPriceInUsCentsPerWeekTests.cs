namespace Fifthweek.Api.Channels.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    

    [TestClass]
    public class ValidChannelPriceInUsCentsPerWeekTests : ValidatedPrimitiveTests<ValidChannelPriceInUsCentsPerWeek, int>
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

        protected override ValidChannelPriceInUsCentsPerWeek Parse(int value, bool exact)
        {
            return ValidChannelPriceInUsCentsPerWeek.Parse(value);
        }

        protected override bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek parsedObject, bool exact)
        {
            return ValidChannelPriceInUsCentsPerWeek.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out ValidChannelPriceInUsCentsPerWeek parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidChannelPriceInUsCentsPerWeek.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(ValidChannelPriceInUsCentsPerWeek parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly int InvalidValue = 0;
    }
}