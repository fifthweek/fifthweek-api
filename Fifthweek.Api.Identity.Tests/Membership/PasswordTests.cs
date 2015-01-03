using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordTests
    {
        [TestMethod]
        public void ItShouldAllowBasicPasswords()
        {
            this.GoodPassword("Secr3T!");
        }

        [TestMethod]
        public void ItShouldNotAllowPasswordsUnder6Characters()
        {
            this.GoodPassword("passwo");
            this.BadPassword("passw");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesOver100Characters()
        {
            this.GoodPassword(new string(' ', 100));
            this.BadPassword(new string(' ', 101));
        }

        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var password1 = Password.Parse("password");
            var password2 = Password.Parse("password");

            Assert.AreEqual(password1, password2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var password1 = Password.Parse("password");
            var password2 = Password.Parse("password2");

            Assert.AreNotEqual(password1, password2);
        }

        protected void GoodPassword(string passwordValue)
        {
            Password password;
            var passwordValid = Password.TryParse(passwordValue, out password);

            Assert.IsTrue(passwordValid);
            Assert.AreEqual(passwordValue, password.Value);
        }

        protected void BadPassword(string passwordValue)
        {
            Password password;
            var passwordValid = Password.TryParse(passwordValue, out password);

            Assert.IsFalse(passwordValid);
        }
    }
}