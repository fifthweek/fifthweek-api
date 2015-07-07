namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ConfirmPasswordResetCommandHandlerTests
    {
        private const string Token = "abc";
        private const string NewPassword = "Secret";

        private static readonly Guid UserId = Guid.Parse("7265bc4f-555e-4386-ad57-701dbdbc78bb");
        
        private ConfirmPasswordResetCommand command;
        private Mock<IUserManager> userManager;
        private ConfirmPasswordResetCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.command = ConfirmPasswordResetCommandTests.NewCommand(new PasswordResetConfirmationData
            {
                UserId = new UserId(UserId),
                Token = Token,
                NewPassword = NewPassword
            });

            this.userManager = new Mock<IUserManager>();
            this.target = new ConfirmPasswordResetCommandHandler(this.userManager.Object);
        }

        [TestMethod]
        public async Task WhenTokenIsValid_ItShouldResetThePassword()
        {
            this.SetupUserManager(isTokenValid: true);

            this.userManager.Setup(_ => _.ResetPasswordAsync(UserId, Token, NewPassword))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            await this.target.HandleAsync(this.command);

            this.userManager.Verify();
        }

        [TestMethod]
        public async Task WhenTokenIsInvalid_ItShouldRaiseRecoverableException()
        {
            this.SetupUserManager(isTokenValid: false);

            Func<Task> badMethodCall = () => this.target.HandleAsync(this.command);

            await badMethodCall.AssertExceptionAsync<RecoverableException>();
        }

        private void SetupUserManager(bool isTokenValid)
        {
            this.userManager.Setup(_ => _.FindByIdAsync(UserId)).ReturnsAsync(new FifthweekUser
            {
                Id = UserId
            });

            this.userManager
                .Setup(_ =>
                    _.ValidatePasswordResetTokenAsync(
                        It.Is<FifthweekUser>(user => user.Id == UserId),
                        Token))
                .ReturnsAsync(isTokenValid);
        }
    }

    public static class ConfirmPasswordResetCommandTests
    {
        public static ConfirmPasswordResetCommand NewCommand(PasswordResetConfirmationData data)
        {
            return new ConfirmPasswordResetCommand(
                data.UserId, 
                data.Token, 
                ValidPassword.Parse(data.NewPassword));
        }
    }
}