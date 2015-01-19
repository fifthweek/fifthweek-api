namespace Fifthweek.WebJobs.Files.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Tests.Shared;
    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.Azure.WebJobs;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileProcessorTests
    {
        private const string Purpose = "purpose";

        private const string ContainerName = "containerName";

        private const string BlobName = "blobName";

        private const bool Overwrite = true;

        private readonly ProcessFileMessage message = new ProcessFileMessage(ContainerName, BlobName, Purpose, Overwrite);

        private Mock<IFilePurposeTasks> filePurposeTasks;

        private Mock<ICloudQueueResolver> cloudQueueResolver;

        private Mock<IBinder> binder;
        
        private Mock<ILogger> logger;

        private FileProcessor target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.filePurposeTasks = new Mock<IFilePurposeTasks>();
            this.cloudQueueResolver = new Mock<ICloudQueueResolver>();
            this.binder = new Mock<IBinder>();
            this.logger = new Mock<ILogger>();

            this.target = new FileProcessor(this.filePurposeTasks.Object, this.cloudQueueResolver.Object);
        }

        [TestMethod]
        public async Task WhenATaskIsReturned_ItShouldBeCalled()
        {
            var task = new Mock<IFileTask>();
            var cloudQueue = new Mock<ICloudQueue>();
            var queueName = "queue";

            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(new List<IFileTask> { task.Object });

            this.cloudQueueResolver.Setup(v => v.GetQueueAsync(this.binder.Object, queueName))
                .ReturnsAsync(cloudQueue.Object);

            task.Setup(v => v.QueueName).Returns(queueName);
            task.Setup(v => v.HandleAsync(cloudQueue.Object, this.message))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.ProcessFileAsync(
                this.message,
                this.binder.Object,
                this.logger.Object,
                CancellationToken.None);

            task.Verify();
        }

        [TestMethod]
        public async Task WhenATaskThrowsAnException_ItShouldLogAndThrow()
        {
            var task = new Mock<IFileTask>();
            var cloudQueue = new Mock<ICloudQueue>();
            var queueName = "queue";

            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(new List<IFileTask> { task.Object });

            this.cloudQueueResolver.Setup(v => v.GetQueueAsync(this.binder.Object, queueName))
                .ReturnsAsync(cloudQueue.Object);

            task.Setup(v => v.QueueName).Returns(queueName);
            task.Setup(v => v.HandleAsync(cloudQueue.Object, this.message))
                .Throws(new DivideByZeroException());

            this.logger.Setup(v => v.Error(It.IsAny<string>(), It.IsAny<object[]>()))
                .Verifiable();

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.ProcessFileAsync(
                this.message,
                this.binder.Object,
                this.logger.Object,
                CancellationToken.None));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenCancellationTokenActive_ItShouldLogAndThrowAnException()
        {
            var task = new Mock<IFileTask>();
            var cloudQueue = new Mock<ICloudQueue>();

            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(new List<IFileTask> { task.Object });

            task.Setup(v => v.HandleAsync(cloudQueue.Object, this.message))
                .Throws(new Exception("Should not be called"));

            this.logger.Setup(v => v.Error(It.IsAny<string>(), It.IsAny<object[]>()))
                .Verifiable();

            var cts = new CancellationTokenSource();
            cts.Cancel();
            
            await ExpectedException.AssertExceptionAsync<OperationCanceledException>(
                () => this.target.ProcessFileAsync(
                this.message,
                this.binder.Object,
                this.logger.Object,
                cts.Token));

            this.logger.Verify();
        }

        [TestMethod]
        public async Task WhenNoTasksReturned_ItShouldReturnImmediately()
        {
            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(Enumerable.Empty<IFileTask>()).Verifiable();

            await this.target.ProcessFileAsync(
                this.message,
                this.binder.Object,
                this.logger.Object,
                CancellationToken.None);

            this.filePurposeTasks.Verify();
        }
    }
}