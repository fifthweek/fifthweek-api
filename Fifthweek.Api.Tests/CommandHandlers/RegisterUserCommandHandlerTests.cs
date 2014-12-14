namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Models;
    using Fifthweek.Api.Repositories;

    using Microsoft.AspNet.Identity;
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
                await target.HandleAsync(new RegisterUserCommand(this.registrationData));
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
                await target.HandleAsync(new RegisterUserCommand(this.registrationData));
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
            await target.HandleAsync(new RegisterUserCommand(this.registrationData));

            Assert.IsNotNull(applicationUser);
            Assert.AreEqual(this.registrationData.Email, applicationUser.Email);
            Assert.AreEqual(this.registrationData.Username, applicationUser.UserName);
            Assert.AreEqual(this.registrationData.ExampleWork, applicationUser.ExampleWork);
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
                await target.HandleAsync(new RegisterUserCommand(this.registrationData));
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

        /// <summary>
        /// This is required as the constructor for IdentityResult that indicates success is protected.
        /// </summary>
        private class MockIdentityResult : IdentityResult
        {
            public MockIdentityResult()
                : base(true)
            {
            }

            public MockIdentityResult(params string[] errors)
                : base(errors)
            {
            }
        }
    }
}