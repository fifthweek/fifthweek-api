namespace Fifthweek.Api.Identity.Membership.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Membership.Commands;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AccountSettingsControllerTests
    {
        private static readonly Guid SecurityGuid = Guid.NewGuid();
        private static readonly string SecurityToken = SecurityGuid.ToString();
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly UserId RequestedUserId = new UserId(Guid.NewGuid());
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");
        private static readonly ValidUsername Username = ValidUsername.Parse("username");
        private static readonly ValidPassword Password = ValidPassword.Parse("passw0rd");
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidCreatorName Name = ValidCreatorName.Parse("name");
        private static readonly string ContainerName = "containerName";
        private static readonly string BlobName = "blobName";
        private static readonly string FileUri = "uri";
        private static readonly FileInformation FileInformation = new FileInformation(FileId, ContainerName);

        private Mock<IGuidCreator> guidCreator;
        private Mock<IRequesterContext> requesterContext;
        private Mock<ICommandHandler<UpdateAccountSettingsCommand>> updateAccountSettings;
        private Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>> getAccountSettings;
        private Mock<ICommandHandler<UpdateCreatorAccountSettingsCommand>> promoteUserToCreator;
        private AccountSettingsController target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.updateAccountSettings = new Mock<ICommandHandler<UpdateAccountSettingsCommand>>();
            this.getAccountSettings = new Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>>();
            this.promoteUserToCreator = new Mock<ICommandHandler<UpdateCreatorAccountSettingsCommand>>();

            this.guidCreator.Setup(v => v.Create()).Returns(SecurityGuid);
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.target = new AccountSettingsController(
                this.guidCreator.Object,
                this.requesterContext.Object,
                this.updateAccountSettings.Object,
                this.getAccountSettings.Object,
                this.promoteUserToCreator.Object);
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
        public async Task WhenGetIsCalled_ItShouldCallTheQueryHandler()
        {
            var query = new GetAccountSettingsQuery(Requester, RequestedUserId);
            this.getAccountSettings.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(new GetAccountSettingsResult(Name, Username, Email, FileInformation))
                .Verifiable();

            var result = await this.target.Get(RequestedUserId.Value.EncodeGuid());

            this.getAccountSettings.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(Username, result.Username);
            Assert.AreEqual(Email, result.Email);
            Assert.AreEqual(FileInformation, result.ProfileImage);
        }

        [TestMethod]
        public async Task WhenGetIsCalledAndNoProfileImageExists_ItShouldNotReturnAnyFileInformation()
        {
            var query = new GetAccountSettingsQuery(Requester, RequestedUserId);
            this.getAccountSettings.Setup(v => v.HandleAsync(query))
                .ReturnsAsync(new GetAccountSettingsResult(Name, Username, Email, null))
                .Verifiable();

            var result = await this.target.Get(RequestedUserId.Value.EncodeGuid());

            this.getAccountSettings.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(Username, result.Username);
            Assert.AreEqual(Email, result.Email);
            Assert.IsNull(result.ProfileImage);
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

        [TestMethod]
        public async Task WhenPutIsCalled_ItShouldCallTheCommandHandler()
        {
            var command = new UpdateAccountSettingsCommand(Requester, RequestedUserId, Username, Email, SecurityToken, Password, FileId);
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
        public async Task WhenPutIsCalledWithNullPassword_ItShouldCallTheCommandHandler()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            var command = new UpdateAccountSettingsCommand(Requester, RequestedUserId, Username, Email, SecurityToken, null, FileId);
            this.updateAccountSettings.Setup(v => v.HandleAsync(command))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var updatedAccountSettings = new UpdatedAccountSettings
            {
                NewEmail = Email.Value,
                NewPassword = null,
                NewUsername = Username.Value,
                NewProfileImageId = FileId.Value.EncodeGuid(),
            };

            await this.target.Put(RequestedUserId.Value.EncodeGuid(), updatedAccountSettings);

            this.updateAccountSettings.Verify();
        }

        [TestMethod]
        public async Task WhenPutIsCalledWithNullProfileImageId_ItShouldCallTheCommandHandler()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            var command = new UpdateAccountSettingsCommand(Requester, RequestedUserId, Username, Email, SecurityToken, Password, null);
            this.updateAccountSettings.Setup(v => v.HandleAsync(command))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var updatedAccountSettings = new UpdatedAccountSettings
            {
                NewEmail = Email.Value,
                NewPassword = Password.Value,
                NewUsername = Username.Value,
                NewProfileImageId = null,
            };

            await this.target.Put(RequestedUserId.Value.EncodeGuid(), updatedAccountSettings);

            this.updateAccountSettings.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPutCreatorInformationIsCalledWithNullUserId_ItShouldThrowAnException()
        {
            await this.target.PutCreatorInformation(null, new CreatorInformation());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPutCreatorInformationIsCalledWithNullCreatorInformation_ItShouldThrowAnException()
        {
            await this.target.PutCreatorInformation(RequestedUserId.Value.EncodeGuid(), null);
        }

        [TestMethod]
        public async Task WhenPutCreatorInformationIsCalled_ItShouldCallTheCommandHandler()
        {
            var command = new UpdateCreatorAccountSettingsCommand(Requester, RequestedUserId, Name);
            this.promoteUserToCreator.Setup(v => v.HandleAsync(command))
                .Returns(Task.FromResult(0))
                .Verifiable();

            var updatedAccountSettings = new CreatorInformation
            {
                Name = Name.Value,
            };

            await this.target.PutCreatorInformation(RequestedUserId.Value.EncodeGuid(), updatedAccountSettings);

            this.updateAccountSettings.Verify();
        }
    }
}
