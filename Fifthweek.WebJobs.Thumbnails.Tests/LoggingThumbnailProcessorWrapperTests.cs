namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;
    using Fifthweek.WebJobs.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class LoggingThumbnailProcessorWrapperTests
    {
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly string ContainerName = "containerName";
        private static readonly string InputBlobName = "inputBlob";
        private static readonly string OutputBlobName = "outputBlob";

        private static readonly CreateThumbnailsMessage Message = new CreateThumbnailsMessage(
            FileId,
            ContainerName,
            InputBlobName,
            new List<ThumbnailDefinition> 
            {
                new ThumbnailDefinition(
                    OutputBlobName,
                    200,
                    100,
                    ResizeBehaviour.MaintainAspectRatio,
                    ProcessingBehaviour.Darken,
                    null)
            },
            false);

        private Mock<IThumbnailProcessor> thumbnailProcessor;
        private Mock<ISetFileProcessingCompleteDbStatement> setFileProcessingComplete;
        private Mock<ILogger> logger;
        private Mock<ICloudStorageAccount> storageAccount;
        private Mock<ICloudBlockBlob> input;
        private LoggingThumbnailProcessorWrapper target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.thumbnailProcessor = new Mock<IThumbnailProcessor>();
            this.setFileProcessingComplete = new Mock<ISetFileProcessingCompleteDbStatement>(MockBehavior.Strict);
            this.logger = new Mock<ILogger>(MockBehavior.Strict);
            this.storageAccount = new Mock<ICloudStorageAccount>();
            this.input = new Mock<ICloudBlockBlob>();
            this.target = new LoggingThumbnailProcessorWrapper(this.thumbnailProcessor.Object, this.setFileProcessingComplete.Object);
        }

        [TestMethod]
        public async Task WhenProcessingAMessage_ItShouldLogStatsToTheDatabase()
        {
            this.thumbnailProcessor.Setup(v => v.CreateThumbnailSetAsync(Message, this.input.Object, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .ReturnsAsync(new CreateThumbnailSetResult(800, 600));

            this.setFileProcessingComplete.Setup(v => v.ExecuteAsync(Message.FileId, 9, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 800, 600))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.CreateThumbnailSetAsync(
                Message,
                this.input.Object,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None);

            this.setFileProcessingComplete.Verify();
        }

        [TestMethod]
        public async Task WhenProcessingAMessageAndNoResultIsReturned_ItShouldNotLogStatsToTheDatabase()
        {
            this.thumbnailProcessor.Setup(v => v.CreateThumbnailSetAsync(Message, this.input.Object, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .ReturnsAsync(null);

            await this.target.CreateThumbnailSetAsync(
                Message,
                this.input.Object,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None);

            // Strict mocking will ensure the service wasn't called.
        }

        [TestMethod]
        public async Task WhenProcessingAMessageAndAnErrorOccursProcessingThumbnails_ItShouldLogTheError()
        {
            this.thumbnailProcessor.Setup(v => v.CreateThumbnailSetAsync(Message, this.input.Object, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .ThrowsAsync(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(() => this.target.CreateThumbnailSetAsync(
                Message,
                this.input.Object,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenProcessingAMessageAndAnErrorOccursLoggingToDatabase_ItShouldLogTheError()
        {
            this.thumbnailProcessor.Setup(v => v.CreateThumbnailSetAsync(Message, this.input.Object, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .ReturnsAsync(new CreateThumbnailSetResult(800, 600));

            this.setFileProcessingComplete.Setup(v => v.ExecuteAsync(Message.FileId, 9, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 800, 600))
                .Throws(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(() => this.target.CreateThumbnailSetAsync(
                Message,
                this.input.Object,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenProcessingAPoisonMessage_ItShouldLogStatsToTheDatabase()
        {
            this.logger.Setup(v => v.Warn("Failed to resize image from blob {0}/{1}", Message.ContainerName, Message.InputBlobName)).Verifiable();
            this.thumbnailProcessor.Setup(v => v.CreatePoisonThumbnailSetAsync(Message, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .Returns(Task.FromResult(0)).Verifiable();
            this.setFileProcessingComplete.Setup(v => v.ExecuteAsync(Message.FileId, -9, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1, 1))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.CreatePoisonThumbnailSetAsync(
                Message,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None);

            this.logger.Verify();
            this.thumbnailProcessor.Verify();
            this.setFileProcessingComplete.Verify();
        }

        [TestMethod]
        public async Task WhenProcessingAPoisonMessageAndAnErrorOccursProcessingThumbnails_ItShouldLogTheError()
        {
            this.logger.Setup(v => v.Warn("Failed to resize image from blob {0}/{1}", Message.ContainerName, Message.InputBlobName));
            this.thumbnailProcessor.Setup(v => v.CreatePoisonThumbnailSetAsync(Message, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .Throws(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(() => this.target.CreatePoisonThumbnailSetAsync(
                Message,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenProcessingAPoisonMessageAndAnErrorOccursLoggingToDatabase_ItShouldLogTheError()
        {
            this.logger.Setup(v => v.Warn("Failed to resize image from blob {0}/{1}", Message.ContainerName, Message.InputBlobName));
            this.thumbnailProcessor.Setup(v => v.CreatePoisonThumbnailSetAsync(Message, this.storageAccount.Object, this.logger.Object, CancellationToken.None))
                .Returns(Task.FromResult(0)).Verifiable();
            this.setFileProcessingComplete.Setup(v => v.ExecuteAsync(Message.FileId, -9, It.IsAny<DateTime>(), It.IsAny<DateTime>(), 1, 1))
                .Throws(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<DivideByZeroException>())).Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(() => this.target.CreatePoisonThumbnailSetAsync(
                Message,
                this.storageAccount.Object,
                this.logger.Object,
                9,
                CancellationToken.None));

            this.logger.Verify();
        }
    }
}