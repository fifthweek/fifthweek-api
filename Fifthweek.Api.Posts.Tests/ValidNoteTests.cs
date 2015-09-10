namespace Fifthweek.Api.Posts.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidNoteTests : ValidatedStringTests<ValidNote>
    {
        protected override string ValueA
        {
            get { return "Hey peeps :D"; }
        }

        protected override string ValueB
        {
            get { return "On holiday next month! Been balling too hard ;)"; }
        }

        [TestMethod]
        public void ItShouldTreatNullAsEmpty()
        {
            var result = ValidNote.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidNote.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidNote.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidNote.IsEmpty("a");
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
        public void ItShouldNotAllowEmptyNotes()
        {
            this.AssertMinLength(1);
        }

        [TestMethod]
        public void ItShouldNotAllowNotesOver280Characters()
        {
            this.AssertMaxLength(280);
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.AssertTabsNotAllowed();
        }

        [TestMethod]
        public void ItShouldAllowNewLines()
        {
            this.AssertNewLinesAllowed();
        }

        protected override ValidNote Parse(string value)
        {
            return ValidNote.Parse(value);
        }

        protected override bool TryParse(string value, out ValidNote parsedObject)
        {
            return ValidNote.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidNote parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidNote.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidNote parsedObject)
        {
            return parsedObject.Value;
        }
    }
}