using System;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class SubscriptionIdTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var id1 = SubscriptionId.Parse(this.guidA);
            var id2 = SubscriptionId.Parse(this.guidA);

            Assert.AreEqual(id1, id2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentObjects()
        {
            var id1 = SubscriptionId.Parse(this.guidA);
            var id2 = SubscriptionId.Parse(this.guidB);

            Assert.AreNotEqual(id1, id2);
        }

        private readonly Guid guidA = Guid.NewGuid();
        private readonly Guid guidB = Guid.NewGuid();
    }
}