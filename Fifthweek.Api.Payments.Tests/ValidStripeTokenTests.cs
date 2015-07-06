namespace Fifthweek.Api.Payments.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidStripeTokenTests : ValidatedStringTests<ValidStripeToken>
    {
        protected override string ValueA
        {
            get
            {
                return "SomeTokenA";
            }
        }

        protected override string ValueB
        {
            get
            {
                return "SomeTokenB";
            }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidStripeToken.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidStripeToken.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidStripeToken.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidStripeToken.IsEmpty("a");
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
        public void ItShouldAllowPunctuation()
        {
            this.AssertPunctuationAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyShortValues()
        {
            this.AssertMinLength(1, false);
        }

        [TestMethod]
        public void ItShouldNotAllowOverlyLongValues()
        {
            this.AssertMaxLength(255, false);
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
        
        protected override ValidStripeToken Parse(string value)
        {
            return ValidStripeToken.Parse(value);
        }

        protected override bool TryParse(string value, out ValidStripeToken parsedObject)
        {
            return ValidStripeToken.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidStripeToken parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidStripeToken.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidStripeToken parsedObject)
        {
            return parsedObject.Value;
        }
    }
}