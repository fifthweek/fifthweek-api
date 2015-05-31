namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileInformationAggregatorTests
    {
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly string FilePurpose = "filePurpose";

        private FileInformationAggregator target;
        private Mock<IBlobLocationGenerator> blobLocationGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobLocationGenerator = new Mock<IBlobLocationGenerator>();

            this.target = new FileInformationAggregator(this.blobLocationGenerator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullFileId_ItShouldThrowAnException()
        {
            await this.target.GetFileInformationAsync(ChannelId, null, FilePurpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullPurpose_ItShouldThrowAnException()
        {
            await this.target.GetFileInformationAsync(ChannelId, FileId, null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldReturnTheFileInformation()
        {
            const string ContainerName = "containerName";
            const string BlobName = "blobName";

            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(ChannelId, FileId, FilePurpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            var result = await this.target.GetFileInformationAsync(ChannelId, FileId, FilePurpose);

            Assert.IsNotNull(result);
            Assert.AreEqual(FileId, result.FileId);
            Assert.AreEqual(ContainerName, result.ContainerName);
        }

        [TestMethod]
        public async Task WhenCalledWithNullUserId_ItShouldReturnTheFileInformation()
        {
            const string ContainerName = "containerName";
            const string BlobName = "blobName";

            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(null, FileId, FilePurpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            var result = await this.target.GetFileInformationAsync(null, FileId, FilePurpose);

            Assert.IsNotNull(result);
            Assert.AreEqual(FileId, result.FileId);
            Assert.AreEqual(ContainerName, result.ContainerName);
        }
    }
}