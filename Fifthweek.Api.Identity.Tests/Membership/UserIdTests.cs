using System;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class UserIdTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var userId1 = UserId.Parse(this.guidA);
            var userId2 = UserId.Parse(this.guidA);

            Assert.AreEqual(userId1, userId2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var userId1 = UserId.Parse(this.guidA);
            var userId2 = UserId.Parse(this.guidB);

            Assert.AreNotEqual(userId1, userId2);
        }

        private readonly Guid guidA = Guid.NewGuid();
        private readonly Guid guidB = Guid.NewGuid();
    }
}