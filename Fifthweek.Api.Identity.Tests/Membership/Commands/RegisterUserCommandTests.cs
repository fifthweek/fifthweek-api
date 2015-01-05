using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Tests.Membership.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegisterUserCommandTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var data = RegistrationDataTests.NewData();
            var command1 = NewCommand(data);
            var command2 = NewCommand(data);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var data = RegistrationDataTests.NewData();
            var command1 = NewCommand(data);

            data.ExampleWork = "Different";
            var command2 = NewCommand(data);

            Assert.AreNotEqual(command1, command2);
        }

        public static RegisterUserCommand NewCommand(RegistrationData registrationData)
        {
            return new RegisterUserCommand(
                registrationData.ExampleWork,
                NormalizedEmail.Parse(registrationData.Email),
                NormalizedUsername.Parse(registrationData.Username),
                Password.Parse(registrationData.Password));
        }
    }
}