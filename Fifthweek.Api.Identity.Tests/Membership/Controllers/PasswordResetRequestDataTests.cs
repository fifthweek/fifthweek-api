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
            var request1 = NewPasswordResetRequestData();
            var request2 = NewPasswordResetRequestData();

            Assert.AreEqual(request1, request2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            var request1 = NewPasswordResetRequestData();
            var request2 = NewPasswordResetRequestData();
            request2.Email = "Different";

            Assert.AreNotEqual(request1, request2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            var request1 = NewPasswordResetRequestData();
            var request2 = NewPasswordResetRequestData();
            request2.Username = "Different";

            Assert.AreNotEqual(request1, request2);
        }

        public static PasswordResetRequestData NewPasswordResetRequestData()
        {
            return new PasswordResetRequestData
            {
                Email = "test@test.com",
                Username = "test_username",
            };
        }
    }
}