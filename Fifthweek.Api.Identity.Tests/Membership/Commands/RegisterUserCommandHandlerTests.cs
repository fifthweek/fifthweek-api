using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Membership.Events;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
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
        private Mock<IUserManager> userManager;
        private RegisterUserCommandHandler target; 

        [TestInitialize]
        public void TestInitialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.userRegistered = new Mock<IEventHandler<UserRegisteredEvent>>();
            this.target = new RegisterUserCommandHandler(this.userManager.Object, this.userRegistered.Object);
        }

        [TestMethod]
        public async Task WhenUserManagerFindsEmail_ItShouldThrowAnException()
        {
            this.userManager.Setup(v => v.FindByEmailAsync(RegistrationData.Email)).ReturnsAsync(new FifthweekUser());
            this.userManager.Setup(v => v.FindByNameAsync(RegistrationData.Username)).ReturnsAsync(null);

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(Command);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains("email"));
            Assert.IsTrue(exception.Message.Contains("taken"));
        }

        [TestMethod]
        public async Task WhenUserManagerFindsUsername_ItShouldThrowAnException()
        {
            this.userManager.Setup(v => v.FindByEmailAsync(RegistrationData.Email)).ReturnsAsync(null);
            this.userManager.Setup(v => v.FindByNameAsync(RegistrationData.Username)).ReturnsAsync(new FifthweekUser());

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(Command);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains("username"));
            Assert.IsTrue(exception.Message.Contains("taken"));
        }

        [TestMethod]
        public async Task WhenCredentialsAccepted_ItShouldCreateUser()
        {
            FifthweekUser fifthweekUser = null;
            this.userManager.Setup(v => v.FindByEmailAsync(RegistrationData.Email)).ReturnsAsync(null);
            this.userManager.Setup(v => v.FindByNameAsync(RegistrationData.Username)).ReturnsAsync(null);
            this.userManager.Setup(v => v.CreateAsync(It.IsAny<FifthweekUser>(), RegistrationData.Password))
                .Callback<FifthweekUser, string>((u, p) => fifthweekUser = u)
                .ReturnsAsync(new MockIdentityResult());

            await this.target.HandleAsync(Command);

            Assert.IsNotNull(fifthweekUser);
            Assert.AreEqual<string>(RegistrationData.Email, fifthweekUser.Email);
            Assert.AreEqual<string>(RegistrationData.Username, fifthweekUser.UserName);
            Assert.AreNotEqual<DateTime>(DateTime.MinValue, fifthweekUser.RegistrationDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, fifthweekUser.LastSignInDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, fifthweekUser.LastAccessTokenDate);
        }

        [TestMethod]
        public async Task WhenUserCreationSucceeds_ItShouldPublishEvent()
        {
            this.userRegistered.Setup(_ => _.HandleAsync(new UserRegisteredEvent(UserId))).Returns(Task.FromResult(0)).Verifiable();
            this.userManager.Setup(v => v.FindByEmailAsync(RegistrationData.Email)).ReturnsAsync(null);
            this.userManager.Setup(v => v.FindByNameAsync(RegistrationData.Username)).ReturnsAsync(null);
            this.userManager.Setup(v => v.CreateAsync(It.IsAny<FifthweekUser>(), RegistrationData.Password))
                .ReturnsAsync(new MockIdentityResult());

            await this.target.HandleAsync(Command);

            this.userRegistered.Verify();
        }

        [TestMethod]
        public async Task WhenUserCreationFails_ItShouldThrowAnException()
        {
            this.userManager.Setup(v => v.FindByEmailAsync(RegistrationData.Email)).ReturnsAsync(null);
            this.userManager.Setup(v => v.FindByNameAsync(RegistrationData.Username)).ReturnsAsync(null);
            this.userManager.Setup(v => v.CreateAsync(It.IsAny<FifthweekUser>(), RegistrationData.Password))
                .ReturnsAsync(new MockIdentityResult("One", "Two"));

            Exception exception = null;
            try
            {
                await this.target.HandleAsync(Command);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains(RegistrationData.Username));
            Assert.IsInstanceOfType(exception, typeof(AggregateException));

            var ae = (AggregateException)exception;
            Assert.AreEqual(2, ae.InnerExceptions.Count);
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "One"));
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "Two"));
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