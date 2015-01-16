using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Tests.Shared;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Constants;

    [TestClass]
    public class GenerateWritableBlobUriQueryHandlerTests
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly FileId fileId = new FileId(Guid.NewGuid());
        private readonly string blobName = "blobName";
        private readonly string containerName = "containerName";
        private readonly string url = "http://blah.com/blob";
        private readonly string purpose = "purpose";

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
            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(this.userId, this.fileId))
                .Returns(Task.FromResult(0));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(this.userId, this.fileId, this.purpose))
                .Returns(new BlobLocation(this.containerName, this.blobName));

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(this.containerName, this.blobName))
                .ReturnsAsync(this.url);

            var result = await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(this.userId, this.fileId, this.purpose));

            Assert.AreEqual(this.url, result);
        }

        [TestMethod]
        public async Task WhenCalledAndTheUserDoesNotOwnTheFile_ItShouldFail()
        {
            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(this.userId, this.fileId))
                .Throws(new UnauthorizedException());

            this.blobNameCreator.Setup(v => v.GetBlobLocation(this.userId, this.fileId, this.purpose))
                .Throws(new Exception("Shouldn't be called"));

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(this.containerName, this.blobName))
                .Throws(new Exception("Shouldn't be called"));

            await ExpectedException<UnauthorizedException>.AssertAsync(
                () => this.handler.HandleAsync(new GenerateWritableBlobUriQuery(this.userId, this.fileId, this.purpose)));
        }
    }
}
