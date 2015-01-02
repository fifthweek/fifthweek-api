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
            GoodEmail("joe@bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowTokens()
        {
            GoodEmail("joe+token@bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowSmallTlds()
        {
            GoodEmail("joe@bloggs.co");
        }

        [TestMethod]
        public void ItShouldAllowLargeTlds()
        {
            GoodEmail("joe@bloggs.museum");
        }

        [TestMethod]
        public void ItShouldAllowIPAddresses()
        {
            GoodEmail("joe@[127.0.0.1]");
            GoodEmail("joe@127.0.0.1");
        }

        [TestMethod]
        public void ItShouldAllowSubdomains()
        {
            GoodEmail("joe@sub.bloggs.com");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            GoodEmail("joe123@bloggs456.com");
        }

        [TestMethod]
        public void ItShouldNotAllowQuotedNames()
        {
            BadEmail("\"joe bloggs\"@bloggs.com");
        }

        [TestMethod]
        public void ItShouldNotAllowWhitespace()
        {
            BadEmail(" joe@bloggs.com");
            BadEmail("joe@bloggs.com ");
            BadEmail("joe @bloggs.com");
            BadEmail("joe@ bloggs.com");
            BadEmail("jo e@bloggs.com");
            BadEmail("joe@blo ggs.com");
            BadEmail("joe@bloggs .com");
            BadEmail("joe@bloggs. com");
        }

        [TestMethod]
        public void ItShouldNotAllowUppercase()
        {
            BadEmail("Joe@Bloggs.com");
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

        private static void GoodEmail(string emailValue)
        {
            Email email;
            var emailValid = Email.TryParse(emailValue, out email);

            Assert.IsTrue(emailValid);
            Assert.AreEqual(emailValue, email.Value);
        }

        private static void BadEmail(string emailValue)
        {
            Email email;
            var emailValid = Email.TryParse(emailValue, out email);

            Assert.IsFalse(emailValid);
        }
    }
}