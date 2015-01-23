using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Azure.Tests
{
    using System.Threading.Tasks;

    using Fifthweek.Azure;

    using Microsoft.WindowsAzure.Storage.Blob;

    using Moq;

    [TestClass]
    public class BlobServiceTests
    {
        private static readonly string ContainerName = "testContainer";
        private static readonly string BlobName = "testBlob";
        private static readonly string Uri = "http://uri/";
        private static readonly string Token = "token";
        private static readonly string ContentType = "test/fifthweek";
        private static readonly int BlobLength = 1111;

        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<ICloudBlobContainer> cloudBlobContainer;
        private Mock<ICloudBlobClient> cloudBlobClient;
        private Mock<ICloudBlockBlob> cloudBlockBlob;

        private BlobService target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>();
            this.cloudBlobClient = new Mock<ICloudBlobClient>();
            this.cloudBlobContainer = new Mock<ICloudBlobContainer>();
            this.cloudBlockBlob = new Mock<ICloudBlockBlob>();

            this.target = new BlobService(this.cloudStorageAccount.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequestingBlobContainerCreation_ItShouldCheckContainerNameIsNotNull()
        {
            await this.target.CreateBlobContainerAsync(null);
        }

        [TestMethod]
        public async Task WhenRequestingBlobContainerCreation_ItShouldCreateANewBlobContainer()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object);
            this.cloudBlobClient.Setup(v => v.GetContainerReference(ContainerName)).Returns(this.cloudBlobContainer.Object);

            this.cloudBlobContainer.Setup(v => v.CreateIfNotExistsAsync()).ReturnsAsync(true).Verifiable();

            await this.target.CreateBlobContainerAsync(ContainerName);

            this.cloudBlobContainer.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequestingBlobSasUriForWriting_ItShouldCheckContainerNameIsNotNull()
        {
            await this.target.GetBlobSharedAccessInformationForWritingAsync(null, BlobName);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequestingBlobSasUriForWriting_ItShouldCheckBlobNameIsNotNull()
        {
            await this.target.GetBlobSharedAccessInformationForWritingAsync(ContainerName, null);
        }

        [TestMethod]
        public async Task WhenRequestingBlobSasUriForWriting_ItShouldReturnTheCorrectUri()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object);
            this.cloudBlobClient.Setup(v => v.GetContainerReference(ContainerName)).Returns(this.cloudBlobContainer.Object);
            this.cloudBlobContainer.Setup(v => v.GetBlockBlobReference(BlobName)).Returns(this.cloudBlockBlob.Object);

            this.cloudBlockBlob.Setup(v => v.Uri).Returns(new Uri(Uri));

            SharedAccessBlobPolicy submittedPolicy = null;
            this.cloudBlockBlob.Setup(v => v.GetSharedAccessSignature(It.IsAny<SharedAccessBlobPolicy>()))
                .Callback<SharedAccessBlobPolicy>(v => submittedPolicy = v).Returns(Token).Verifiable();

            var result = await this.target.GetBlobSharedAccessInformationForWritingAsync(ContainerName, BlobName);

            Assert.IsNotNull(submittedPolicy);
            Assert.AreEqual(SharedAccessBlobPermissions.Write, submittedPolicy.Permissions);
            Assert.AreEqual(null, submittedPolicy.SharedAccessStartTime);
            Assert.IsTrue(submittedPolicy.SharedAccessExpiryTime.HasValue);
            var expiry = submittedPolicy.SharedAccessExpiryTime.Value;
            Assert.IsTrue(expiry.Offset.Ticks == 0);
            Assert.IsTrue(expiry > DateTime.UtcNow.AddMinutes(15));
            Assert.IsTrue(expiry < DateTime.UtcNow.AddDays(1));

            Assert.IsNotNull(result);
            Assert.AreEqual(ContainerName, result.ContainerName);
            Assert.AreEqual(BlobName, result.BlobName);
            Assert.AreEqual(submittedPolicy.SharedAccessExpiryTime, (DateTimeOffset)result.Expiry);
            Assert.AreEqual(Token, result.Signature);
            Assert.AreEqual(Uri, result.Uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequestingBlobContainerSasUriForReading_ItShouldCheckContainerNameIsNotNull()
        {
            await this.target.GetBlobContainerSharedAccessInformationForReadingAsync(null);
        }

        [TestMethod]
        public async Task WhenRequestingBlobContainerSasUriForReading_ItShouldReturnTheCorrectUri()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object);
            this.cloudBlobClient.Setup(v => v.GetContainerReference(ContainerName)).Returns(this.cloudBlobContainer.Object);

            this.cloudBlobContainer.Setup(v => v.Uri).Returns(new Uri(Uri));

            SharedAccessBlobPolicy submittedPolicy = null;
            this.cloudBlobContainer.Setup(v => v.GetSharedAccessSignature(It.IsAny<SharedAccessBlobPolicy>()))
                .Callback<SharedAccessBlobPolicy>(v => submittedPolicy = v).Returns(Token);

            var result = await this.target.GetBlobContainerSharedAccessInformationForReadingAsync(ContainerName);

            Assert.IsNotNull(submittedPolicy);
            Assert.AreEqual(SharedAccessBlobPermissions.Read, submittedPolicy.Permissions);
            Assert.AreEqual(null, submittedPolicy.SharedAccessStartTime);
            Assert.IsTrue(submittedPolicy.SharedAccessExpiryTime.HasValue);
            var expiry = submittedPolicy.SharedAccessExpiryTime.Value;
            Assert.IsTrue(expiry.Offset.Ticks == 0);
            Assert.IsTrue(expiry > DateTime.UtcNow.AddMinutes(15));
            Assert.IsTrue(expiry < DateTime.UtcNow.AddDays(1));

            Assert.IsNotNull(result);
            Assert.AreEqual(ContainerName, result.ContainerName);
            Assert.AreEqual(submittedPolicy.SharedAccessExpiryTime, (DateTimeOffset)result.Expiry);
            Assert.AreEqual(Token, result.Signature);
            Assert.AreEqual(Uri, result.Uri);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGettingBlobLengthAndSettingContentType_ItShouldCheckContainerNameIsNotNull()
        {
            await this.target.GetBlobLengthAndSetContentTypeAsync(null, BlobName, ContentType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGettingBlobLengthAndSettingContentType_ItShouldCheckBlobNameIsNotNull()
        {
            await this.target.GetBlobLengthAndSetContentTypeAsync(ContainerName, null, ContentType);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGettingBlobLengthAndSettingContentType_ItShouldCheckContentTypeNameIsNotNull()
        {
            await this.target.GetBlobLengthAndSetContentTypeAsync(ContainerName, BlobName, null);
        }

        [TestMethod]
        public async Task WhenGettingBlobLengthAndSettingContentType_ItShouldFetchAttributesFromAzureAndSave()
        {
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(this.cloudBlobClient.Object);
            this.cloudBlobClient.Setup(v => v.GetContainerReference(ContainerName)).Returns(this.cloudBlobContainer.Object);
            this.cloudBlobContainer.Setup(v => v.GetBlockBlobReference(BlobName)).Returns(this.cloudBlockBlob.Object);

            var properties = new Mock<IBlobProperties>();
            properties.Setup(v => v.Length).Returns(BlobLength);
            this.cloudBlockBlob.Setup(v => v.Properties).Returns(properties.Object);

            this.cloudBlockBlob.Setup(v => v.FetchAttributesAsync()).Returns(Task.FromResult(0)).Verifiable();
            properties.SetupSet(v => v.ContentType = ContentType).Verifiable();
            this.cloudBlockBlob.Setup(v => v.SetPropertiesAsync()).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.GetBlobLengthAndSetContentTypeAsync(ContainerName, BlobName, ContentType);

            this.cloudBlockBlob.Verify();
            properties.Verify();

            Assert.AreEqual(BlobLength, result);
        }
    }
}
