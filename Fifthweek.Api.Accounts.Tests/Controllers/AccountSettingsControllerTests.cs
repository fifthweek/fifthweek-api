namespace Fifthweek.Api.Accounts.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Accounts.Commands;
    using Fifthweek.Api.Accounts.Controllers;
    using Fifthweek.Api.Accounts.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AccountSettingsControllerTests
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());
        
        private readonly UserId requestedUserId = new UserId(Guid.NewGuid());

        private readonly ValidEmail email = ValidEmail.Parse("test@testing.fifthweek.com");

        private readonly ValidUsername username = ValidUsername.Parse("username");

        private readonly ValidPassword password = ValidPassword.Parse("passw0rd");

        private readonly FileId fileId = new FileId(Guid.NewGuid());
        
        private AccountSettingsController target;

        private Mock<IUserContext> userContext;

        private Mock<ICommandHandler<UpdateAccountSettingsCommand>> updateAccountSettings;

        private Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>> getAccountSettings;

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
            this.userContext.Setup(v => v.GetUserId()).Returns(this.userId);

            var query = new GetAccountSettingsQuery(this.userId, this.requestedUserId);
            this.getAccountSettings.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(new GetAccountSettingsResult(this.email, this.fileId))
                .Verifiable();

            var result = await this.target.Get(this.requestedUserId.Value.EncodeGuid());

            this.getAccountSettings.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(this.email.Value, result.Email);
            Assert.AreEqual(this.fileId.Value.EncodeGuid(), result.ProfileImageFileId);
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
            this.userContext.Setup(v => v.GetUserId()).Returns(this.userId);

            var command = new UpdateAccountSettingsCommand(this.userId, this.requestedUserId, this.username, this.email, this.password, this.fileId);
            this.updateAccountSettings.Setup(v => v.HandleAsync(command))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var updatedAccountSettings = new UpdatedAccountSettings 
            {
                 NewEmail = this.email.Value,
                 NewPassword = this.password.Value,
                 NewUsername = this.username.Value,
                 NewProfileImageId = this.fileId.Value.EncodeGuid(),
            };
            
            await this.target.Put(this.requestedUserId.Value.EncodeGuid(), updatedAccountSettings);

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
            await this.target.Put(this.requestedUserId.Value.EncodeGuid(), null);
        }
    }
}
