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
            var registration1 = NewRegisterUserCommand();
            var registration2 = NewRegisterUserCommand();

            Assert.AreEqual(registration1, registration2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentRegistrationData()
        {
            var registration1 = NewRegisterUserCommand();
            var registration2 = NewRegisterUserCommand();
            registration2.RegistrationData.ExampleWork = "Different";

            Assert.AreNotEqual(registration1, registration2);
        }

        public static RegisterUserCommand NewRegisterUserCommand()
        {
            return new RegisterUserCommand(RegistrationDataTests.NewRegistrationData());
        }
    }
}