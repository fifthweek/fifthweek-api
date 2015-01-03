using System;
using Fifthweek.Api.Core;
using Fifthweek.Api.Identity.Tests.Membership.Controllers;
using Fifthweek.Api.Persistence;
using Microsoft.AspNet.Identity;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ConfirmPasswordResetCommandHandlerTests
    {
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
        [ExpectedException(typeof(RecoverableException))]
        public async Task WhenTokenIsInvalid_ItShouldRaiseRecoverableException()
        {
            this.SetupUserManager(isTokenValid: false);

            await this.target.HandleAsync(this.command);

            Assert.Fail("Expected a recoverable exception.");
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

        [TestInitialize]
        public void TestInitialize()
        {
            this.command = ConfirmPasswordResetCommandTests.NewCommand(new PasswordResetConfirmationData
            {
                UserId = UserId,
                Token = Token,
                NewPassword = NewPassword
            });

            this.userManager = new Mock<IUserManager>();
            this.userTokenProvider = new Mock<IUserTokenProvider<FifthweekUser, Guid>>();
            this.target = new ConfirmPasswordResetCommandHandler(this.userManager.Object);
        }

        private static readonly Guid UserId = Guid.Parse("7265bc4f-555e-4386-ad57-701dbdbc78bb");
        private const string Token = "abc";
        private const string NewPassword = "Secret";
        private ConfirmPasswordResetCommand command;
        private Mock<IUserManager> userManager;
        private Mock<IUserTokenProvider<FifthweekUser, Guid>> userTokenProvider;
        private ConfirmPasswordResetCommandHandler target;
    }
}