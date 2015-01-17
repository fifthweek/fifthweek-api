using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.FileManagement.Tests.Commands
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Commands;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Tests.Shared;

    using Moq;

    using Constants = Fifthweek.Api.FileManagement.Constants;

    [TestClass]
    public class CompleteFileUploadCommandHandlerTests
    {
        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly string blobName = "blobName";
        private readonly string containerName = "containerName";
        private readonly string purpose = "purpose";

        private readonly string fileExtension = "jpeg";
        private readonly string contentType = "image/jpeg";

        private readonly long blobSize = 1234;

        private Mock<IFileRepository> fileRepository;
        private Mock<IBlobService> blobService;
        private Mock<IQueueService> queueService;
        private Mock<IBlobLocationGenerator> blobNameCreator;

        private CompleteFileUploadCommandHandler handler;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobNameCreator = new Mock<IBlobLocationGenerator>();

            // Give side-effecting components strict mock behaviour.
            this.fileRepository = new Mock<IFileRepository>(MockBehavior.Strict);
            this.blobService = new Mock<IBlobService>(MockBehavior.Strict);
            this.queueService = new Mock<IQueueService>(MockBehavior.Strict);

            this.handler = new CompleteFileUploadCommandHandler(
                this.fileRepository.Object,
                this.blobService.Object,
                this.queueService.Object,
                this.blobNameCreator.Object);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldUpdateStorageAndQueue()
        {
            this.fileRepository.Setup(v => v.GetFileWaitingForUploadAsync(this.fileId))
                .ReturnsAsync(new FileWaitingForUpload(this.fileId, this.userId, "myfile", this.fileExtension, this.purpose));

            this.blobNameCreator.Setup(v => v.GetBlobLocation(this.userId, this.fileId, this.purpose))
                .Returns(new BlobLocation(this.containerName, this.blobName));

            this.blobService.Setup(v => v.GetBlobLengthAndSetContentTypeAsync(this.containerName, this.blobName, this.contentType))
                .ReturnsAsync(this.blobSize).Verifiable();

            this.fileRepository.Setup(v => v.SetFileUploadComplete(this.fileId, this.blobSize))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.queueService.Setup(v => v.PostFileUploadCompletedMessageToQueueAsync(this.containerName, this.blobName, this.purpose))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.handler.HandleAsync(new CompleteFileUploadCommand(this.userId, this.fileId));

            this.fileRepository.Verify();
            this.blobService.Verify();
            this.queueService.Verify();
        }

        [TestMethod]
        public async Task WhenCalledAndFileDoesNotBelogToUser_ItShouldNotUpdateStorageOrQueue()
        {
            this.fileRepository.Setup(v => v.GetFileWaitingForUploadAsync(this.fileId))
                .ReturnsAsync(new FileWaitingForUpload(this.fileId, new UserId(Guid.NewGuid()), "myfile", this.fileExtension, this.purpose));

            Func<Task> badMethodCall = () => this.handler.HandleAsync(new CompleteFileUploadCommand(this.userId, this.fileId));

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }
    }
}
