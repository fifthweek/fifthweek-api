namespace Fifthweek.Api.Identity.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserCommandHandlerTests
    {
        private RegistrationData registrationData;

        [TestInitialize]
        public void TestInitialize()
        {
            this.registrationData = new RegistrationData
            {
                Email = "test@testing.fifthweek.com",
                ExampleWork = "testing.fifthweek.com",
                Password = "TestPassword",
                Username = "TestUsername",
            };
        }

        [TestMethod]
        public async Task WhenUserManagerFindsEmailShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(new ApplicationUser());
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
        public async Task WhenUserManagerFindsUsernameShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(new ApplicationUser());

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
        public async Task WhenCredentialsAcceptedShouldCreateUser()
        {
            ApplicationUser applicationUser = null;
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(null);
            userManager.Setup(v => v.CreateAsync(It.IsAny<ApplicationUser>(), this.registrationData.Password))
                .Callback<ApplicationUser, string>((u, p) => applicationUser = u)
                .ReturnsAsync(new MockIdentityResult());

            var target = new RegisterUserCommandHandler(userManager.Object);
            await target.HandleAsync(this.CreateRegisterUserCommand());

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual<string>(this.registrationData.Email, applicationUser.Email);
            Assert.AreEqual<string>(this.registrationData.Username, applicationUser.UserName);
            Assert.AreNotEqual<DateTime>(DateTime.MinValue, applicationUser.RegistrationDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, applicationUser.LastSignInDate);
            Assert.AreEqual<DateTime>(SqlDateTime.MinValue.Value, applicationUser.LastAccessTokenDate);
        }

        [TestMethod]
        public async Task WhenUserCreationFailsShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByEmailAsync(this.registrationData.Email)).ReturnsAsync(null);
            userManager.Setup(v => v.FindByNameAsync(this.registrationData.Username)).ReturnsAsync(null);
            userManager.Setup(v => v.CreateAsync(It.IsAny<ApplicationUser>(), this.registrationData.Password))
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
            return RegisterUserCommandTests.NewRegisterUserCommand(this.registrationData);
        }
    }
}