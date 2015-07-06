namespace Fifthweek.Api.Payments.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidCreditCardPrefixTests : ValidatedStringTests<ValidCreditCardPrefix>
    {
        protected override string ValueA
        {
            get
            {
                return "123456";
            }
        }

        protected override string ValueB
        {
            get
            {
                return "567890";
            }
        }

        protected override char AppendCharacter
        {
            get
            {
                return '1';
            }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidCreditCardPrefix.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidCreditCardPrefix.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidCreditCardPrefix.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidCreditCardPrefix.IsEmpty("a");
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
            this.AssertMinLength(6, false);
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyLongValues()
        {
            this.AssertMaxLength(6, false);
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

        protected override ValidCreditCardPrefix Parse(string value)
        {
            return ValidCreditCardPrefix.Parse(value);
        }

        protected override bool TryParse(string value, out ValidCreditCardPrefix parsedObject)
        {
            return ValidCreditCardPrefix.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidCreditCardPrefix parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidCreditCardPrefix.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidCreditCardPrefix parsedObject)
        {
            return parsedObject.Value;
        }
    }
}