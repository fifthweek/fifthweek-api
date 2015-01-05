namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        [TestMethod]
        public async Task WhenUserManagerFindsEmail_ItShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(new FifthweekUser());
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(null);

            var target = new RegisterUserCommandHandler(userManager.Object);
            Exception exception = null;
            try
            {
                await target.HandleAsync(this.CreateRegisterUserCommand());
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
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(new FifthweekUser());

            var target = new RegisterUserCommandHandler(userManager.Object);
            Exception exception = null;
            try
            {
                await target.HandleAsync(this.CreateRegisterUserCommand());
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
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(null);
            userManager.Setup(v => v.CreateAsync(It.IsAny<FifthweekUser>(), this.registrationData.Password))
                .Callback<FifthweekUser, string>((u, p) => fifthweekUser = u)
                .ReturnsAsync(new MockIdentityResult());

            var target = new RegisterUserCommandHandler(userManager.Object);
            await target.HandleAsync(this.CreateRegisterUserCommand());

            Assert.IsNotNull(fifthweekUser);
            Assert.AreEqual<string>(this.registrationData.Email, fifthweekUser.Email);
            Assert.AreEqual<string>(this.registrationData.Username, fifthweekUser.UserName);
            Assert.AreNotEqual<DateTime>(DateTime.MinValue, fifthweekUser.RegistrationDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, fifthweekUser.LastSignInDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, fifthweekUser.LastAccessTokenDate);
        }

        [TestMethod]
        public async Task WhenUserCreationFails_ItShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(null);
            userManager.Setup(v => v.CreateAsync(It.IsAny<FifthweekUser>(), this.registrationData.Password))
                .ReturnsAsync(new MockIdentityResult("One", "Two"));

            var target = new RegisterUserCommandHandler(userManager.Object);
            Exception exception = null;
            try
            {
                await target.HandleAsync(this.CreateRegisterUserCommand());
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains(this.registrationData.Username));
            Assert.IsInstanceOfType(exception, typeof(AggregateException));

            var ae = (AggregateException)exception;
            Assert.AreEqual(2, ae.InnerExceptions.Count);
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "One"));
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "Two"));
        }

        private RegisterUserCommand CreateRegisterUserCommand()
        {
            return RegisterUserCommandTests.NewCommand(Guid.Empty, this.registrationData);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.registrationData = new RegistrationData
            {
                Email = "test@testing.fifthweek.com",
                ExampleWork = "testing.fifthweek.com",
                Password = "TestPassword",
                Username = "test_username",
            };
        }

        private RegistrationData registrationData;
    }
}