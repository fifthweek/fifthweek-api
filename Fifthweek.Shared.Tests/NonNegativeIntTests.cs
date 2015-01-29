namespace Fifthweek.Shared.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class NonNegativeIntTests : ValidatedPrimitiveTests<NonNegativeInt, int>
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
        public void ItShouldAllow0Higher()
        {
            this.GoodValue(0);
            this.GoodValue(int.MaxValue);
        }

        [TestMethod]
        public void ItShouldNotAllowLessThan0()
        {
            this.BadValue(-1);
            this.BadValue(int.MinValue);
        }

        protected override NonNegativeInt Parse(int value)
        {
            return NonNegativeInt.Parse(value);
        }

        protected override bool TryParse(int value, out NonNegativeInt parsedObject)
        {
            return NonNegativeInt.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out NonNegativeInt parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return NonNegativeInt.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(NonNegativeInt parsedObject)
        {
            return parsedObject.Value;
        }
    }
}