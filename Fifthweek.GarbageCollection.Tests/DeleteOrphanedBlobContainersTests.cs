namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Azure;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage.Blob;

    using Moq;

    [TestClass]
    public class DeleteOrphanedBlobContainersTests
    {
        private static readonly DateTime EndTimeExclusive = DateTime.UtcNow;

        private Mock<IGetAllChannelIdsDbStatement> getAllChannelIds;
        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<IKeepAliveHandler> keepAliveHandler;

        private DeleteOrphanedBlobContainers target;

        [TestInitialize]
        public void Initialize()
        {
            this.getAllChannelIds = new Mock<IGetAllChannelIdsDbStatement>(MockBehavior.Strict);
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);
            this.keepAliveHandler = new Mock<IKeepAliveHandler>(MockBehavior.Strict);

            this.target = new DeleteOrphanedBlobContainers(
                this.getAllChannelIds.Object,
                this.cloudStorageAccount.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenLoggerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, this.keepAliveHandler.Object, EndTimeExclusive, CancellationToken.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenKeepAliveHandlerIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(new Mock<ILogger>().Object, null, EndTimeExclusive, CancellationToken.None);
        }

        [TestMethod]
        public async Task ItShouldDeleteAllOrphanedBlobContainers()
        {
            var channelIds = Enumerable.Repeat(0, 5).Select(v => ChannelId.Random()).ToList();

            this.getAllChannelIds.Setup(v => v.ExecuteAsync()).ReturnsAsync(channelIds);

            var blobClient = new Mock<ICloudBlobClient>(MockBehavior.Strict);
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient())
                .Returns(blobClient.Object);

            var segment1 = new Mock<IContainerResultSegment>(MockBehavior.Strict);
            var continuationToken = new BlobContinuationToken();
            segment1.Setup(v => v.ContinuationToken).Returns(continuationToken);

            var segment2 = new Mock<IContainerResultSegment>(MockBehavior.Strict);
            segment2.Setup(v => v.ContinuationToken).Returns((BlobContinuationToken)null);

            var container1 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container2 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container3 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container4 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container5 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);

            segment1.Setup(v => v.Results).Returns(
                new List<ICloudBlobContainer> 
                { 
                    container1.Object, 
                    container2.Object,
                    container3.Object,
                });

            segment2.Setup(v => v.Results).Returns(
                new List<ICloudBlobContainer> 
                { 
                    container4.Object, 
                    container5.Object,
                });

            // Fails delete condition.
            container1.Setup(v => v.Name).Returns("NotAGuid");

            // Fails delete condition.
            container2.Setup(v => v.Name).Returns(channelIds[2].Value.ToString("N"));
            
            container3.Setup(v => v.Name).Returns(Guid.NewGuid().ToString("N"));
            container4.Setup(v => v.Name).Returns(Guid.NewGuid().ToString("N"));
            container5.Setup(v => v.Name).Returns(Guid.NewGuid().ToString("N"));

            // Fails delete condition.
            container3.Setup(v => v.Properties).Returns(this.GetBlobContainerProperties(null));
            
            // Fails delete condition.
            container4.Setup(v => v.Properties).Returns(this.GetBlobContainerProperties(EndTimeExclusive));

            container5.Setup(v => v.Properties).Returns(this.GetBlobContainerProperties(EndTimeExclusive.AddTicks(-1)));

            blobClient.Setup(v => v.ListContainersSegmentedAsync(null)).ReturnsAsync(segment1.Object);
            blobClient.Setup(v => v.ListContainersSegmentedAsync(continuationToken)).ReturnsAsync(segment2.Object);

            container5.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(new Mock<ILogger>().Object, this.keepAliveHandler.Object, EndTimeExclusive, CancellationToken.None);

            container5.Verify();
            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(5));
        }

        [TestMethod]
        public async Task ItShouldStopProcessingWhenCancelled()
        {
            var channelIds = Enumerable.Repeat(0, 5).Select(v => ChannelId.Random()).ToList();

            this.getAllChannelIds.Setup(v => v.ExecuteAsync()).ReturnsAsync(channelIds);

            var blobClient = new Mock<ICloudBlobClient>(MockBehavior.Strict);
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient())
                .Returns(blobClient.Object);

            var segment1 = new Mock<IContainerResultSegment>(MockBehavior.Strict);
            var continuationToken = new BlobContinuationToken();
            segment1.Setup(v => v.ContinuationToken).Returns(continuationToken);

            var container1 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container2 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            var container3 = new Mock<ICloudBlobContainer>(MockBehavior.Strict);

            segment1.Setup(v => v.Results).Returns(
                new List<ICloudBlobContainer> 
                { 
                    container1.Object, 
                    container2.Object,
                    container3.Object,
                });

            var cts = new CancellationTokenSource();

            // Fails delete condition.
            container1.Setup(v => v.Name).Returns("NotAGuid").Callback(cts.Cancel);

            blobClient.Setup(v => v.ListContainersSegmentedAsync(null)).ReturnsAsync(segment1.Object);

            this.keepAliveHandler.Setup(v => v.KeepAliveAsync()).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(new Mock<ILogger>().Object, this.keepAliveHandler.Object, EndTimeExclusive, cts.Token);
            this.keepAliveHandler.Verify(v => v.KeepAliveAsync(), Times.Exactly(1));
        }

        private BlobContainerProperties GetBlobContainerProperties(DateTime? lastModified)
        {
            var properties = new BlobContainerProperties();

            if (lastModified != null)
            {
                var lastModifiedProperty = typeof(BlobContainerProperties).GetProperty(
                    "LastModified",
                    BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                lastModifiedProperty.SetValue(properties, new DateTimeOffset(lastModified.Value));
            }

            return properties;
        }
    }
}