namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RequestPasswordResetCommandTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var registrationData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command1 = NewRequestPasswordResetCommand(registrationData);
            var command2 = NewRequestPasswordResetCommand(registrationData);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var registrationData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            var command1 = NewRequestPasswordResetCommand(registrationData);

            registrationData.Username = "Different";
            var command2 = NewRequestPasswordResetCommand(registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        public static RequestPasswordResetCommand NewRequestPasswordResetCommand(PasswordResetRequestData registrationData)
        {
            return new RequestPasswordResetCommand(
                registrationData.Email,
                registrationData.Username);
        }
    }
}