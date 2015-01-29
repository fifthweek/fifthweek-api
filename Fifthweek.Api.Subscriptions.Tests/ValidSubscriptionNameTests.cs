namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Subscriptions.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ValidSubscriptionNameTests : ValidatedStringTests<ValidSubscriptionName>
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

        protected override ValidSubscriptionName Parse(string value)
        {
            return ValidSubscriptionName.Parse(value);
        }

        protected override bool TryParse(string value, out ValidSubscriptionName parsedObject)
        {
            return ValidSubscriptionName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out ValidSubscriptionName parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return ValidSubscriptionName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(ValidSubscriptionName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}