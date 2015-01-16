namespace Fifthweek.Api.Azure.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Webjobs.Files.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.Queue;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class QueueServiceTests
    {
        private readonly string containerName = "containerName";
        private readonly string blobName = "blobName";
        private readonly string purpose = "purpose";

        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<ICloudQueueClient> cloudQueueClient;
        private Mock<ICloudQueue> cloudQueue;

        private QueueService queueService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>();
            this.cloudQueueClient = new Mock<ICloudQueueClient>();
            this.cloudQueue = new Mock<ICloudQueue>();

            this.queueService = new QueueService(this.cloudStorageAccount.Object);
        }

        [TestMethod]
        public async Task WhenPostingFileUploadCompletedMessage_ItShouldPostMessageToQueue()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudQueueClient()).Returns(this.cloudQueueClient.Object);
            this.cloudQueueClient.Setup(v => v.GetQueueReference(Constants.FilesQueueName)).Returns(this.cloudQueue.Object);

            CloudQueueMessage message = null;

            this.cloudQueue.Setup(v => v.CreateIfNotExistsAsync()).Returns(Task.FromResult(0)).Verifiable();
            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>()))
                .Callback<CloudQueueMessage>(v => message = v)
                .Returns(Task.FromResult(0));

            await this.queueService.PostFileUploadCompletedMessageToQueueAsync(
                this.containerName,
                this.blobName,
                this.purpose);

            this.cloudQueue.Verify();

            Assert.IsNotNull(message);
            var messageContent = JsonConvert.DeserializeObject<ProcessFileMessage>(message.AsString);
            Assert.AreEqual(this.containerName, messageContent.ContainerName);
            Assert.AreEqual(this.blobName, messageContent.BlobName);
            Assert.AreEqual(this.purpose, messageContent.Purpose);
            Assert.IsFalse(messageContent.Overwrite);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingFileUploadCompletedMessage_ItShouldRequireContainerName()
        {
            await this.queueService.PostFileUploadCompletedMessageToQueueAsync(
                null,
                this.blobName,
                this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingFileUploadCompletedMessage_ItShouldRequireBlobName()
        {
            await this.queueService.PostFileUploadCompletedMessageToQueueAsync(
                this.containerName,
                null,
                this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingFileUploadCompletedMessage_ItShouldRequirePurpose()
        {
            await this.queueService.PostFileUploadCompletedMessageToQueueAsync(
                this.containerName,
                this.blobName,
                null);
        }
    }
}