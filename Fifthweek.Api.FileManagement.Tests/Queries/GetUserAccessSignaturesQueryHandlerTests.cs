namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [TestClass]
    public class GetUserAccessSignaturesQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly List<ChannelId> CreatorChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly List<ChannelId> SubscribedChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        
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
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, UserId, CreatorChannelIds, SubscribedChannelIds));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenRequestingForNonAuthenticatedUserId_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, new UserId(Guid.NewGuid()),CreatorChannelIds, SubscribedChannelIds));
        }

        [TestMethod]
        public async Task WhenUnauthenticated_ItShouldReturnAccessSignatureForPublicFiles()
        {
            var filesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", DateTime.UtcNow);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(Constants.PublicFileBlobContainerName, It.IsAny<DateTime>()))
                .ReturnsAsync(filesInformation);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null, CreatorChannelIds, SubscribedChannelIds));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(filesInformation, result.PublicSignature);
            Assert.AreEqual(0, result.PrivateSignatures.Count);
        }

        [TestMethod]
        public async Task WhenAuthenticated_ItShouldReturnAccessSignaturesForTheUser()
        {
            var now = DateTime.UtcNow;
            var expectedPublicExpiry = this.target.GetNextExpiry(now, true);
            var expectedPrivateExpiry = this.target.GetNextExpiry(now, false);
            var publicFilesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", expectedPublicExpiry);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(Constants.PublicFileBlobContainerName, expectedPublicExpiry))
                .ReturnsAsync(publicFilesInformation);

            var userContainerName1 = "creatorContainerName1";
            var userContainerName2 = "creatorContainerName2";
            var creatorContainerName1 = "containerName1";
            var creatorContainerName2 = "containerName2";
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(CreatorChannelIds[0])).Returns(userContainerName1);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(CreatorChannelIds[1])).Returns(userContainerName2);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(SubscribedChannelIds[0])).Returns(creatorContainerName1);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(SubscribedChannelIds[1])).Returns(creatorContainerName2);

            var userInformation1 = new BlobContainerSharedAccessInformation(userContainerName1, "useruri", "usersig", expectedPrivateExpiry);
            var userInformation2 = new BlobContainerSharedAccessInformation(userContainerName2, "useruri", "usersig", expectedPrivateExpiry);
            var creatorInformation1 = new BlobContainerSharedAccessInformation(creatorContainerName1, "useruri1", "usersig1", expectedPrivateExpiry);
            var creatorInformation2 = new BlobContainerSharedAccessInformation(creatorContainerName2, "useruri2", "usersig2", expectedPrivateExpiry);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(userContainerName1, expectedPrivateExpiry))
                .ReturnsAsync(userInformation1);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(userContainerName2, expectedPrivateExpiry))
                .ReturnsAsync(userInformation2);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(creatorContainerName1, expectedPrivateExpiry))
                .ReturnsAsync(creatorInformation1);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(creatorContainerName2, expectedPrivateExpiry))
                .ReturnsAsync(creatorInformation2);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, CreatorChannelIds, SubscribedChannelIds));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(publicFilesInformation, result.PublicSignature);

            Assert.AreEqual(4, result.PrivateSignatures.Count);

            Assert.AreEqual(CreatorChannelIds[0], result.PrivateSignatures[0].ChannelId);
            Assert.AreEqual(userInformation1, result.PrivateSignatures[0].Information);
            Assert.AreEqual(CreatorChannelIds[1], result.PrivateSignatures[1].ChannelId);
            Assert.AreEqual(userInformation2, result.PrivateSignatures[1].Information);
            Assert.AreEqual(SubscribedChannelIds[0], result.PrivateSignatures[2].ChannelId);
            Assert.AreEqual(creatorInformation1, result.PrivateSignatures[2].Information);
            Assert.AreEqual(SubscribedChannelIds[1], result.PrivateSignatures[3].ChannelId);
            Assert.AreEqual(creatorInformation2, result.PrivateSignatures[3].Information);
        }

        [TestMethod]
        public async Task WhenGettingNextPublicExpiry_ItShouldReturnTheNextWholeWeekTakingMinimumExpiryIntoAccount()
        {
            DateTime result;
            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 30, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 49, 59, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 50, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 30, 00, 00, 00, DateTimeKind.Utc), result);
        }

        [TestMethod]
        public async Task WhenGettingNextPrivateExpiry_ItShouldReturnTheNextWholeHourTakingMinimumExpiryIntoAccount()
        {
            DateTime result;
            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 30, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 49, 59, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 50, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 12, 00, 00, DateTimeKind.Utc), result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingNextPublicExpiry_ItShouldExpectTimesAsUtc()
        {
            this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00), true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingNextPrivateExpiry_ItShouldExpectTimesAsUtc()
        {
            this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00), false);
        }

        [TestMethod]
        public async Task WhenGettingNextPublicExpiry_ItShouldReturnTimesAsUtc()
        {
            var result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public async Task WhenGettingNextPrivateExpiry_ItShouldReturnTimesAsUtc()
        {
            var result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }
    }
}
