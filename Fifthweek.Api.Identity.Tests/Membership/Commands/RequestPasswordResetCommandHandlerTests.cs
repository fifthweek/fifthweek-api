using Fifthweek.Api.Identity.Tests.Membership.Controllers;

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
            await this.target.HandleAsync(this.requestPasswordResetCommand);
            Assert.Fail();
        }

        [TestMethod]
        public async Task WhenUsernameDoesNotExist_ItShouldDoNothing()
        {
            await this.target.HandleAsync(this.requestPasswordResetCommand);
            Assert.Fail();
        }

        [TestMethod]
        public async Task WhenEmailDoesNotExist_ItShouldDoNothing()
        {
            await this.target.HandleAsync(this.requestPasswordResetCommand);
            Assert.Fail();
        }

        [TestMethod]
        public async Task WhenUsernameExists_ItShouldSendEmail()
        {
            await this.target.HandleAsync(this.requestPasswordResetCommand);
            Assert.Fail();
        }

        [TestMethod]
        public async Task WhenEmailExists_ItShouldSendEmail()
        {
            await this.target.HandleAsync(this.requestPasswordResetCommand);
            Assert.Fail();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.passwordResetRequestData = PasswordResetRequestDataTests.NewPasswordResetRequestData();
            this.requestPasswordResetCommand = RequestPasswordResetCommandTests.NewRequestPasswordResetCommand(this.passwordResetRequestData);
            this.userManager = new Mock<IUserManager>();
            this.target = new RequestPasswordResetCommandHandler(this.userManager.Object);
        }

        private PasswordResetRequestData passwordResetRequestData;
        private RequestPasswordResetCommand requestPasswordResetCommand;
        private Mock<IUserManager> userManager;
        private RequestPasswordResetCommandHandler target;
    }
}