namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Files.Shared;
    using Fifthweek.WebJobs.Thumbnails.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.Queue;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class ThumbnailFileTaskTests
    {
        private readonly ProcessFileMessage message = new ProcessFileMessage(
            "containerName",
            "blobName",
            "purpose",
            true);

        private Mock<ICloudQueue> cloudQueue;

        [TestInitialize]
        public void TestInitialize()
        {
            this.cloudQueue = new Mock<ICloudQueue>(MockBehavior.Strict);
        }

        [TestMethod]
        public async Task WhenMaintainingAspectRatio_TestHandleAsync()
        {
            CloudQueueMessage output = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>()))
                .Callback<CloudQueueMessage>(v => output = v)
                .Returns(Task.FromResult(0));

            var target = new ThumbnailFileTask(1000, 1000, ResizeBehaviour.MaintainAspectRatio);
            await target.HandleAsync(this.cloudQueue.Object, this.message);

            var outputMessage = JsonConvert.DeserializeObject<CreateThumbnailMessage>(output.AsString);

            Assert.AreEqual(this.message.ContainerName, outputMessage.ContainerName);
            Assert.AreEqual(this.message.BlobName, outputMessage.InputBlobName);
            Assert.AreEqual(string.Format("{0}-{1}-{2}", this.message.BlobName, target.Width, target.Height), outputMessage.OutputBlobName);
            Assert.AreEqual(target.Width, outputMessage.Width);
            Assert.AreEqual(target.Height, outputMessage.Height);
            Assert.AreEqual(target.ResizeBehaviour, outputMessage.ResizeBehaviour);
            Assert.AreEqual(this.message.Overwrite, outputMessage.Overwrite);
        }

        [TestMethod]
        public async Task WhenCropping_TestHandleAsync()
        {
            CloudQueueMessage output = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>()))
                .Callback<CloudQueueMessage>(v => output = v)
                .Returns(Task.FromResult(0));

            var target = new ThumbnailFileTask(1000, 1000, ResizeBehaviour.CropToAspectRatio);
            await target.HandleAsync(this.cloudQueue.Object, this.message);

            var outputMessage = JsonConvert.DeserializeObject<CreateThumbnailMessage>(output.AsString);

            Assert.AreEqual(this.message.ContainerName, outputMessage.ContainerName);
            Assert.AreEqual(this.message.BlobName, outputMessage.InputBlobName);
            Assert.AreEqual(string.Format("{0}-{1}-{2}-crop", this.message.BlobName, target.Width, target.Height), outputMessage.OutputBlobName);
            Assert.AreEqual(target.Width, outputMessage.Width);
            Assert.AreEqual(target.Height, outputMessage.Height);
            Assert.AreEqual(target.ResizeBehaviour, outputMessage.ResizeBehaviour);
            Assert.AreEqual(this.message.Overwrite, outputMessage.Overwrite);
        }
    }
}