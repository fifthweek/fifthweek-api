namespace Fifthweek.Api.Azure.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.Queue;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class QueueServiceTests
    {
        private readonly string queueName = "queueName";
        private readonly TestMessage testMessage = new TestMessage { One = 1, Two = "2" };

        private readonly TimeSpan timeToLive = TimeSpan.FromMinutes(100);
        private readonly TimeSpan initialVisibilityDelay = TimeSpan.FromMinutes(200);

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
        public async Task WhenPostingMessage_ItShouldPostMessageToQueue()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudQueueClient()).Returns(this.cloudQueueClient.Object);
            this.cloudQueueClient.Setup(v => v.GetQueueReference(this.queueName)).Returns(this.cloudQueue.Object);

            CloudQueueMessage message = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>(), null, null))
                .Callback<CloudQueueMessage, TimeSpan?, TimeSpan?>((m, x, y) => message = m)
                .Returns(Task.FromResult(0));

            await this.queueService.AddMessageToQueueAsync(
                this.queueName,
                this.testMessage);

            this.cloudQueue.Verify();

            Assert.IsNotNull(message);
            var messageContent = JsonConvert.DeserializeObject<TestMessage>(message.AsString);
            Assert.AreEqual(this.testMessage.One, messageContent.One);
            Assert.AreEqual(this.testMessage.Two, messageContent.Two);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingMessage_ItShouldRequireQueueName()
        {
            await this.queueService.AddMessageToQueueAsync(
                null,
                this.testMessage);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingMessage_ItShouldRequireMessage()
        {
            await this.queueService.AddMessageToQueueAsync<TestMessage>(
                this.queueName,
                null);
        }

        [TestMethod]
        public async Task WhenPostingMessageSpecifyingTimeoutAndVisibility_ItShouldPostMessageToQueue()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudQueueClient()).Returns(this.cloudQueueClient.Object);
            this.cloudQueueClient.Setup(v => v.GetQueueReference(this.queueName)).Returns(this.cloudQueue.Object);

            CloudQueueMessage message = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>(), this.timeToLive, this.initialVisibilityDelay))
                .Callback<CloudQueueMessage, TimeSpan?, TimeSpan?>((m, x, y) => message = m)
                .Returns(Task.FromResult(0));

            await this.queueService.AddMessageToQueueAsync(
                this.queueName,
                this.testMessage,
                this.timeToLive,
                this.initialVisibilityDelay);

            this.cloudQueue.Verify();

            Assert.IsNotNull(message);
            var messageContent = JsonConvert.DeserializeObject<TestMessage>(message.AsString);
            Assert.AreEqual(this.testMessage.One, messageContent.One);
            Assert.AreEqual(this.testMessage.Two, messageContent.Two);
        }

        [TestMethod]
        public async Task WhenPostingMessageSpecifyingNullTimeoutAndVisibility_ItShouldPostMessageToQueue()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudQueueClient()).Returns(this.cloudQueueClient.Object);
            this.cloudQueueClient.Setup(v => v.GetQueueReference(this.queueName)).Returns(this.cloudQueue.Object);

            CloudQueueMessage message = null;

            this.cloudQueue.Setup(v => v.AddMessageAsync(It.IsAny<CloudQueueMessage>(), null, null))
                .Callback<CloudQueueMessage, TimeSpan?, TimeSpan?>((m, x, y) => message = m)
                .Returns(Task.FromResult(0));

            await this.queueService.AddMessageToQueueAsync(
                this.queueName,
                this.testMessage,
                null,
                null);

            this.cloudQueue.Verify();

            Assert.IsNotNull(message);
            var messageContent = JsonConvert.DeserializeObject<TestMessage>(message.AsString);
            Assert.AreEqual(this.testMessage.One, messageContent.One);
            Assert.AreEqual(this.testMessage.Two, messageContent.Two);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingMessageSpecifyingTimeoutAndVisibility_ItShouldRequireQueueName()
        {
            await this.queueService.AddMessageToQueueAsync(
                null,
                this.testMessage,
                this.timeToLive,
                this.initialVisibilityDelay);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostingMessageSpecifyingTimeoutAndVisibility_ItShouldRequireMessage()
        {
            await this.queueService.AddMessageToQueueAsync<TestMessage>(
                this.queueName,
                null,
                this.timeToLive,
                this.initialVisibilityDelay);
        }

        public class TestMessage
        {
            public int One { get; set; }

            public string Two { get; set; }
        }
    }
}