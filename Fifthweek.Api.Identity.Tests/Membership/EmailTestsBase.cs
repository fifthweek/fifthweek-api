using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class EmailTestsBase
    {
        [TestMethod]
        public void ItShouldAllowBasicEmailAddresses()
        {
            this.GoodEmail("joe@bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowTokens()
        {
            this.GoodEmail("joe+token@bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowSmallTlds()
        {
            this.GoodEmail("joe@bloggs.co");
        }

        [TestMethod]
        public void ItShouldAllowLargeTlds()
        {
            this.GoodEmail("joe@bloggs.museum");
        }

        [TestMethod]
        public void ItShouldAllowIPAddresses()
        {
            this.GoodEmail("joe@[127.0.0.1]");
            this.GoodEmail("joe@127.0.0.1");
        }

        [TestMethod]
        public void ItShouldAllowSubdomains()
        {
            this.GoodEmail("joe@sub.bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            this.GoodEmail("joe123@bloggs456.com");
        }

        [TestMethod]
        public void ItShouldNotAllowQuotedNames()
        {
            this.BadEmail("\"joe bloggs\"@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowInnerWhitespace()
        {
            this.BadEmail("joe @bloggs.com");
            this.BadEmail("joe@ bloggs.com");
            this.BadEmail("jo e@bloggs.com");
            this.BadEmail("joe@blo ggs.com");
            this.BadEmail("joe@bloggs .com");
            this.BadEmail("joe@bloggs. com");
            this.BadEmail("joe@bloggs.com\njoe@bloggs.com");
            this.BadEmail("joe\n@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowEmptyAddresses()
        {
            this.BadEmail("");
            this.BadEmail(" ");
        }

        [TestMethod]
        public void ItShouldNotAllowAddressesWithoutAtSymbol()
        {
            this.BadEmail("joebloggs.com");
        }

        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var email1 = Parse("joe@example.com");
            var email2 = Parse("joe@example.com");

            Assert.AreEqual(email1, email2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var email1 = Parse("joe@example.com");
            var email2 = Parse("bloggs@example.com");

            Assert.AreNotEqual(email1, email2);
        }

        protected abstract Email Parse(string emailValue);

        protected abstract bool TryParse(string emailValue, out Email email);

        protected void GoodEmail(string emailValue)
        {
            Email email;
            var emailValid = this.TryParse(emailValue, out email);

            Assert.IsTrue(emailValid);
            Assert.AreEqual(emailValue, email.Value);
        }

        protected void BadEmail(string emailValue)
        {
            Email email;
            var emailValid = this.TryParse(emailValue, out email);

            Assert.IsFalse(emailValid);
        }
    }
}