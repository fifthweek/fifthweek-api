﻿namespace Fifthweek.Api.FileManagement.Tests.Queries
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
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Shared.Constants;

    [TestClass]
    public class GetUserAccessSignaturesQueryHandlerTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly List<ChannelId> CreatorChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly List<ChannelId> SubscribedChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly List<ChannelId> FreeAccessChannelIds = new List<ChannelId> { new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        
        private GetUserAccessSignaturesQueryHandler target;

        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IGetAccessSignatureExpiryInformation> getAccessSignatureExpiryInformation;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobLocationGenerator;
        private Mock<IRequesterSecurity> requesterSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.getAccessSignatureExpiryInformation = new Mock<IGetAccessSignatureExpiryInformation>();
            this.blobService = new Mock<IBlobService>();
            this.blobLocationGenerator = new Mock<IBlobLocationGenerator>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.target = new GetUserAccessSignaturesQueryHandler(
                this.timestampCreator.Object,
                this.getAccessSignatureExpiryInformation.Object,
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
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, UserId, CreatorChannelIds, SubscribedChannelIds, FreeAccessChannelIds));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenRequestingForNonAuthenticatedUserId_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, new UserId(Guid.NewGuid()), CreatorChannelIds, SubscribedChannelIds, FreeAccessChannelIds));
        }

        [TestMethod]
        public async Task WhenUnauthenticated_ItShouldReturnAccessSignatureForPublicFiles()
        {
            var expectedPublicExpiry = Now.AddDays(1);
            var expectedPrivateExpiry = Now.AddDays(2);
            this.getAccessSignatureExpiryInformation.Setup(v => v.Execute(Now))
                .Returns(new AccessSignatureExpiryInformation(expectedPublicExpiry, expectedPrivateExpiry));

            var filesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", expectedPublicExpiry);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(Constants.PublicFileBlobContainerName, expectedPublicExpiry))
                .ReturnsAsync(filesInformation);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null, CreatorChannelIds, SubscribedChannelIds, FreeAccessChannelIds));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(filesInformation, result.PublicSignature);
            Assert.AreEqual(0, result.PrivateSignatures.Count);
        }

        [TestMethod]
        public async Task WhenAuthenticated_ItShouldReturnAccessSignaturesForTheUser()
        {
            var expectedPublicExpiry = Now.AddDays(1);
            var expectedPrivateExpiry = Now.AddDays(2);
            this.getAccessSignatureExpiryInformation.Setup(v => v.Execute(Now))
                .Returns(new AccessSignatureExpiryInformation(expectedPublicExpiry, expectedPrivateExpiry));

            var publicFilesInformation = new BlobContainerSharedAccessInformation("files", "uri", "sig", expectedPublicExpiry);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(Constants.PublicFileBlobContainerName, expectedPublicExpiry))
                .ReturnsAsync(publicFilesInformation);

            var userContainerName1 = "creatorContainerName1";
            var userContainerName2 = "creatorContainerName2";
            var creatorContainerName1 = "containerName1";
            var creatorContainerName2 = "containerName2";
            var freeAccessContainerName1 = "freeAccessContainerName1";
            var freeAccessContainerName2 = "freeAccessContainerName2";
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(CreatorChannelIds[0])).Returns(userContainerName1);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(CreatorChannelIds[1])).Returns(userContainerName2);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(SubscribedChannelIds[0])).Returns(creatorContainerName1);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(SubscribedChannelIds[1])).Returns(creatorContainerName2);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(FreeAccessChannelIds[0])).Returns(freeAccessContainerName1);
            this.blobLocationGenerator.Setup(v => v.GetBlobContainerName(FreeAccessChannelIds[1])).Returns(freeAccessContainerName2);

            var userInformation1 = new BlobContainerSharedAccessInformation(userContainerName1, "useruri", "usersig", expectedPrivateExpiry);
            var userInformation2 = new BlobContainerSharedAccessInformation(userContainerName2, "useruri", "usersig", expectedPrivateExpiry);
            var creatorInformation1 = new BlobContainerSharedAccessInformation(creatorContainerName1, "useruri1", "usersig1", expectedPrivateExpiry);
            var creatorInformation2 = new BlobContainerSharedAccessInformation(creatorContainerName2, "useruri2", "usersig2", expectedPrivateExpiry);
            var freeAccessInformation1 = new BlobContainerSharedAccessInformation(freeAccessContainerName1, "useruri3", "usersig3", expectedPrivateExpiry);
            var freeAccessInformation2 = new BlobContainerSharedAccessInformation(freeAccessContainerName2, "useruri4", "usersig4", expectedPrivateExpiry);

            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(userContainerName1, expectedPrivateExpiry))
                .ReturnsAsync(userInformation1);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(userContainerName2, expectedPrivateExpiry))
                .ReturnsAsync(userInformation2);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(creatorContainerName1, expectedPrivateExpiry))
                .ReturnsAsync(creatorInformation1);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(creatorContainerName2, expectedPrivateExpiry))
                .ReturnsAsync(creatorInformation2);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(freeAccessContainerName1, expectedPrivateExpiry))
                .ReturnsAsync(freeAccessInformation1);
            this.blobService.Setup(v => v.GetBlobContainerSharedAccessInformationForReadingAsync(freeAccessContainerName2, expectedPrivateExpiry))
                .ReturnsAsync(freeAccessInformation2);

            var result = await this.target.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, CreatorChannelIds, SubscribedChannelIds, FreeAccessChannelIds));

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.PublicSignature);
            Assert.AreEqual(publicFilesInformation, result.PublicSignature);

            Assert.AreEqual(6, result.PrivateSignatures.Count);

            Assert.AreEqual(CreatorChannelIds[0], result.PrivateSignatures[0].ChannelId);
            Assert.AreEqual(userInformation1, result.PrivateSignatures[0].Information);
            Assert.AreEqual(CreatorChannelIds[1], result.PrivateSignatures[1].ChannelId);
            Assert.AreEqual(userInformation2, result.PrivateSignatures[1].Information);
            Assert.AreEqual(SubscribedChannelIds[0], result.PrivateSignatures[2].ChannelId);
            Assert.AreEqual(creatorInformation1, result.PrivateSignatures[2].Information);
            Assert.AreEqual(SubscribedChannelIds[1], result.PrivateSignatures[3].ChannelId);
            Assert.AreEqual(creatorInformation2, result.PrivateSignatures[3].Information);
            Assert.AreEqual(FreeAccessChannelIds[0], result.PrivateSignatures[4].ChannelId);
            Assert.AreEqual(freeAccessInformation1, result.PrivateSignatures[4].Information);
            Assert.AreEqual(FreeAccessChannelIds[1], result.PrivateSignatures[5].ChannelId);
            Assert.AreEqual(freeAccessInformation2, result.PrivateSignatures[5].Information);
        }
    }
}
