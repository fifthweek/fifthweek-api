namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GenerateWritableBlobUriQueryHandlerTests
    {
        private const string BlobName = "blobName";
        private const string ContainerName = "containerName";
        private const string Url = "http://blah.com/blob";
        private const string Purpose = "purpose";

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IFileSecurity> fileSecurity;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobNameCreator;

        private GenerateWritableBlobUriQueryHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();

            this.handler = new GenerateWritableBlobUriQueryHandler(
                this.blobService.Object,
                this.blobNameCreator.Object,
                this.fileSecurity.Object);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCheckPermissionsAndUpdateTheDatabase()
        {
            this.fileSecurity.Setup(v => v.AssertUsageAllowedAsync(UserId, FileId))
                .Returns(Task.FromResult(0));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(UserId, FileId, Purpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(ContainerName, BlobName))
                .ReturnsAsync(Url);

            var result = await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(UserId, FileId, Purpose));

            Assert.AreEqual(Url, result);
        }

        [TestMethod]
        public async Task WhenCalledAndTheUserDoesNotOwnTheFile_ItShouldFail()
        {
            this.fileSecurity.Setup(v => v.AssertUsageAllowedAsync(UserId, FileId))
                .Throws(new UnauthorizedException());

            this.blobNameCreator.Setup(v => v.GetBlobLocation(UserId, FileId, Purpose))
                .Throws(new Exception("Shouldn't be called"));

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(ContainerName, BlobName))
                .Throws(new Exception("Shouldn't be called"));

            await ExpectedException<UnauthorizedException>.AssertAsync(
                () => this.handler.HandleAsync(new GenerateWritableBlobUriQuery(UserId, FileId, Purpose)));
        }
    }
}
