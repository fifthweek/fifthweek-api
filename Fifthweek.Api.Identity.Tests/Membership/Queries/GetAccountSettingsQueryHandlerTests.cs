namespace Fifthweek.Api.Identity.Tests.Membership.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAccountSettingsQueryHandlerTests
    {
        private static readonly Username Username = new Username("username");
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ValidEmail Email = ValidEmail.Parse("test@testing.fifthweek.com");

        private GetAccountSettingsQueryHandler target;
        private Mock<IGetAccountSettingsDbStatement> getAccountSettings;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFileInformationAggregator> fileInformationAggregator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            this.getAccountSettings = new Mock<IGetAccountSettingsDbStatement>();
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();

            this.target = new GetAccountSettingsQueryHandler(this.requesterSecurity.Object, this.getAccountSettings.Object, this.fileInformationAggregator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledWithUnauthorizedUserId_ItShouldThrowAnException()
        {
            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId))
                .Throws(new Exception("This should not be called"));

            await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullQuery_ItThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallTheAccountRepository()
        {
            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(new GetAccountSettingsDbResult(Username, Email, null))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId));

            this.getAccountSettings.Verify();

            Assert.IsNotNull(result);
            Assert.AreEqual(Username, result.Username);
            Assert.AreEqual(Email, result.Email);
            Assert.AreEqual(null, result.ProfileImage);
        }

        [TestMethod]
        public async Task WhenAccountSettingsHasAProfileImageFileId_ItShouldReturnTheBlobInformation()
        {
            const string ContainerName = "containerName";
            const string BlobName = "blobName";
            const string BlobUri = "uri";

            this.getAccountSettings.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(new GetAccountSettingsDbResult(Username, Email, FileId));

            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(UserId, FileId, FilePurposes.ProfileImage))
                .ReturnsAsync(new FileInformation(FileId, ContainerName, BlobName, BlobUri));

            var result = await this.target.HandleAsync(new GetAccountSettingsQuery(Requester, UserId));

            Assert.IsNotNull(result);
            Assert.AreEqual(Username, result.Username);
            Assert.AreEqual(Email, result.Email);
            Assert.IsNotNull(result.ProfileImage);
            Assert.AreEqual(FileId, result.ProfileImage.FileId);
            Assert.AreEqual(ContainerName, result.ProfileImage.ContainerName);
            Assert.AreEqual(BlobName, result.ProfileImage.BlobName);
            Assert.AreEqual(BlobUri, result.ProfileImage.Uri);
        }
    }
}
