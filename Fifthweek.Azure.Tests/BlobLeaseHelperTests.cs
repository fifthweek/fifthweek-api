namespace Fifthweek.Azure.Tests
{
    using System.Net;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage;

    using Moq;

    [TestClass]
    public class BlobLeaseHelperTests
    {
        private Mock<ICloudStorageAccount> cloudStorageAccount;

        private BlobLeaseHelper target;

        public virtual void Initialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);
            
            this.target = new BlobLeaseHelper(this.cloudStorageAccount.Object);
        }

        public class GetLeaseBlob : BlobLeaseHelperTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public void ItShouldReturnRequestedBlob()
            {
                var leaseObjectName = "leaseObjectName";

                var blobClient = new Mock<ICloudBlobClient>();
                this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(blobClient.Object);

                var leaseContainer = new Mock<ICloudBlobContainer>();
                blobClient.Setup(v => v.GetContainerReference(Azure.Constants.AzureLeaseObjectsContainerName))
                    .Returns(leaseContainer.Object);

                var blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
                leaseContainer.Setup(v => v.GetBlockBlobReference(leaseObjectName)).Returns(blob.Object);

                var result = this.target.GetLeaseBlob(leaseObjectName);

                Assert.AreSame(blob.Object, result);
            }
        }

        public class IsLeaseConflictException : BlobLeaseHelperTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public void WhenConflict_ItShouldReturnTrue()
            {
                var exception = new StorageException(
                    new RequestResult { HttpStatusCode = (int)HttpStatusCode.Conflict },
                    "Error",
                    null);

                var result = this.target.IsLeaseConflictException(exception);

                Assert.IsTrue(result);
            }

            [TestMethod]
            public void WhenNotConflict_ItShouldReturnFalse()
            {
                var exception = new StorageException(
                    new RequestResult { HttpStatusCode = (int)HttpStatusCode.BadRequest },
                    "Error",
                    null);

                var result = this.target.IsLeaseConflictException(exception);

                Assert.IsFalse(result);
            }
        }
    }
}