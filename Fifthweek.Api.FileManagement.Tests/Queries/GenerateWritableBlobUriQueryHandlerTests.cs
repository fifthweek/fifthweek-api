namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    

    [TestClass]
    public class GenerateWritableBlobUriQueryHandlerTests
    {
        private const string BlobName = "blobName";
        private const string ContainerName = "containerName";
        private const string Url = "http://blah.com/blob";
        private const string Signature = "?sig";
        private const string Purpose = "purpose";

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private static readonly BlobSharedAccessInformation SharedAccessInformation =
            new BlobSharedAccessInformation(ContainerName, BlobName, Url, Signature, DateTime.UtcNow);


        private Mock<IFileSecurity> fileSecurity;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobNameCreator;
        private Mock<IRequesterSecurity> requesterSecurity;

        private GenerateWritableBlobUriQueryHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fileSecurity = new Mock<IFileSecurity>();
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give side-effecting components strict mock behaviour.
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);

            this.handler = new GenerateWritableBlobUriQueryHandler(
                this.blobService.Object,
                this.blobNameCreator.Object,
                this.fileSecurity.Object,
                this.requesterSecurity.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowException()
        {
            await this.handler.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(Requester.Unauthenticated, FileId, Purpose));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalledAndTheUserDoesNotOwnTheFile_ItShouldFail()
        {
            this.fileSecurity.Setup(v => v.AssertUsageAllowedAsync(UserId, FileId))
                .Throws(new UnauthorizedException());

            await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(Requester, FileId, Purpose));
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCheckPermissionsAndUpdateTheDatabase()
        {
            this.fileSecurity.Setup(v => v.AssertUsageAllowedAsync(UserId, FileId))
                .Returns(Task.FromResult(0));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(UserId, FileId, Purpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobSharedAccessInformationForWritingAsync(ContainerName, BlobName))
                .ReturnsAsync(SharedAccessInformation);

            var result = await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(Requester, FileId, Purpose));

            Assert.AreEqual(SharedAccessInformation, result);
        }
    }
}
