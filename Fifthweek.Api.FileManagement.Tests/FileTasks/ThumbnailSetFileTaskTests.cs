namespace Fifthweek.Api.FileManagement.Tests.FileTasks
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.FileTasks;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.Queue;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class ThumbnailSetFileTaskTests
    {
        private const string ContainerName = "containerName";
        private const string BlobName = "blobName";
        private const string FilePurpose = "file-purpose";

        private Mock<IQueueService> queueService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.queueService = new Mock<IQueueService>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task WhenMaintainingAspectRatio_TestHandleAsync()
        {
            CreateThumbnailsMessage outputMessage = null;

            this.queueService.Setup(v => v.AddMessageToQueueAsync(Constants.ThumbnailsQueueName, It.IsAny<CreateThumbnailsMessage>()))
                .Callback<string, CreateThumbnailsMessage>((s, v) => outputMessage = v)
                .Returns(Task.FromResult(0));

            var target = new CreateThumbnailsTask(new Thumbnail(1000, 1000, "alias", ResizeBehaviour.MaintainAspectRatio));
            await target.HandleAsync(this.queueService.Object, ContainerName, BlobName, FilePurpose);

            Assert.AreEqual(ContainerName, outputMessage.ContainerName);
            Assert.AreEqual(BlobName, outputMessage.InputBlobName);
            Assert.AreEqual(string.Format("{0}/{1}", BlobName, target.Items[0].Alias), outputMessage.Items[0].OutputBlobName);
            Assert.AreEqual(target.Items[0].Width, outputMessage.Items[0].Width);
            Assert.AreEqual(target.Items[0].Height, outputMessage.Items[0].Height);
            Assert.AreEqual(target.Items[0].ResizeBehaviour, outputMessage.Items[0].ResizeBehaviour);
            Assert.AreEqual(false, outputMessage.Overwrite);
        }

        [TestMethod]
        public async Task WhenCropping_TestHandleAsync()
        {
            CreateThumbnailsMessage outputMessage = null;

            this.queueService.Setup(v => v.AddMessageToQueueAsync(Constants.ThumbnailsQueueName, It.IsAny<CreateThumbnailsMessage>()))
                .Callback<string, CreateThumbnailsMessage>((s, v) => outputMessage = v)
                .Returns(Task.FromResult(0));

            var target = new CreateThumbnailsTask(new Thumbnail(1000, 1000, "alias", ResizeBehaviour.CropToAspectRatio));
            await target.HandleAsync(this.queueService.Object, ContainerName, BlobName, FilePurpose);

            Assert.AreEqual(ContainerName, outputMessage.ContainerName);
            Assert.AreEqual(BlobName, outputMessage.InputBlobName);
            Assert.AreEqual(string.Format("{0}/{1}", BlobName, target.Items[0].Alias), outputMessage.Items[0].OutputBlobName);
            Assert.AreEqual(target.Items[0].Width, outputMessage.Items[0].Width);
            Assert.AreEqual(target.Items[0].Height, outputMessage.Items[0].Height);
            Assert.AreEqual(target.Items[0].ResizeBehaviour, outputMessage.Items[0].ResizeBehaviour);
            Assert.AreEqual(false, outputMessage.Overwrite);
        }
    }
}