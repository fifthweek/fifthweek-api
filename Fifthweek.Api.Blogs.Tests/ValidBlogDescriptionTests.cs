namespace Fifthweek.Api.Blogs.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidBlogDescriptionTests : ValidatedStringTests<ValidBlogDescription>
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
            var result = ValidBlogDescription.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidBlogDescription.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidBlogDescription.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidBlogDescription.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicDescriptions()
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
        public void ItShouldAllowTabs()
        {
            this.AssertCharacter('\t', isGood: true);
        }

        [TestMethod]
        public void ItShouldAllowNewLines()
        {
            this.AssertCharacter('\n', isGood: true);
            this.AssertCharacter('\r', isGood: true);
        }

        [TestMethod]
        public void ItShouldNotAllowEmptyDescriptions()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowDescriptionsOver2000Characters()
        {
            this.AssertMaxLength(2000);
        }

        protected override ValidBlogDescription Parse(string value)
        {
            return ValidBlogDescription.Parse(value);
        }

        protected override bool TryParse(string value, out ValidBlogDescription parsedObject)
        {
            return ValidBlogDescription.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidBlogDescription parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidBlogDescription.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidBlogDescription parsedObject)
        {
            return parsedObject.Value;
        }
    }
}