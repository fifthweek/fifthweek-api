using System;
using Fifthweek.Api.Identity.Tests.Membership.Controllers;
using Fifthweek.Api.Persistence;
using Microsoft.AspNet.Identity;

namespace Fifthweek.Api.Identity.Tests.Membership.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ConfirmPasswordResetCommandHandlerTests
    {
        [TestMethod]
        public async Task ItShouldResetThePassword()
        {
            var command = ConfirmPasswordResetCommandTests.NewCommand(new PasswordResetConfirmationData
            {
                UserId = userId,
                Token = Token,
                NewPassword = NewPassword
            });

            this.userManager.Setup(_ => _.ResetPasswordAsync(userId.ToString(), Token, NewPassword))
                .ReturnsAsync(IdentityResult.Success)
                .Verifiable();

            await this.target.HandleAsync(command);

            this.userManager.Verify();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);
            this.target = new ConfirmPasswordResetCommandHandler(this.userManager.Object);
        }
        
        private readonly Guid userId = Guid.NewGuid();
        private const string Token = "abc";
        private const string NewPassword = "Secret";
        private Mock<IUserManager> userManager;
        private ConfirmPasswordResetCommandHandler target;
    }
}