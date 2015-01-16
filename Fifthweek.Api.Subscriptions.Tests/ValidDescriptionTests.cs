namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidDescriptionTests : ValidatedStringTests<ValidDescription>
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

        protected override ValidDescription Parse(string value, bool exact)
        {
            return ValidDescription.Parse(value);
        }

        protected override bool TryParse(string value, out ValidDescription parsedObject, bool exact)
        {
            return ValidDescription.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidDescription parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return ValidDescription.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidDescription parsedObject)
        {
            return parsedObject.Value;
        }
    }
}