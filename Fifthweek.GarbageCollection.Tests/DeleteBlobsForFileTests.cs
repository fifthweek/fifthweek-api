namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Azure;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteBlobsForFileTests
    {
        private static readonly BlobLocation Location = new BlobLocation("containerName", "blobName");
        private static readonly OrphanedFileData File = new OrphanedFileData(FileId.Random(), ChannelId.Random(), FilePurposes.PostImage);

        private Mock<IBlobLocationGenerator> blobLocationGenerator;
        private Mock<ICloudStorageAccount> cloudStorageAccount;

        private DeleteBlobsForFile target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobLocationGenerator = new Mock<IBlobLocationGenerator>(MockBehavior.Strict);
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);

            this.target = new DeleteBlobsForFile(
                this.blobLocationGenerator.Object,
                this.cloudStorageAccount.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenFileIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldDeleteBlobs()
        {
            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(File.ChannelId, File.FileId, File.Purpose))
                .Returns(Location);

            var blobClient = new Mock<ICloudBlobClient>(MockBehavior.Strict);
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient())
                .Returns(blobClient.Object);

            var container = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            blobClient.Setup(v => v.GetContainerReference(Location.ContainerName))
                .Returns(container.Object);

            var parentBlob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            container.Setup(v => v.GetBlockBlobReference(Location.BlobName))
                .Returns(parentBlob.Object);

            parentBlob.Setup(v => v.ExistsAsync()).ReturnsAsync(true);
            parentBlob.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();

            var directory = new Mock<ICloudBlobDirectory>(MockBehavior.Strict);
            container.Setup(v => v.GetDirectoryReference(Location.BlobName))
                .Returns(directory.Object);

            var childBlob1 = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            var childBlob2 = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            var childBlobs = new List<ICloudBlockBlob>
            {
                childBlob1.Object,
                childBlob2.Object,
            };

            directory.Setup(v => v.ListCloudBlockBlobsAsync(true)).ReturnsAsync(childBlobs);

            childBlob1.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();
            childBlob2.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(File);

            parentBlob.Verify();
            childBlob1.Verify();
            childBlob2.Verify();
        }

        [TestMethod]
        public async Task WhenParentBlobDoesNotExist_ItShouldDeletChildBlobsOnly()
        {
            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(File.ChannelId, File.FileId, File.Purpose))
                .Returns(Location);

            var blobClient = new Mock<ICloudBlobClient>(MockBehavior.Strict);
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient())
                .Returns(blobClient.Object);

            var container = new Mock<ICloudBlobContainer>(MockBehavior.Strict);
            blobClient.Setup(v => v.GetContainerReference(Location.ContainerName))
                .Returns(container.Object);

            var parentBlob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            container.Setup(v => v.GetBlockBlobReference(Location.BlobName))
                .Returns(parentBlob.Object);

            parentBlob.Setup(v => v.ExistsAsync()).ReturnsAsync(false);

            var directory = new Mock<ICloudBlobDirectory>(MockBehavior.Strict);
            container.Setup(v => v.GetDirectoryReference(Location.BlobName))
                .Returns(directory.Object);

            var childBlob1 = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            var childBlob2 = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            var childBlobs = new List<ICloudBlockBlob>
            {
                childBlob1.Object,
                childBlob2.Object,
            };

            directory.Setup(v => v.ListCloudBlockBlobsAsync(true)).ReturnsAsync(childBlobs);

            childBlob1.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();
            childBlob2.Setup(v => v.DeleteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(File);

            childBlob1.Verify();
            childBlob2.Verify();
        }
    }
}