using System;
using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using Fifthweek.Api.Identity.Membership.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class IsPasswordResetTokenValidQueryTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var query1 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidA), TokenA);
            var query2 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidA), TokenA);

            Assert.AreEqual(query1, query2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUserIds()
        {
            var query1 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidA), TokenA);
            var query2 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidB), TokenA);

            Assert.AreNotEqual(query1, query2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentTokens()
        {
            var query1 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidA), TokenA);
            var query2 = new IsPasswordResetTokenValidQuery(UserId.Parse(guidA), TokenB);

            Assert.AreNotEqual(query1, query2);
        }


        private readonly Guid guidA = Guid.NewGuid();
        private readonly Guid guidB = Guid.NewGuid();
        private const string TokenA = "Token A";
        private const string TokenB = "Token B";
    }
}