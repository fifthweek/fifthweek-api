namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class GetUserAccessSignaturesQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        
        private GetUserAccessSignaturesQueryHandler target;

        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobLocationGenerator;
        private Mock<IRequesterSecurity> requesterSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.blobLocationGenerator = new Mock<IBlobLocationGenerator>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();

            this.target = new GetUserAccessSignaturesQueryHandler(
                this.blobService.Object,
                this.blobLocationGenerator.Object,
                this.requesterSecurity.Object);

            this.requesterSecurity.SetupFor(Requester);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticatedAndRequestingForUserId_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenRequestingForNonAuthenticatedUserId_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        public async Task WhenUnauthenticated_ItShouldReturnAccessSignatureForPublicFiles()
        {
            var filesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", DateTime.UtcNow);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(FileManagement.Constants.PublicFileBlobContainerName))
                .ReturnsAsync(filesInformation);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(filesInformation, result.PublicSignature);
            Assert.AreEqual(0, result.PrivateSignatures.Count);
        }

        [TestMethod]
        public async Task WhenAuthenticated_ItShouldReturnAccessSignaturesForTheUser()
        {
            var filesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", DateTime.UtcNow);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(FileManagement.Constants.PublicFileBlobContainerName))
                .ReturnsAsync(filesInformation);

            var userContainerName = "containerName";
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(UserId)).Returns(userContainerName);

            var userInformation = new BlobContainerSharedAccessInformation(userContainerName, "useruri", "usersig", DateTime.UtcNow);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(userContainerName))
                .ReturnsAsync(userInformation);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(filesInformation, result.PublicSignature);

            Assert.AreEqual(1, result.PrivateSignatures.Count);

            Assert.AreEqual(UserId, result.PrivateSignatures[0].CreatorId);
            Assert.AreEqual(userInformation, result.PrivateSignatures[0].Information);
        }
    }
}