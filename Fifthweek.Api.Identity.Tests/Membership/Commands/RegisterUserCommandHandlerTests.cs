namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Shared.Membership.Events;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly RegistrationData RegistrationData = new RegistrationData
        {
            Email = "test@testing.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username",
        };

        private static readonly RegisterUserCommand Command = RegisterUserCommandTests.NewCommand(UserId.Value, RegistrationData);

        private Mock<IEventHandler<UserRegisteredEvent>> userRegistered;
        private Mock<IRegisterUserDbStatement> registerUser;
        private RegisterUserCommandHandler target;

        static RegisterUserCommandHandlerTests()
        {
            RegistrationData.Parse();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.userRegistered = new Mock<IEventHandler<UserRegisteredEvent>>();

            // Give potentially side-effecting components strict mock behaviour.
            this.registerUser = new Mock<IRegisterUserDbStatement>(MockBehavior.Strict);

            this.target = new RegisterUserCommandHandler(this.userRegistered.Object, this.registerUser.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenUserCreationRequested_ItShouldCallTheRegisterUserDbStatement()
        {
            this.registerUser.Setup(v => v.ExecuteAsync(UserId, RegistrationData.UsernameObject, RegistrationData.EmailObject, RegistrationData.ExampleWork, RegistrationData.PasswordObject, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.userRegistered.Setup(_ => _.HandleAsync(new UserRegisteredEvent(UserId))).Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.registerUser.Verify();
        }

        [TestMethod]
        public async Task WhenUserCreationSucceeds_ItShouldPublishEvent()
        {
            this.registerUser.Setup(v => v.ExecuteAsync(UserId, RegistrationData.UsernameObject, RegistrationData.EmailObject, RegistrationData.ExampleWork, RegistrationData.PasswordObject, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0));

            this.userRegistered.Setup(_ => _.HandleAsync(new UserRegisteredEvent(UserId))).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.userRegistered.Verify();
        }
    }

    public static class RegisterUserCommandTests
    {
        public static RegisterUserCommand NewCommand(Guid userId, RegistrationData registrationData)
        {
            return new RegisterUserCommand(
                new UserId(userId),
                registrationData.ExampleWork,
                ValidEmail.Parse(registrationData.Email),
                ValidUsername.Parse(registrationData.Username),
                ValidPassword.Parse(registrationData.Password));
        }
    }
}