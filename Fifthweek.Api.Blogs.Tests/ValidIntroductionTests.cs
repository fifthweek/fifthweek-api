namespace Fifthweek.Api.Blogs.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidIntroductionTests : ValidatedStringTests<ValidIntroduction>
    {
        public static readonly string InvalidValue = "!";

        protected override string ValueA
        {
            get { return "Hi, I'm Lawrence. Thinking of subscribing? Awesome!"; }
        }

        protected override string ValueB
        {
            get { return "Hi, I'm James. Thinking of subscribing? Awesome!"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidIntroduction.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidIntroduction.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidIntroduction.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidIntroduction.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicIntroductions()
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
        public void ItShouldNotAllowIntroductionsUnder15Characters()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowIntroductionsOver250Characters()
        {
            this.AssertMaxLength(250);
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.AssertTabsNotAllowed();
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.AssertNewLinesNotAllowed();
        }

        protected override ValidIntroduction Parse(string value)
        {
            return ValidIntroduction.Parse(value);
        }

        protected override bool TryParse(string value, out ValidIntroduction parsedObject)
        {
            return ValidIntroduction.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidIntroduction parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidIntroduction.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidIntroduction parsedObject)
        {
            return parsedObject.Value;
        }
    }
}