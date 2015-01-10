using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Queries
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;

    using Moq;

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

            this.fileRepository.Setup(v => v.AssertFileBelongsToUserAsync(userId, fileId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.blobNameCreator.Setup(v => v.CreateFileName(fileId)).Returns(blobName);

            this.blobService.Setup(v => v.GetBlobSasUriForWritingAsync(Constants.FileBlobContainerName, blobName))
                .ReturnsAsync(url);

            var result = await this.handler.HandleAsync(new GenerateWritableBlobUriQuery(userId, fileId));

            this.fileRepository.Verify();

            Assert.AreEqual(url, result);
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.fileRepository = new Mock<IFileRepository>();
            this.blobNameCreator = new Mock<IBlobNameCreator>();

            this.handler = new GenerateWritableBlobUriQueryHandler(
                this.blobService.Object,
                this.blobNameCreator.Object,
                this.fileRepository.Object);
        }

        private Mock<IFileRepository> fileRepository;
        private Mock<IBlobService> blobService;
        private Mock<IBlobNameCreator> blobNameCreator;

        private GenerateWritableBlobUriQueryHandler handler;
    }
}
