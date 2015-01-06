using System;

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
        public void ItShouldRecogniseNullAsDifferent()
        {
            var confirmation1 = NewData();

            Assert.AreNotEqual(confirmation1, null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUserId()
        {
            var confirmation1 = NewData();
            var confirmation2 = NewData();
            confirmation2.UserId = Guid.NewGuid();

            Assert.AreNotEqual(confirmation1, confirmation2);
            Assert.AreNotEqual(confirmation1, null);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentToken()
        {
            var confirmation1 = NewData();
            var confirmation2 = NewData();
            confirmation2.Token = "Different";

            Assert.AreNotEqual(confirmation1, confirmation2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
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
                UserId = Guid.Parse("{5E41A09B-0523-4FD3-BC82-9D5A02D2FB97}"),
                Token = "SomeLongBase64EncodedGumpf",
                NewPassword = "SecretSquirrel",
            };
        }
    }
}