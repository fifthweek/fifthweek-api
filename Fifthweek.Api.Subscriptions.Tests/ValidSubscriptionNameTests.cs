namespace Fifthweek.Api.Blogs.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidSubscriptionNameTests : ValidatedStringTests<ValidBlogName>
    {
        public static readonly string InvalidValue = string.Empty;

        protected override string ValueA
        {
            get { return "Lawrence"; }
        }

        protected override string ValueB
        {
            get { return "James"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidBlogName.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidBlogName.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidBlogName.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidBlogName.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicSubscriptionNames()
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
        public void ItShouldNotAllowEmptySubscriptionNames()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowSubscriptionNamesOver25Characters()
        {
            this.AssertMaxLength(25);
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

        protected override ValidBlogName Parse(string value)
        {
            return ValidBlogName.Parse(value);
        }

        protected override bool TryParse(string value, out ValidBlogName parsedObject)
        {
            return ValidBlogName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidBlogName parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidBlogName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidBlogName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}