namespace Fifthweek.Api.Identity.Tests.Commands
{
    using Fifthweek.Api.Identity.Commands;
    using Fifthweek.Api.Identity.Controllers;
    using Fifthweek.Api.Identity.Tests.Controllers;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class RegisterUserCommandTests
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var registrationData = RegistrationDataTests.NewRegistrationData();
            var command1 = NewRegisterUserCommand(registrationData);
            var command2 = NewRegisterUserCommand(registrationData);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var registrationData = RegistrationDataTests.NewRegistrationData();
            var command1 = NewRegisterUserCommand(registrationData);

            registrationData.ExampleWork = "Different";
            var command2 = NewRegisterUserCommand(registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        public static RegisterUserCommand NewRegisterUserCommand(RegistrationData registrationData)
        {
            return new RegisterUserCommand(
                registrationData.ExampleWork,
                registrationData.Email,
                registrationData.Username,
                registrationData.Password);
        }
    }
}