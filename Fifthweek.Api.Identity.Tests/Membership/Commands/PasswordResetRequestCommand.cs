namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class PasswordResetRequestCommandTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var registrationData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command1 = NewPasswordResetRequestCommand(registrationData);
            var command2 = NewPasswordResetRequestCommand(registrationData);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var registrationData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command1 = NewPasswordResetRequestCommand(registrationData);

            registrationData.Username = "Different";
            var command2 = NewPasswordResetRequestCommand(registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        public static PasswordResetRequestCommand NewPasswordResetRequestCommand(PasswordResetRequestData registrationData)
        {
            return new PasswordResetRequestCommand(
                registrationData.Email,
                registrationData.Username);
        }
    }
}