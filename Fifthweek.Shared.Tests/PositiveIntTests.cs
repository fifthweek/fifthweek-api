namespace Fifthweek.Shared.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PositiveIntTests : ValidatedPrimitiveTests<PositiveInt, int>
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
        public void ItShouldAllow1Higher()
        {
            this.GoodValue(1);
            this.GoodValue(int.MaxValue);
        }

        [TestMethod]
        public void ItShouldNotAllowLessThan1()
        {
            this.BadValue(0);
            this.BadValue(-1);
            this.BadValue(int.MinValue);
        }

        protected override PositiveInt Parse(int value)
        {
            return PositiveInt.Parse(value);
        }

        protected override bool TryParse(int value, out PositiveInt parsedObject)
        {
            return PositiveInt.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(int value, out PositiveInt parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return PositiveInt.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override int GetValue(PositiveInt parsedObject)
        {
            return parsedObject.Value;
        }
    }
}