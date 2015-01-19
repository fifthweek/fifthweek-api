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

        private ThumbnailFileTask target;

        private Mock<ICloudQueue> cloudQueue;

        [TestInitialize]
        public void TestInitialize()
        {
            target = new ThumbnailFileTask(1000, 1000, ResizeBehaviour.MaintainAspectRatio);
            this.cloudQueue = new Mock<ICloudQueue>(MockBehavior.Strict);
        }
        
        [TestMethod]
        public async Task WhenHandleAsyncCalled()
        {
            CloudQueueMessage output = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>()))
                .Callback<CloudQueueMessage>(v => output = v)
                .Returns(Task.FromResult(0));

            await this.target.HandleAsync(this.cloudQueue.Object, this.message);

            var outputMessage = JsonConvert.DeserializeObject<CreateThumbnailMessage>(output.AsString);

            Assert.AreEqual(this.message.ContainerName, outputMessage.ContainerName);
            Assert.AreEqual(this.message.BlobName, outputMessage.InputBlobName);
            Assert.AreEqual(string.Format("{0}-{1}-{2}", this.message.BlobName, this.target.Width, this.target.Height), outputMessage.OutputBlobName);
            Assert.AreEqual(this.target.Width, outputMessage.Width);
            Assert.AreEqual(this.target.Height, outputMessage.Height);
            Assert.AreEqual(this.target.ResizeBehaviour, outputMessage.ResizeBehaviour);
            Assert.AreEqual(this.message.Overwrite, outputMessage.Overwrite);
        }
    }
}