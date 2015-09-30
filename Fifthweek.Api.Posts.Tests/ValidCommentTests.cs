namespace Fifthweek.Api.Posts.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidCommentTests : ValidatedStringTests<ValidComment>
    {
        protected override string ValueA
        {
            get { return "This is a great post"; }
        }

        protected override string ValueB
        {
            get { return "I agree!"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidComment.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidComment.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidComment.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidComment.IsEmpty("a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldAllowBasicValues()
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
        public void ItShouldNotAllowEmptyComments()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowCommentsOver50000Characters()
        {
            this.AssertMaxLength(50000);
        }

        [TestMethod]
        public void ItShouldAllowNewLines()
        {
            this.AssertNewLinesAllowed();
        }

        protected override ValidComment Parse(string value)
        {
            return ValidComment.Parse(value);
        }

        protected override bool TryParse(string value, out ValidComment parsedObject)
        {
            return ValidComment.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidComment parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidComment.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidComment parsedObject)
        {
            return parsedObject.Value;
        }
    }
}