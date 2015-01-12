namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System.Collections.Generic;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmailTests : ValidatedStringTests<Email>
    {
        protected override string ValueA
        {
            get { return "joe@example.com"; }
        }

        protected override string ValueB
        {
            get { return "bloggs@example.com"; }
        }

        [TestMethod]
        public void ItShouldAllowBasicEmailAddresses()
        {
            this.GoodValue("joe@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowNull()
        {
            this.BadValue(null);
        }

        [TestMethod]
        public void ItShouldAllowTokens()
        {
            this.GoodValue("joe+token@bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowSmallTlds()
        {
            this.GoodValue("joe@bloggs.co");
        }

        [TestMethod]
        public void ItShouldAllowLargeTlds()
        {
            this.GoodValue("joe@bloggs.museum");
        }

        [TestMethod]
        public void ItShouldAllowIPAddresses()
        {
            this.GoodValue("joe@[127.0.0.1]");
            this.GoodValue("joe@127.0.0.1");
        }

        [TestMethod]
        public void ItShouldAllowSubdomains()
        {
            this.GoodValue("joe@sub.bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            this.GoodValue("joe123@bloggs456.com");
        }

        [TestMethod]
        public void ItShouldNotAllowQuotedNames()
        {
            this.BadValue("\"joe bloggs\"@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowInnerWhitespace()
        {
            this.BadValue("joe @bloggs.com");
            this.BadValue("joe@ bloggs.com");
            this.BadValue("jo e@bloggs.com");
            this.BadValue("joe@blo ggs.com");
            this.BadValue("joe@bloggs .com");
            this.BadValue("joe@bloggs. com");
            this.BadValue("joe@bloggs.com\njoe@bloggs.com");
            this.BadValue("joe\n@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowEmailsUnder3Characters()
        {
            this.GoodValue("a@a");
            this.BadValue("a@");

            // Whitespace should be ignored.
            this.BadValue("a@ ");
            this.BadValue(" a@");
            this.BadValue(" a@ ");
        }

        [TestMethod]
        public void ItShouldNotAllowEmailsOver256Characters()
        {
            this.AssertMaxLength(256, whitespaceSensitive: false);
        }

        [TestMethod]
        public void ItShouldNotAllowAddressesWithoutAtSymbol()
        {
            this.BadValue("joebloggs.com");
        }

        [TestMethod]
        public void ItShouldNormalizeToHaveNoLeadingOrTrailingWhitespace()
        {
            this.GoodNonExactValue(" joe@bloggs.com", "joe@bloggs.com");
            this.GoodNonExactValue("joe@bloggs.com ", "joe@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNormalizeToHaveAllLowercase()
        {
            this.GoodNonExactValue("Joe@Bloggs.com", "joe@bloggs.com");
        }

        protected override Email Parse(string value, bool exact)
        {
            return Email.Parse(value, exact);
        }

        protected override bool TryParse(string value, out Email parsedObject, bool exact)
        {
            return Email.TryParse(value, out parsedObject, exact);
        }

        protected override bool TryParse(string value, out Email parsedObject, out IReadOnlyCollection<string> errorMessages, bool exact)
        {
            return Email.TryParse(value, out parsedObject, out errorMessages, exact);
        }

        protected override string GetValue(Email parsedObject)
        {
            return parsedObject.Value;
        }

        public static readonly string InvalidValue = "!";
    }
}