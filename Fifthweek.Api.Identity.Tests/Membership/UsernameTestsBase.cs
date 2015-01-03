using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public abstract class UsernameTestsBase
    {
        [TestMethod]
        public void ItShouldAllowBasicUsernames()
        {
            this.GoodUsername("joebloggs");
        }

        [TestMethod]
        public void ItShouldAllowUnderscores()
        {
            this.GoodUsername("joe_bloggs");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            this.GoodUsername("joe123");
            this.GoodUsername("123456");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesUnder6Characters()
        {
            this.GoodUsername("joeblo");
            this.BadUsername("joebl");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesOver20Characters()
        {
            this.GoodUsername("joe_bloggs_123456789");
            this.BadUsername("joe_bloggs_1234567890");
        }

        [TestMethod]
        public void ItShouldNotAllowIllegalCharacters()
        {
            // Include 'a' to ensure we're not just checking for the existence of at least one valid character.
            this.BadUsername("a!@£#$%^&*()-+={}[]");
            this.BadUsername("a:;'`\"\\|<>,./?~§±");
        }

        [TestMethod]
        public void ItShouldNotAllowInnerWhitespace()
        {
            this.BadUsername("joe bloggs");
        }

        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var username1 = Parse("joe_bloggs");
            var username2 = Parse("joe_bloggs");

            Assert.AreEqual(username1, username2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var username1 = Parse("joe_bloggs");
            var username2 = Parse("joe_bloggs2");

            Assert.AreNotEqual(username1, username2);
        }

        protected abstract Username Parse(string usernameValue);

        protected abstract bool TryParse(string usernameValue, out Username username);

        protected void GoodUsername(string usernameValue)
        {
            Username username;
            var usernameValid = this.TryParse(usernameValue, out username);

            Assert.IsTrue(usernameValid);
            Assert.AreEqual(usernameValue, username.Value);
        }

        protected void BadUsername(string usernameValue)
        {
            Username username;
            var usernameValid = this.TryParse(usernameValue, out username);

            Assert.IsFalse(usernameValid);
        }
    }
}