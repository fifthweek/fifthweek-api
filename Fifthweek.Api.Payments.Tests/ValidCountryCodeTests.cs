namespace Fifthweek.Api.Payments.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidCountryCodeTests : ValidatedStringTests<ValidCountryCode>
    {
        protected override string ValueA
        {
            get
            {
                return "US";
            }
        }

        protected override string ValueB
        {
            get
            {
                return "USA";
            }
        }

        protected override char AppendCharacter
        {
            get
            {
                return 'X';
            }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidCountryCode.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidCountryCode.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidCountryCode.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidCountryCode.IsEmpty("a");
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
            this.AssertMinLength(2, false);
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyLongValues()
        {
            this.AssertMaxLength(3, false);
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

        protected override ValidCountryCode Parse(string value)
        {
            return ValidCountryCode.Parse(value);
        }

        protected override bool TryParse(string value, out ValidCountryCode parsedObject)
        {
            return ValidCountryCode.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidCountryCode parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidCountryCode.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidCountryCode parsedObject)
        {
            return parsedObject.Value;
        }
    }
}