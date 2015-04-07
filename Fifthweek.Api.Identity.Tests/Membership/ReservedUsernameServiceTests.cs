namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class ReservedUsernameServiceTests
    {
        private ReservedUsernameService target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new ReservedUsernameService();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void IsReservedShouldRequireAUsername()
        {
            this.target.IsReserved(null);
        }

        [TestMethod]
        public void IsReservedShouldReturnTrueForReservedUsernames()
        {
            Assert.IsTrue(this.target.IsReserved(ValidUsername.Parse("static")));
            Assert.IsTrue(this.target.IsReserved(ValidUsername.Parse("bower_components")));
        }

        [TestMethod]
        public void IsReservedShouldReturnFalseForNonReservedUsernames()
        {
            Assert.IsFalse(this.target.IsReserved(ValidUsername.Parse("phil")));
            Assert.IsFalse(this.target.IsReserved(ValidUsername.Parse("fifthweek")));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AssertAvailableShouldRequireAUsername()
        {
            this.target.AssertNotReserved(null);
        }

        [TestMethod]
        [ExpectedException(typeof(RecoverableException))]
        public void AssertAvailableShouldThrowAnExceptionForReservedUsernames()
        {
            this.target.AssertNotReserved(ValidUsername.Parse("static"));
        }

        [TestMethod]
        public void AssertAvailableShouldReturnForNonReservedUsernames()
        {
            this.target.AssertNotReserved(ValidUsername.Parse("phil"));
        }
    }
}