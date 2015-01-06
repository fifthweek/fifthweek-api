using Fifthweek.Api.Identity.Membership;
using Fifthweek.Api.Tests.Shared;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class EmailTestsBase<T> : TryParsableTests<T, string> where T : Email
    {
        [TestMethod]
        public void ItShouldRecogniseEquality()
        {
            this.TestEquality();
        }

        [TestMethod]
        public void ItShouldAllowBasicEmailAddresses()
        {
            this.GoodValue("joe@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNowAllowNull()
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
        public void ItShouldNotAllowEmptyAddresses()
        {
            this.BadValue("");
            this.BadValue(" ");
        }

        [TestMethod]
        public void ItShouldNotAllowAddressesWithoutAtSymbol()
        {
            this.BadValue("joebloggs.com");
        }

        protected override string ValueA
        {
            get { return "joe@example.com"; }
        }

        protected override string ValueB
        {
            get { return "bloggs@example.com"; }
        }

        protected override string GetValue(T parsedObject)
        {
            return parsedObject.Value;
        }
    }
}