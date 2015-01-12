namespace Fifthweek.Api.Subscriptions.Tests
{
    using System.Collections.Generic;

    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubscriptionNameTests : ValidatedStringTests<SubscriptionName>
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

        protected override SubscriptionName Parse(string value, bool exact)
        {
            return SubscriptionName.Parse(value);
        }

        protected override bool TryParse(string value, out SubscriptionName parsedObject, bool exact)
        {
            return SubscriptionName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out SubscriptionName parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return SubscriptionName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(SubscriptionName parsedObject)
        {
            return parsedObject.Value;
        }
    }
}