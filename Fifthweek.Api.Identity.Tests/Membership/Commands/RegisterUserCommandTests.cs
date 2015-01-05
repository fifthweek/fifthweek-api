using Fifthweek.Api.Identity.Membership;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;

    using Fifthweek.Api.Core;
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
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, data);
            var command2 = NewCommand(userId, data);

            Assert.AreEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentExampleWork()
        {
            var registrationData = RegistrationDataTests.NewData();
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, registrationData);

            registrationData.ExampleWork = "different";
            registrationData.Parse();
            var command2 = NewCommand(userId, registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentEmail()
        {
            var registrationData = RegistrationDataTests.NewData();
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, registrationData);

            registrationData.Email = "different@test.com";
            registrationData.Parse();
            var command2 = NewCommand(userId, registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentPassword()
        {
            var registrationData = RegistrationDataTests.NewData();
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, registrationData);

            registrationData.Password = "different";
            registrationData.Parse();
            var command2 = NewCommand(userId, registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUsername()
        {
            var registrationData = RegistrationDataTests.NewData();
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, registrationData);

            registrationData.Username = "different";
            registrationData.Parse();
            var command2 = NewCommand(userId, registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        [TestMethod]
        public void ItShouldRecogniseDifferentUserId()
        {
            var registrationData = RegistrationDataTests.NewData();
            var userId = Guid.NewGuid();
            var command1 = NewCommand(userId, registrationData);

            userId = Guid.NewGuid();
            var command2 = NewCommand(userId, registrationData);

            Assert.AreNotEqual(command1, command2);
        }

        public static RegisterUserCommand NewCommand(Guid userId, RegistrationData registrationData)
        {
            return new RegisterUserCommand(
                userId,
                registrationData.ExampleWork,
                NormalizedEmail.Parse(registrationData.Email),
                NormalizedUsername.Parse(registrationData.Username),
                Password.Parse(registrationData.Password));
        }
    }
}