using Fifthweek.Api.Identity.Membership;

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
            var data = PasswordResetRequestDataTests.NewData();
            var command1 = NewCommand(data);
            var command2 = NewCommand(data);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var data = PasswordResetRequestDataTests.NewData();
            var command1 = NewCommand(data);

            data.Username = "different";
            var command2 = NewCommand(data);

            Assert.AreNotEqual(command1, command2);
        }

        public static RequestPasswordResetCommand NewCommand(PasswordResetRequestData passwordResetRequest)
        {
            return new RequestPasswordResetCommand(
                passwordResetRequest.Email == null ? null : NormalizedEmail.Parse(passwordResetRequest.Email),
                passwordResetRequest.Username == null ? null : NormalizedUsername.Parse(passwordResetRequest.Username));
        }
    }
}