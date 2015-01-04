using System;
using Fifthweek.Api.Identity.Tests.Membership.Controllers;
using Fifthweek.Api.Persistence;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestPasswordResetCommandHandlerTests
    {
        [TestMethod]
        public async Task WhenUsernameAndEmailAreNotProvided_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(null, null);
            
            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameAndEmailDoNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), NormalizedUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameDoesNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(null, NormalizedUsername.Parse(Username));
            
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);
            
            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenEmailDoesNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), null);

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameExists_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(null, NormalizedUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExists_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), null);

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenUsernameExistsAndEmailDoesNotExist_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), NormalizedUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExistsAndUsernameDoesNotExist_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), NormalizedUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExistsAndUsernameExists_ItShouldSendEmailToUserIdentifiedByUsername()
        {
            var command = new RequestPasswordResetCommand(NormalizedEmail.Parse(EmailAddress), NormalizedUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId
            });
            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new ApplicationUser()
            {
                Id = UserId2
            });
            
            await this.AssertEmailSent(command);
        }

        private async Task AssertEmailSent(RequestPasswordResetCommand command)
        {
            this.userManager.Setup(_ => _.GeneratePasswordResetTokenAsync(UserId)).ReturnsAsync(Token);

            this.userManager.Setup(_ =>
                _.SendEmailAsync(
                    UserId,
                    EmailSubject,
                    It.Is<string>(emailBody => emailBody.Contains(activationLink))))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(command);

            this.userManager.Verify();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.passwordResetRequestData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            this.requestPasswordResetCommand = RequestPasswordResetCommandTests.NewRequestPasswordResetCommand(this.passwordResetRequestData);
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);
            this.target = new RequestPasswordResetCommandHandler(this.userManager.Object);
        }
        
        private const string UserId = "123";
        private const string UserId2 = "789";
        private const string Token = "abc";
        private const string EmailAddress = "test@example.com";
        private const string Username = "test_user";
        private const string EmailSubject = "Reset Password";
        private readonly string activationLink = string.Format("\"https://www.fifthweek.com/#/resetPassword?userId={0}&token={1}\"", UserId, Token);
        private PasswordResetRequestData passwordResetRequestData;
        private RequestPasswordResetCommand requestPasswordResetCommand;
        private Mock<IUserManager> userManager;
        private RequestPasswordResetCommandHandler target;
    }
}