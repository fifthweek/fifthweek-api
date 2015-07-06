namespace Fifthweek.Api.Payments.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidIpAddressTests : ValidatedStringTests<ValidIpAddress>
    {
        protected override string ValueA
        {
            get
            {
                return "ABCD:ABCD:ABCD:ABCD:ABCD:ABCD:192.168.158.190";
            }
        }

        protected override string ValueB
        {
            get
            {
                return "1.1.1.1";
            }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidIpAddress.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidIpAddress.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidIpAddress.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidIpAddress.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicBlogNames()
        {
            this.GoodValue(this.ValueA);
            this.GoodValue(this.ValueB);
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldNotAllowPunctuation()
        {
            this.AssertPunctuationNotAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyShortValues()
        {
            this.GoodValue("1.1.1.1");
            this.BadValue("1.1.1");
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyLongValues()
        {
            this.AssertMaxLength(45, false);
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.AssertTabsNotAllowed(false);
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.AssertNewLinesNotAllowed(false);
        }

        protected override ValidIpAddress Parse(string value)
        {
            return ValidIpAddress.Parse(value);
        }

        protected override bool TryParse(string value, out ValidIpAddress parsedObject)
        {
            return ValidIpAddress.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidIpAddress parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidIpAddress.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidIpAddress parsedObject)
        {
            return parsedObject.Value;
        }
    }
}