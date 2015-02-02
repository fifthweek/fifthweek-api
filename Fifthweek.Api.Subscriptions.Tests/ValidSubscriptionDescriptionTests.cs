namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidSubscriptionDescriptionTests : ValidatedStringTests<ValidSubscriptionDescription>
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
            var result = ValidSubscriptionDescription.IsEmpty(null);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatEmptyStringAsEmpty()
        {
            var result = ValidSubscriptionDescription.IsEmpty(string.Empty);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithOnlyWhiteSpaceCharactersAsNonEmpty()
        {
            var result = ValidSubscriptionDescription.IsEmpty(" ");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ItShouldTreatStringWithAtLeast1NonWhiteSpaceCharacterAsNonEmpty()
        {
            var result = ValidSubscriptionDescription.IsEmpty("a");
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

        protected override ValidSubscriptionDescription Parse(string value)
        {
            return ValidSubscriptionDescription.Parse(value);
        }

        protected override bool TryParse(string value, out ValidSubscriptionDescription parsedObject)
        {
            return ValidSubscriptionDescription.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidSubscriptionDescription parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidSubscriptionDescription.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidSubscriptionDescription parsedObject)
        {
            return parsedObject.Value;
        }
    }
}