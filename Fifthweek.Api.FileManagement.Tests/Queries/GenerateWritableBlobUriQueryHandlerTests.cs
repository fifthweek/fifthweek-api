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

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Constants;

    [TestClass]
    public class GenerateWritableBlobUriQueryHandlerTests
    {
        [TestMethod]
        public async Task WhenCalled_ItShouldCheckPermissionsAndUpdateTheDatabase()
        {
            var userId = new UserId(Guid.NewGuid());
            var fileId = new FileId(Guid.NewGuid());
            var blobName = "blobName";
            var url = "http://blah.com/blob";

            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(userId, fileId))
                .Returns(Task.FromResult(0));

            this.blobNameCreator.Setup(v => v.CreateFileName(fileId)).Returns(blobName);

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(Constants.FileBlobContainerName, blobName))
                .ReturnsAsync(url);

            var result = await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(userId, fileId));

            this.fileSecurity.Verify();

            Assert.AreEqual(url, result);
        }

        [TestMethod]
        public async Task WhenCalledAndTheUserDoesNotOwnTheFile_ItShouldFail()
        {
            var userId = new UserId(Guid.NewGuid());
            var fileId = new FileId(Guid.NewGuid());
            var blobName = "blobName";
            var url = "http://blah.com/blob";

            this.fileSecurity.Setup(v => v.AssertFileBelongsToUserAsync(userId, fileId))
                .Throws(new UnauthorizedException());

            this.blobNameCreator.Setup(v => v.CreateFileName(fileId))
                .Throws(new Exception("Shouldn't be called"));

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(Constants.FileBlobContainerName, blobName))
                .Throws(new Exception("Shouldn't be called"));

            UnauthorizedException exception = null;
            try
            {
                await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(userId, fileId));
            }
            catch (UnauthorizedException t)
            {
                exception = t;
            }

            Assert.IsNotNull(exception);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.fileSecurity = new Mock<IFileSecurity>();
            this.blobNameCreator = new Mock<IBlobNameCreator>();

            this.handler = new GenerateWritableBlobUriQueryHandler(
                this.blobService.Object,
                this.blobNameCreator.Object,
                this.fileSecurity.Object);
        }

        private Mock<IFileSecurity> fileSecurity;
        private Mock<IBlobService> blobService;
        private Mock<IBlobNameCreator> blobNameCreator;

        private GenerateWritableBlobUriQueryHandler handler;
    }
}
