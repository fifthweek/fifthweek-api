using System;
using System.Collections.Generic;
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
            this.GoodValue("joebloggs");
        }

        [TestMethod]
        public void ItShouldAllowUnderscores()
        {
            this.GoodValue("joe_bloggs");
        }

        [TestMethod]
        public void ItShouldAllowNumbers()
        {
            this.GoodValue("joe123");
            this.GoodValue("123456");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesUnder6Characters()
        {
            this.GoodValue("joeblo");
            this.BadValue("joebl");
        }

        [TestMethod]
        public void ItShouldNotAllowUsernamesOver20Characters()
        {
            this.GoodValue("joe_bloggs_123456789");
            this.BadValue("joe_bloggs_1234567890");
        }

        [TestMethod]
        public void ItShouldNotAllowIllegalCharacters()
        {
            // Include 'a' to ensure we're not just checking for the existence of at least one valid character.
            this.BadValue("a!@£#$%^&*()-+={}[]");
            this.BadValue("a:;'`\"\\|<>,./?~§±");
        }

        [TestMethod]
        public void ItShouldNotAllowInnerWhitespace()
        {
            this.BadValue("joe bloggs");
            this.BadValue("joe\nbloggs");
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

        protected abstract bool TryParse(string usernameValue, out Username username, out IReadOnlyCollection<string> errorMessages);

        protected void GoodValue(string value)
        {
            Username parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, parsedObject.Value);

            valid = this.TryParse(value, out parsedObject, out errorMessages);
            Assert.IsTrue(valid);
            Assert.AreEqual(value, parsedObject.Value);
            Assert.IsTrue(errorMessages.Count == 0);

            parsedObject = this.Parse(value);
            Assert.AreEqual(value, parsedObject.Value);
        }

        protected void BadValue(string value)
        {
            Username parsedObject;
            IReadOnlyCollection<string> errorMessages;

            var valid = this.TryParse(value, out parsedObject);
            Assert.IsFalse(valid);

            valid = this.TryParse(value, out parsedObject, out errorMessages);
            Assert.IsFalse(valid);
            Assert.IsTrue(errorMessages.Count > 0);

            try
            {
                this.Parse(value);
                Assert.Fail("Expected argument exception");
            }
            catch (ArgumentException)
            {
            }
        }
    }
}