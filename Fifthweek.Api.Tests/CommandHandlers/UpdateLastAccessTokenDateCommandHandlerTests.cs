namespace Fifthweek.Api.Tests.CommandHandlers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.CommandHandlers;
    using Fifthweek.Api.Commands;
    using Fifthweek.Api.Entities;
    using Fifthweek.Api.Repositories;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateLastAccessTokenDateCommandHandlerTests
    {
        private string testUsername = "TestUsername";

        [TestMethod]
        public async Task WhenTheUsernameIsNotFoundItShouldThrowAnException()
        {
            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(this.testUsername)).ReturnsAsync(null);

            var target = new UpdateLastAccessTokenDateCommandHandler(userManager.Object);

            Exception exception = null;
            try
            {
                var command = new UpdateLastAccessTokenDateCommand(
                    this.testUsername,
                    DateTime.UtcNow,
                    UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn);
                await target.HandleAsync(command);
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains("username"));
            Assert.IsTrue(exception.Message.Contains(this.testUsername));
            Assert.IsTrue(exception.Message.Contains("not found"));
        }

        [TestMethod]
        public async Task WhenTheCreationTypeIsSignInItSouldUpdateBothTimestamps()
        {
            var applicationUser = new ApplicationUser
            {
                LastAccessTokenDate = DateTime.MinValue,
                LastSignInDate = DateTime.MinValue,
            };

            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(this.testUsername)).ReturnsAsync(applicationUser);
            userManager.Setup(v => v.UpdateAsync(applicationUser)).ReturnsAsync(new MockIdentityResult()).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userManager.Object);

            var command = new UpdateLastAccessTokenDateCommand(
                this.testUsername,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn);

            await target.HandleAsync(command);

            userManager.Verify();

            Assert.AreEqual(command.Timestamp, applicationUser.LastAccessTokenDate);
            Assert.AreEqual(command.Timestamp, applicationUser.LastSignInDate);
        }

        [TestMethod]
        public async Task WhenTheCreationTypeIsRefreshTokenItSouldUpdateAccessTokenTimestampOnly()
        {
            var applicationUser = new ApplicationUser
            {
                LastAccessTokenDate = DateTime.MinValue,
                LastSignInDate = DateTime.MinValue,
            };

            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(this.testUsername)).ReturnsAsync(applicationUser);
            userManager.Setup(v => v.UpdateAsync(applicationUser)).ReturnsAsync(new MockIdentityResult()).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userManager.Object);

            var command = new UpdateLastAccessTokenDateCommand(
                this.testUsername,
                DateTime.UtcNow,
                UpdateLastAccessTokenDateCommand.AccessTokenCreationType.RefreshToken);

            await target.HandleAsync(command);

            userManager.Verify();

            Assert.AreEqual(command.Timestamp, applicationUser.LastAccessTokenDate);
            Assert.AreEqual(DateTime.MinValue, applicationUser.LastSignInDate);
        }

        [TestMethod]
        public async Task WhenUpdatingTheUserFailsItShouldThrowAnException()
        {
            var applicationUser = new ApplicationUser
            {
                LastAccessTokenDate = DateTime.MinValue,
                LastSignInDate = DateTime.MinValue,
            };

            var userManager = new Mock<IUserManager>();
            userManager.Setup(v => v.FindByNameAsync(this.testUsername)).ReturnsAsync(applicationUser);
            userManager.Setup(v => v.UpdateAsync(applicationUser)).ReturnsAsync(
                new MockIdentityResult("One", "Two")).Verifiable();

            var target = new UpdateLastAccessTokenDateCommandHandler(userManager.Object);

            Exception exception = null;
            try
            {
                var command = new UpdateLastAccessTokenDateCommand(
                    this.testUsername,
                    DateTime.UtcNow,
                    UpdateLastAccessTokenDateCommand.AccessTokenCreationType.SignIn);

                await target.HandleAsync(command);
            }
            catch (Exception t)
            {
                exception = t;
            }
            
            userManager.Verify();

            Assert.IsNotNull(exception);
            Assert.IsTrue(exception.Message.Contains(this.testUsername));
            Assert.IsInstanceOfType(exception, typeof(AggregateException));

            var ae = (AggregateException)exception;
            Assert.AreEqual(2, ae.InnerExceptions.Count);
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "One"));
            Assert.IsTrue(ae.InnerExceptions.Any(v => v.Message == "Two"));
        }
    }
}