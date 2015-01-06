namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordResetRequestDataTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var request1 = NewData();
            var request2 = NewData();

            Assert.AreEqual(request1, request2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            var request1 = NewData();
            var request2 = NewData();
            request2.Email = "Different";

            Assert.AreNotEqual(request1, request2);
            Assert.AreNotEqual(request1, null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            var request1 = NewData();
            var request2 = NewData();
            request2.Username = "Different";

            Assert.AreNotEqual(request1, request2);
        }

        public static PasswordResetRequestData NewData()
        {
            return new PasswordResetRequestData
            {
                Email = "test@test.com",
                Username = "test_username",
            };
        }
    }
}