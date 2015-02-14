namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestPasswordResetCommandHandlerTests
    {
        private const string Token = "abc";
        private const string EmailAddress = "test@example.com";
        private const string Username = "test_user";
        private const string EmailSubject = "Reset Password";

        private static readonly Guid UserId = Guid.NewGuid();
        private static readonly Guid UserId2 = Guid.NewGuid();
        
        private readonly string activationLink = string.Format("\"https://www.fifthweek.com/#/resetPassword?userId={0}&token={1}\"", UserId.EncodeGuid(), Token);
        private Mock<IUserManager> userManager;
        private Mock<IHtmlLinter> htmlLinter;
        private RequestPasswordResetCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.htmlLinter = new Mock<IHtmlLinter>();
            this.htmlLinter.Setup(_ => _.RemoveWhitespaceForHtmlEmail(It.IsAny<string>())).Returns((string result) => result);

            // Mock potentially side-effecting components with strict mock behaviour.
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);
            
            this.target = new RequestPasswordResetCommandHandler(this.userManager.Object, this.htmlLinter.Object);
        }

        [TestMethod]
        public async Task WhenUsernameAndEmailAreNotProvided_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(null, null);
            
            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameAndEmailDoNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), ValidUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameDoesNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(null, ValidUsername.Parse(Username));
            
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);
            
            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenEmailDoesNotExist_ItShouldDoNothing()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), null);

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        public async Task WhenUsernameExists_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(null, ValidUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId,
                UserName = Username
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExists_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), null);

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId,
                UserName = Username
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenUsernameExistsAndEmailDoesNotExist_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), ValidUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId,
                UserName = Username
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExistsAndUsernameDoesNotExist_ItShouldSendEmail()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), ValidUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(null);
            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId,
                UserName = Username
            });

            await this.AssertEmailSent(command);
        }

        [TestMethod]
        public async Task WhenEmailExistsAndUsernameExists_ItShouldSendEmailToUserIdentifiedByUsername()
        {
            var command = new RequestPasswordResetCommand(ValidEmail.Parse(EmailAddress), ValidUsername.Parse(Username));

            this.userManager.Setup(_ => _.FindByNameAsync(Username)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId,
                UserName = Username
            });
            this.userManager.Setup(_ => _.FindByEmailAsync(EmailAddress)).ReturnsAsync(new FifthweekUser()
            {
                Id = UserId2,
                UserName = Username
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
                    It.Is<string>(emailBody => emailBody.Contains(activationLink) && emailBody.Contains(Username))))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(command);

            this.userManager.Verify();
        }
    }

    public static class RequestPasswordResetCommandTests
    {
        public static RequestPasswordResetCommand NewCommand(PasswordResetRequestData data)
        {
            return new RequestPasswordResetCommand(
                data.Email == null ? null : ValidEmail.Parse(data.Email),
                data.Username == null ? null : ValidUsername.Parse(data.Username));
        }
    }
}