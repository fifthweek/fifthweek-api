namespace Fifthweek.Api.Identity.Membership.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateAccountSettingsCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidUsername Username = ValidUsername.Parse("username");
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");
        private static readonly ValidPassword Password = ValidPassword.Parse("passw0rd");
        private static readonly string SecurityToken = Guid.NewGuid().ToString();

        private Mock<IUpdateAccountSettingsDbStatement> updateAccountSettings;
        private Mock<IFileSecurity> fileSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IReservedUsernameService> reservedUsernames;
        private UpdateAccountSettingsCommandHandler target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileSecurity = new Mock<IFileSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.reservedUsernames = new Mock<IReservedUsernameService>();
            this.requesterSecurity.SetupFor(Requester);

            // Give side-effecting components strict mock behaviour.
            this.updateAccountSettings = new Mock<IUpdateAccountSettingsDbStatement>(MockBehavior.Strict);

            this.target = new UpdateAccountSettingsCommandHandler(this.updateAccountSettings.Object, this.requesterSecurity.Object, this.fileSecurity.Object, this.reservedUsernames.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedFileId_ItShouldCallThrowAnUnauthroizedException()
        {
            var command = new UpdateAccountSettingsCommand(
                Requester,
                UserId,
                Username,
                Email,
                SecurityToken,
                Password,
                FileId);

            this.fileSecurity.Setup(v => v.AssertReferenceAllowedAsync(UserId, FileId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(command);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithoutACommand_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new UpdateAccountSettingsCommand(
                Requester.Unauthenticated,
                UserId,
                Username,
                Email,
                SecurityToken,
                Password,
                FileId));
        }

        [TestMethod]
        public async Task WhenCalledWithValidData_ItShouldCallTheAccountRepository()
        {
            this.reservedUsernames.Setup(v => v.AssertNotReserved(Username)).Verifiable();

            var command = new UpdateAccountSettingsCommand(
                Requester,
                UserId,
                Username,
                Email,
                SecurityToken,
                Password,
                FileId);

            this.fileSecurity.Setup(v => v.AssertReferenceAllowedAsync(UserId, FileId))
                .Returns(Task.FromResult(0));

            this.updateAccountSettings.Setup(
                v =>
                v.ExecuteAsync(
                    UserId,
                    Username,
                    Email,
                    Password,
                    FileId,
                    SecurityToken)).ReturnsAsync(new UpdateAccountSettingsDbStatement.UpdateAccountSettingsResult(false))
                    .Verifiable();

            await this.target.HandleAsync(command);

            this.reservedUsernames.Verify();
            this.updateAccountSettings.Verify();
        }
    }
}
