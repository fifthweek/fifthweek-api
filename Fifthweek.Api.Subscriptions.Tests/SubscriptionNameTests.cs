using System.Collections.Generic;
using Fifthweek.Api.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Subscriptions.Tests
{
    [TestClass]
    public class SubscriptionNameTests : ValidatedPrimitiveEqualityTests<SubscriptionName, string>
    {
        [TestMethod]
        public void ItShouldAllowBasicSubscriptionNames()
        {
            this.GoodValue("Web Comics and More");
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldAllowPunctuation()
        {
            this.GoodValue("A webcomic, of romance!?.");
        }

        [TestMethod]
        public void ItShouldNotAllowEmptySubscriptionNames()
        {
            this.GoodValue("1");
            this.BadValue("");
        }

        [TestMethod]
        public void ItShouldNotAllowSubscriptionNamesOver25Characters()
        {
            this.GoodValue(new string('x', 25));
            this.BadValue(new string('x', 26));

            // Test whitespace sensitivity.
            this.GoodValue(new string(' ', 25));
            this.BadValue(new string(' ', 26)); 
        }

        [TestMethod]
        public void ItShouldNotAllowTabs()
        {
            this.BadValue("abcdef\t");
            this.BadValue("\tabcdef");
            this.BadValue("abc\tdef");
        }

        [TestMethod]
        public void ItShouldNotAllowNewLines()
        {
            this.BadValue("abcdef\n");
            this.BadValue("\nabcdef");
            this.BadValue("abc\ndef");

            this.BadValue("abcdef\r");
            this.BadValue("\rabcdef");
            this.BadValue("abc\rdef");
        }

        protected override string ValueA
        {
            get { return "Subscription name A"; }
        }

        protected override string ValueB
        {
            get { return "Subscription name B"; }
        }

        protected override SubscriptionName Parse(string value)
        {
            return SubscriptionName.Parse(value);
        }

        protected override bool TryParse(string value, out SubscriptionName parsedObject)
        {
            return SubscriptionName.TryParse(value, out parsedObject);
        }

        protected override bool TryParse(string value, out SubscriptionName parsedObject, out IReadOnlyCollection<string> errorMessages)
        {
            return SubscriptionName.TryParse(value, out parsedObject, out errorMessages);
        }

        protected override string GetValue(SubscriptionName parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly string InvalidValue = "";
    }
}