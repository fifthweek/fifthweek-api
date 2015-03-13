namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileInformationAggregatorTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly string FilePurpose = "filePurpose";

        private FileInformationAggregator target;
        private Mock<IBlobService> blobService;
        private Mock<IBlobLocationGenerator> blobLocationGenerator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.blobService = new Mock<IBlobService>();
            this.blobLocationGenerator = new Mock<IBlobLocationGenerator>();

            this.target = new FileInformationAggregator(this.blobService.Object, this.blobLocationGenerator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullFileId_ItShouldThrowAnException()
        {
            await this.target.GetFileInformationAsync(UserId, null, FilePurpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalledWithNullPurpose_ItShouldThrowAnException()
        {
            await this.target.GetFileInformationAsync(UserId, FileId, null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldReturnTheFileInformation()
        {
            const string ContainerName = "containerName";
            const string BlobName = "blobName";
            const string BlobUri = "uri";

            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(UserId, FileId, FilePurpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobInformationAsync(ContainerName, BlobName))
                .ReturnsAsync(new BlobInformation(ContainerName, BlobName, BlobUri));

            var result = await this.target.GetFileInformationAsync(UserId, FileId, FilePurpose);

            Assert.IsNotNull(result);
            Assert.AreEqual(FileId, result.FileId);
            Assert.AreEqual(ContainerName, result.ContainerName);
            Assert.AreEqual(BlobUri, result.Uri);
        }

        [TestMethod]
        public async Task WhenCalledWithNullUserId_ItShouldReturnTheFileInformation()
        {
            const string ContainerName = "containerName";
            const string BlobName = "blobName";
            const string BlobUri = "uri";

            this.blobLocationGenerator.Setup(v => v.GetBlobLocation(null, FileId, FilePurpose))
                .Returns(new BlobLocation(ContainerName, BlobName));

            this.blobService.Setup(v => v.GetBlobInformationAsync(ContainerName, BlobName))
                .ReturnsAsync(new BlobInformation(ContainerName, BlobName, BlobUri));

            var result = await this.target.GetFileInformationAsync(null, FileId, FilePurpose);

            Assert.IsNotNull(result);
            Assert.AreEqual(FileId, result.FileId);
            Assert.AreEqual(ContainerName, result.ContainerName);
            Assert.AreEqual(BlobUri, result.Uri);
        }
    }
}