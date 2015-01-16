using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Azure.Tests
{
    using System.Threading.Tasks;

    using Microsoft.WindowsAzure.Storage.Blob;

    using Moq;

    [TestClass]
    public class BlobServiceTests
    {
        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<ICloudBlobContainer> cloudBlobContainer;
        private Mock<ICloudBlobClient> cloudBlobClient;
        private Mock<ICloudBlockBlob> cloudBlockBlob;

        private BlobService blobService;

        [TestInitialize]
        public void TestInitialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>();
            this.cloudBlobClient = new Mock<ICloudBlobClient>();
            this.cloudBlobContainer = new Mock<ICloudBlobContainer>();
            this.cloudBlockBlob = new Mock<ICloudBlockBlob>();
            this.blobService = new BlobService(this.cloudStorageAccount.Object);
        }

        [TestMethod]
        public async Task WhenRequestingBlobContainerCreation_ItShouldCreateANewBlobContainer()
        {
            var containerName = "testContainer";
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object).Verifiable();
            this.cloudBlobClient.Setup(v => v.GetContainerReference(containerName)).Returns(this.cloudBlobContainer.Object).Verifiable();
            this.cloudBlobContainer.Setup(v => v.CreateIfNotExistsAsync()).ReturnsAsync(true).Verifiable();

            await this.blobService.CreateBlobContainerAsync(containerName);

            this.cloudStorageAccount.Verify();
            this.cloudBlobClient.Verify();
            this.cloudBlobContainer.Verify();
        }

        [TestMethod]
        public async Task WhenRequestingBlobSasUriForWriting_ItShouldReturnTheCorrectUri()
        {
            var containerName = "testContainer";
            var blobName = "testBlob";
            var uri = "http://uri/";
            var token = "token";

            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object).Verifiable();
            this.cloudBlobClient.Setup(v => v.GetContainerReference(containerName)).Returns(this.cloudBlobContainer.Object).Verifiable();
            this.cloudBlobContainer.Setup(v => v.GetBlockBlobReference(blobName)).Returns(this.cloudBlockBlob.Object).Verifiable();

            this.cloudBlockBlob.Setup(v => v.Uri).Returns(new Uri(uri)).Verifiable();
            SharedAccessBlobPolicy submittedPolicy = null;
            this.cloudBlockBlob.Setup(v => v.GetSharedAccessSignature(It.IsAny<SharedAccessBlobPolicy>()))
                .Callback<SharedAccessBlobPolicy>(v => submittedPolicy = v).Returns(token).Verifiable();

            var result = await this.blobService.GetBlobSasUriForWritingAsync(containerName, blobName);

            this.cloudStorageAccount.Verify();
            this.cloudBlobClient.Verify();
            this.cloudBlobContainer.Verify();
            this.cloudBlockBlob.Verify();

            Assert.IsNotNull(submittedPolicy);
            Assert.AreEqual(SharedAccessBlobPermissions.Write, submittedPolicy.Permissions);
            Assert.AreEqual(null, submittedPolicy.SharedAccessStartTime);
            Assert.IsTrue(submittedPolicy.SharedAccessExpiryTime.HasValue);
            var expiry = submittedPolicy.SharedAccessExpiryTime.Value;
            Assert.IsTrue(expiry.Offset.Ticks == 0);
            Assert.IsTrue(expiry > DateTime.UtcNow.AddMinutes(15));
            Assert.IsTrue(expiry < DateTime.UtcNow.AddDays(1));

            Assert.AreEqual(result, uri + token);
        }

        [TestMethod]
        public async Task WhenRequestingBlobSasUriForReading_ItShouldReturnTheCorrectUri()
        {
            var containerName = "testContainer";
            var blobName = "testBlob";
            var uri = "http://uri/";
            var token = "token";

            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object).Verifiable();
            this.cloudBlobClient.Setup(v => v.GetContainerReference(containerName)).Returns(this.cloudBlobContainer.Object).Verifiable();
            this.cloudBlobContainer.Setup(v => v.GetBlockBlobReference(blobName)).Returns(this.cloudBlockBlob.Object).Verifiable();

            this.cloudBlockBlob.Setup(v => v.Uri).Returns(new Uri(uri)).Verifiable();
            SharedAccessBlobPolicy submittedPolicy = null;
            this.cloudBlockBlob.Setup(v => v.GetSharedAccessSignature(It.IsAny<SharedAccessBlobPolicy>()))
                .Callback<SharedAccessBlobPolicy>(v => submittedPolicy = v).Returns(token).Verifiable();

            var result = await this.blobService.GetBlobSasUriForReadingAsync(containerName, blobName);

            this.cloudStorageAccount.Verify();
            this.cloudBlobClient.Verify();
            this.cloudBlobContainer.Verify();
            this.cloudBlockBlob.Verify();

            Assert.IsNotNull(submittedPolicy);
            Assert.AreEqual(SharedAccessBlobPermissions.Read, submittedPolicy.Permissions);
            Assert.AreEqual(null, submittedPolicy.SharedAccessStartTime);
            Assert.IsTrue(submittedPolicy.SharedAccessExpiryTime.HasValue);
            var expiry = submittedPolicy.SharedAccessExpiryTime.Value;
            Assert.IsTrue(expiry.Offset.Ticks == 0);
            Assert.IsTrue(expiry > DateTime.UtcNow.AddMinutes(15));
            Assert.IsTrue(expiry < DateTime.UtcNow.AddDays(1));

            Assert.AreEqual(result, uri + token);
        }

        [TestMethod]
        public async Task WhenGettingBlobLengthAndSettingContentType_ItShouldFetchAttributesFromAzureAndSave()
        {
            var containerName = "testContainer";
            var blobName = "testBlob";
            var contentType = "test/fifthweek";
            var blobLength = 1111;

            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object);
            this.cloudBlobClient.Setup(v => v.GetContainerReference(containerName)).Returns(this.cloudBlobContainer.Object);
            this.cloudBlobContainer.Setup(v => v.GetBlockBlobReference(blobName)).Returns(this.cloudBlockBlob.Object);

            var properties = new Mock<IBlobProperties>();
            properties.Setup(v => v.Length).Returns(blobLength);
            this.cloudBlockBlob.Setup(v => v.Properties).Returns(properties.Object);

            this.cloudBlockBlob.Setup(v => v.FetchAttributesAsync()).Returns(Task.FromResult(0)).Verifiable();
            properties.SetupSet(v => v.ContentType = contentType).Verifiable();
            this.cloudBlockBlob.Setup(v => v.SetPropertiesAsync()).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.blobService.GetBlobLengthAndSetContentTypeAsync(containerName, blobName, contentType);

            this.cloudBlockBlob.Verify();
            properties.Verify();

            Assert.AreEqual(blobLength, result);
        }
    }
}
