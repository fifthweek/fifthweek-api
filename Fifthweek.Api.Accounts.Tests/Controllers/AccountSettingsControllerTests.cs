namespace Fifthweek.Api.Accounts.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AccountSettingsControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly UserId RequestedUserId = new UserId(Guid.NewGuid());
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");
        private static readonly ValidUsername Username = ValidUsername.Parse("username");
        private static readonly ValidPassword Password = ValidPassword.Parse("passw0rd");
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IUserContext> userContext;
        private Mock<ICommandHandler<UpdateAccountSettingsCommand>> updateAccountSettings;
        private Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>> getAccountSettings;
        private AccountSettingsController target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.userContext = new Mock<IUserContext>();
            this.updateAccountSettings = new Mock<ICommandHandler<UpdateAccountSettingsCommand>>();
            this.getAccountSettings = new Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>>();

            this.target = new AccountSettingsController(
                this.userContext.Object,
                this.updateAccountSettings.Object,
                this.getAccountSettings.Object);
        }

        [TestMethod]
        public async Task WhenGetIsCalled_ItShouldCallTheQueryHandler()
        {
            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);

            var query = new GetAccountSettingsQuery(Requester, RequestedUserId);
            this.getAccountSettings.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(new GetAccountSettingsResult(Email, FileId))
                .Verifiable();

            var result = await this.target.Get(RequestedUserId.Value.EncodeGuid());

            this.getAccountSettings.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(Email.Value, result.Email);
            Assert.AreEqual(FileId.Value.EncodeGuid(), result.ProfileImageFileId);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGetIsCalledWithEmptyUserId_ItShouldThrowAnException()
        {
            await this.target.Get(" ");
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGetIsCalledWithNullUserId_ItShouldThrowAnException()
        {
            await this.target.Get(null);
        }

        [TestMethod]
        public async Task WhenPutIsCalled_ItShouldCallTheCommandHandler()
        {
            this.userContext.Setup(v => v.GetRequester()).Returns(Requester);

            var command = new UpdateAccountSettingsCommand(Requester, RequestedUserId, Username, Email, Password, FileId);
            this.updateAccountSettings.Setup(v => v.HandleAsync(command))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var updatedAccountSettings = new UpdatedAccountSettings 
            {
                 NewEmail = Email.Value,
                 NewPassword = Password.Value,
                 NewUsername = Username.Value,
                 NewProfileImageId = FileId.Value.EncodeGuid(),
            };
            
            await this.target.Put(RequestedUserId.Value.EncodeGuid(), updatedAccountSettings);

            this.updateAccountSettings.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPutIsCalledWithNullUserId_ItShouldThrowAnException()
        {
            await this.target.Put(null, new UpdatedAccountSettings());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPutIsCalledWithNullUpdatedAccountSettings_ItShouldThrowAnException()
        {
            await this.target.Put(RequestedUserId.Value.EncodeGuid(), null);
        }
    }
}
