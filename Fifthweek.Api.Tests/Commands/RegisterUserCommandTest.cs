using Fifthweek.Api.Commands;
using Fifthweek.Api.Tests.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Tests.Commands
{
    [TestClass]
    public class RegisterUserCommandTest
    {
        [TestMethod]
        public void ItShouldRecogniseEqualObjects()
        {
            var command1 = NewRegisterUserCommand();
            var command2 = NewRegisterUserCommand();

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var command1 = NewRegisterUserCommand();
            var command2 = NewRegisterUserCommand();
            command2.RegistrationData.ExampleWork = "Different";

            Assert.AreNotEqual(command1, command2);
        }

        public static RegisterUserCommand NewRegisterUserCommand()
        {
            return new RegisterUserCommand(RegistrationDataTests.NewRegistrationData());
        }
    }
}