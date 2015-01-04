namespace Fifthweek.Api.Identity.Tests.Membership.Controllers
{
    using Fifthweek.Api.Identity.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordResetConfirmationDataTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var confirmation1 = NewData();
            var confirmation2 = NewData();

            Assert.AreEqual(confirmation1, confirmation2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            var confirmation1 = NewData();
            var confirmation2 = NewData();
            confirmation2.Token = "Different";

            Assert.AreNotEqual(confirmation1, confirmation2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            var confirmation1 = NewData();
            var confirmation2 = NewData();
            confirmation2.NewPassword = "Different";

            Assert.AreNotEqual(confirmation1, confirmation2);
        }

        public static PasswordResetConfirmationData NewData()
        {
            return new PasswordResetConfirmationData
            {
                Token = "SomeLongBase64EncodedGumpf",
                NewPassword = "SecretSquirrel",
            };
        }
    }
}