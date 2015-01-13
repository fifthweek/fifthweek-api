using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.Identity.Membership;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Constants;

    [TestClass]
    public class CompleteFileUploadCommandHandlerTests
    {
        [TestMethod]
        public async Task WhenCalled_ItShouldCheckPermissionsAndUpdateTheDatabase()
        {
            var userId = new UserId(Guid.NewGuid());
            var fileId = new FileId(Guid.NewGuid());
            var blobName = "blobName";

            this.fileRepository.Setup(v => v.AssertFileBelongsToUserAsync(userId, fileId))
                .Returns(Task.FromResult(0));

            this.blobNameCreator.Setup(v => v.CreateFileName(fileId)).Returns(blobName);

            var blobProperties = new Mock<IBlobProperties>();
            blobProperties.Setup(v => v.Length).Returns(1024);

            this.blobService.Setup(v => v.GetBlobPropertiesAsync(Constants.FileBlobContainerName, blobName))
                .ReturnsAsync(blobProperties.Object);

            this.fileRepository.Setup(v => v.SetFileUploadComplete(fileId, 1024))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.handler.HandleAsync(new CompleteFileUploadCommand(userId, fileId));

            this.fileRepository.Verify();
        }

        [TestMethod]
        public async Task WhenCalledAndFileDoesNotBelogToUser_ItShouldNotUpdateTheDatabase()
        {
            var userId = new UserId(Guid.NewGuid());
            var fileId = new FileId(Guid.NewGuid());
            var blobName = "blobName";

            this.fileRepository.Setup(v => v.AssertFileBelongsToUserAsync(userId, fileId))
                .Throws(new UnauthorizedException());

            this.blobNameCreator.Setup(v => v.CreateFileName(fileId)).Returns(blobName);

            var blobProperties = new Mock<IBlobProperties>();
            blobProperties.Setup(v => v.Length).Returns(1024);

            this.blobService.Setup(v => v.GetBlobPropertiesAsync(Constants.FileBlobContainerName, blobName))
                .ReturnsAsync(blobProperties.Object);

            this.fileRepository.Setup(v => v.SetFileUploadComplete(fileId, 1024))
                .Throws(new Exception("This should not be called"));

            Exception exception = null;
            try
            {
                await this.handler.HandleAsync(new CompleteFileUploadCommand(userId, fileId));
            }
            catch (Exception t)
            {
                exception = t;
            }

            Assert.IsInstanceOfType(exception, typeof(UnauthorizedException));
            this.fileRepository.Verify();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.fileRepository = new Mock<IFileRepository>();
            this.blobNameCreator = new Mock<IBlobNameCreator>();

            this.handler = new CompleteFileUploadCommandHandler(
                this.fileRepository.Object,
                this.blobService.Object, 
                this.blobNameCreator.Object);
        }

        private Mock<IFileRepository> fileRepository;
        private Mock<IBlobService> blobService;
        private Mock<IBlobNameCreator> blobNameCreator;

        private CompleteFileUploadCommandHandler handler;
    }
}
