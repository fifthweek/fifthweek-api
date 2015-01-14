using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Accounts.Tests.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;

    using Moq;

    [TestClass]
    public class UpdateAccountSettingsCommandHandlerTests
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private readonly ValidUsername username = ValidUsername.Parse("username");

        private readonly ValidEmail email = ValidEmail.Parse("test@testing.fifthweek.com");

        private readonly ValidPassword password = ValidPassword.Parse("passw0rd");

        private Mock<IAccountRepository> accountRepository;

        private Mock<IFileSecurity> fileSecurity;

        private UpdateAccountSettingsCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.accountRepository = new Mock<IAccountRepository>();
            this.fileSecurity = new Mock<IFileSecurity>();

            this.target = new UpdateAccountSettingsCommandHandler(this.accountRepository.Object, this.fileSecurity.Object);
        }

        [TestMethod]
        public async Task WhenCalledWithValidData_ItShouldCallTheAccountRepository()
        {
            var command = new UpdateAccountSettingsCommand(
                this.userId,
                this.userId,
                this.username,
                this.email,
                this.password,
                this.fileId);

            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(this.userId, this.fileId))
                .Returns(Task.FromResult(0));

            this.accountRepository.Setup(
                v =>
                v.UpdateAccountSettingsAsync(
                    this.userId,
                    this.username,
                    this.email,
                    this.password,
                    this.fileId)).ReturnsAsync(new AccountRepository.UpdateAccountSettingsResult(false))
                    .Verifiable();

            await this.target.HandleAsync(command);

            this.accountRepository.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedFileId_ItShouldCallThrowAnUnauthroizedException()
        {
            var command = new UpdateAccountSettingsCommand(
                this.userId,
                this.userId,
                this.username,
                this.email,
                this.password,
                this.fileId);

            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(this.userId, this.fileId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithoutACommand_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }
    }
}
