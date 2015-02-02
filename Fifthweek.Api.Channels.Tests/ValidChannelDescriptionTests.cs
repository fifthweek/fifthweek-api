namespace Fifthweek.Api.Channels.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidChannelDescriptionTests : ValidatedStringTests<ValidChannelDescription>
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
            var result = ValidChannelDescription.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidChannelDescription.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidChannelDescription.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidChannelDescription.IsEmpty("a");
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
        public void ItShouldNotAllowDescriptionsOver250Characters()
        {
            this.AssertMaxLength(250);
        }

        protected override ValidChannelDescription Parse(string value)
        {
            return ValidChannelDescription.Parse(value);
        }

        protected override bool TryParse(string value, out ValidChannelDescription parsedObject)
        {
            return ValidChannelDescription.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidChannelDescription parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidChannelDescription.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidChannelDescription parsedObject)
        {
            return parsedObject.Value;
        }
    }
}