namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Shared.Membership.Events;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly RegistrationData.Parsed RegistrationData = new RegistrationData
        {
            Email = "test@testing.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username",
        }.Parse();

        private static readonly RegisterUserCommand Command = new RegisterUserCommand(
            UserId,
            RegistrationData.ExampleWork,
            RegistrationData.Email,
            RegistrationData.Username,
            RegistrationData.Password);

        private Mock<IEventHandler<UserRegisteredEvent>> userRegistered;
        private Mock<IRegisterUserDbStatement> registerUser;
        private RegisterUserCommandHandler target;

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
            this.registerUser.Setup(v => v.ExecuteAsync(UserId, RegistrationData.Username, RegistrationData.Email, RegistrationData.ExampleWork, RegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.userRegistered.Setup(_ => _.HandleAsync(new UserRegisteredEvent(UserId))).Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.registerUser.Verify();
        }

        [TestMethod]
        public async Task WhenUserCreationSucceeds_ItShouldPublishEvent()
        {
            this.registerUser.Setup(v => v.ExecuteAsync(UserId, RegistrationData.Username, RegistrationData.Email, RegistrationData.ExampleWork, RegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0));

            this.userRegistered.Setup(_ => _.HandleAsync(new UserRegisteredEvent(UserId))).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.userRegistered.Verify();
        }
    }
}