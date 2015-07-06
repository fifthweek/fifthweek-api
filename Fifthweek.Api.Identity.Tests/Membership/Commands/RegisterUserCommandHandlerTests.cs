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
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly RegistrationData.Parsed NonCreatorRegistrationData = new RegistrationData
        {
            Email = "test@blahtesting.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username"
        }.Parse();

        private static readonly RegistrationData.Parsed CreatorRegistrationData = new RegistrationData
        {
            Email = "test@blahtesting.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username",
            CreatorName = "creator name"
        }.Parse();

        private static readonly RegistrationData.Parsed NonCreatorTestUserRegistrationData = new RegistrationData
        {
            Email = "test@testing.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username"
        }.Parse();

        private static readonly RegistrationData.Parsed CreatorTestUserRegistrationData = new RegistrationData
        {
            Email = "test@testing.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username",
            CreatorName = "creator name"
        }.Parse();

        private static readonly RegisterUserCommand CreatorCommand = new RegisterUserCommand(
            UserId,
            CreatorRegistrationData.ExampleWork,
            CreatorRegistrationData.Email,
            CreatorRegistrationData.Username,
            CreatorRegistrationData.Password,
            true,
            CreatorRegistrationData.CreatorName);

        private static readonly RegisterUserCommand NonCreatorCommand = new RegisterUserCommand(
            UserId,
            NonCreatorRegistrationData.ExampleWork,
            NonCreatorRegistrationData.Email,
            NonCreatorRegistrationData.Username,
            NonCreatorRegistrationData.Password,
            false,
            null);

        private static readonly RegisterUserCommand CreatorTestUserCommand = new RegisterUserCommand(
            UserId,
            CreatorTestUserRegistrationData.ExampleWork,
            CreatorTestUserRegistrationData.Email,
            CreatorTestUserRegistrationData.Username,
            CreatorTestUserRegistrationData.Password,
            true,
            CreatorTestUserRegistrationData.CreatorName);

        private static readonly RegisterUserCommand NonCreatorTestUserCommand = new RegisterUserCommand(
            UserId,
            NonCreatorTestUserRegistrationData.ExampleWork,
            NonCreatorTestUserRegistrationData.Email,
            NonCreatorTestUserRegistrationData.Username,
            NonCreatorTestUserRegistrationData.Password,
            false,
            null);

        private Mock<IRegisterUserDbStatement> registerUser;
        private Mock<IReservedUsernameService> reservedUsernames;
        private Mock<IUserManager> userManager;
        private RegisterUserCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.reservedUsernames = new Mock<IReservedUsernameService>();

            // Give potentially side-effecting components strict mock behaviour.
            this.registerUser = new Mock<IRegisterUserDbStatement>(MockBehavior.Strict);
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);

            this.target = new RegisterUserCommandHandler(this.registerUser.Object, this.reservedUsernames.Object, this.userManager.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommandIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenUserRegistrationRequested_ItShouldCallTheRegisterUserDbStatement()
        {
            this.reservedUsernames.Setup(v => v.AssertNotReserved(NonCreatorCommand.Username)).Verifiable();

            this.registerUser.Setup(v => v.ExecuteAsync(UserId, NonCreatorRegistrationData.Username, NonCreatorRegistrationData.Email, NonCreatorRegistrationData.ExampleWork, null, NonCreatorRegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(NonCreatorCommand);

            this.reservedUsernames.Verify();
            this.registerUser.Verify();
        }

        [TestMethod]
        public async Task WhenCreatorRegistrationRequested_ItShouldCallTheRegisterUserDbStatement()
        {
            this.reservedUsernames.Setup(v => v.AssertNotReserved(CreatorCommand.Username)).Verifiable();

            this.registerUser.Setup(v => v.ExecuteAsync(UserId, CreatorRegistrationData.Username, CreatorRegistrationData.Email, CreatorRegistrationData.ExampleWork, CreatorRegistrationData.CreatorName, CreatorRegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.userManager.Setup(v => v.AddToRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(new IdentityResult()).Verifiable();

            await this.target.HandleAsync(CreatorCommand);

            this.reservedUsernames.Verify();
            this.registerUser.Verify();
            this.userManager.Verify();
        }

        [TestMethod]
        public async Task WhenTestUserRegistrationRequested_ItShouldCallTheRegisterUserDbStatement_AndAddToTestRole()
        {
            this.reservedUsernames.Setup(v => v.AssertNotReserved(NonCreatorCommand.Username)).Verifiable();

            this.registerUser.Setup(v => v.ExecuteAsync(UserId, NonCreatorTestUserRegistrationData.Username, NonCreatorTestUserRegistrationData.Email, NonCreatorTestUserRegistrationData.ExampleWork, null, NonCreatorTestUserRegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.userManager.Setup(v => v.AddToRoleAsync(UserId.Value, FifthweekRole.Test)).ReturnsAsync(new IdentityResult()).Verifiable();

            await this.target.HandleAsync(NonCreatorTestUserCommand);

            this.reservedUsernames.Verify();
            this.registerUser.Verify();
            this.userManager.Verify();
        }

        [TestMethod]
        public async Task WhenTestCreatorRegistrationRequested_ItShouldCallTheRegisterUserDbStatement_AndAddToTestRole()
        {
            this.reservedUsernames.Setup(v => v.AssertNotReserved(CreatorCommand.Username)).Verifiable();

            this.registerUser.Setup(v => v.ExecuteAsync(UserId, CreatorTestUserRegistrationData.Username, CreatorTestUserRegistrationData.Email, CreatorTestUserRegistrationData.ExampleWork, CreatorTestUserRegistrationData.CreatorName, CreatorTestUserRegistrationData.Password, It.IsAny<DateTime>()))
                .Returns(Task.FromResult(0)).Verifiable();

            this.userManager.Setup(v => v.AddToRoleAsync(UserId.Value, FifthweekRole.Creator)).ReturnsAsync(new IdentityResult()).Verifiable();
            this.userManager.Setup(v => v.AddToRoleAsync(UserId.Value, FifthweekRole.Test)).ReturnsAsync(new IdentityResult()).Verifiable();

            await this.target.HandleAsync(CreatorTestUserCommand);

            this.reservedUsernames.Verify();
            this.registerUser.Verify();
            this.userManager.Verify();
        }

        [TestMethod]
        public void RegisterAsTestUserShouldReturnTrueForTestUsers()
        {
            Assert.IsFalse(this.target.RegisterAsTestUser(new Email("phil@cat.com")));
            Assert.IsFalse(this.target.RegisterAsTestUser(new Email("phil@fifthweek.com")));
            Assert.IsFalse(this.target.RegisterAsTestUser(new Email("phil@test.fifthweek.com")));
            Assert.IsTrue(this.target.RegisterAsTestUser(new Email("phil@testing.fifthweek.com")));
        }
    }
}