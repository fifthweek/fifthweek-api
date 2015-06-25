namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.FileTasks;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileProcessorTests
    {
        private const string Purpose = "purpose";
        private const string ContainerName = "containerName";
        private const string BlobName = "blobName";
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private Mock<IFilePurposeTasks> filePurposeTasks;
        private Mock<IQueueService> queueService;
        private FileProcessor target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.filePurposeTasks = new Mock<IFilePurposeTasks>();
            this.queueService = new Mock<IQueueService>();

            this.target = new FileProcessor(this.filePurposeTasks.Object, this.queueService.Object);
        }

        [TestMethod]
        public async Task WhenATaskIsReturned_ItShouldBeCalled()
        {
            var task = new Mock<IFileTask>();

            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(new List<IFileTask> { task.Object });

            task.Setup(v => v.HandleAsync(this.queueService.Object, FileId, ContainerName, BlobName, Purpose))
                .Returns(Task.FromResult(0)).Verifiable();

            await this.target.ProcessFileAsync(
                FileId, 
                ContainerName,
                BlobName,
                Purpose);

            task.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenATaskThrowsAnException_ItPassTheExceptionThrough()
        {
            var task = new Mock<IFileTask>();

            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(new List<IFileTask> { task.Object });

            task.Setup(v => v.HandleAsync(this.queueService.Object, FileId, ContainerName, BlobName, Purpose))
                .Throws(new DivideByZeroException());

            await this.target.ProcessFileAsync(
                FileId,
                ContainerName,
                BlobName,
                Purpose);
        }

        [TestMethod]
        public async Task WhenNoTasksReturned_ItShouldReturnImmediately()
        {
            this.filePurposeTasks.Setup(v => v.GetTasks(Purpose))
                .Returns(Enumerable.Empty<IFileTask>()).Verifiable();

            await this.target.ProcessFileAsync(
                FileId,
                ContainerName,
                BlobName,
                Purpose);

            this.filePurposeTasks.Verify();
        }
    }
}