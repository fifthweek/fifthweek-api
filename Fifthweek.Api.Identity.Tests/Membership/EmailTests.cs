using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class EmailTests
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
        public void ItShouldRecogniseEqualObjects()
        {
            var email1 = Email.Parse("joe@example.com");
            var email2 = Email.Parse("joe@example.com");

            Assert.AreEqual(email1, email2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var email1 = Email.Parse("joe@example.com");
            var email2 = Email.Parse("bloggs@example.com");

            Assert.AreNotEqual(email1, email2);
        }

        protected virtual Email Parse(string emailValue)
        {
            return Email.Parse(emailValue);
        }

        protected virtual bool TryParse(string emailValue, out Email email)
        {
            return Email.TryParse(emailValue, out email);
        }

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