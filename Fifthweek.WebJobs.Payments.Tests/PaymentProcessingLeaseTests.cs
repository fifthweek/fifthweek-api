namespace Fifthweek.WebJobs.Payments.Tests
{
    using System;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Azure;
    using Fifthweek.Payments.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage;

    using Moq;

    [TestClass]
    public class PaymentProcessingLeaseTests
    {
        public const string LeaseId = "lease-id";

        private CancellationToken cancellationToken;
        private Mock<ICloudStorageAccount> cloudStorageAccount;
        private Mock<ICloudBlockBlob> blob;

        private PaymentProcessingLease target;

        public virtual void Initialize()
        {
            this.cancellationToken = new CancellationTokenSource().Token;
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);

            this.target = new PaymentProcessingLease(this.cloudStorageAccount.Object, this.cancellationToken);
        }

        protected void SetupAquireMocks()
        {
            var blobClient = new Mock<ICloudBlobClient>();
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(blobClient.Object);

            var leaseContainer = new Mock<ICloudBlobContainer>();
            blobClient.Setup(v => v.GetContainerReference(Fifthweek.Shared.Constants.AzureLeaseObjectsContainerName))
                .Returns(leaseContainer.Object);

            this.blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            leaseContainer.Setup(v => v.GetBlockBlobReference(Constants.ProcessPaymentsLeaseObjectName)).Returns(this.blob.Object);

            this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken)).ReturnsAsync(LeaseId);
        }

        [TestClass]
        public class TryAcquireLeaseAsync : PaymentProcessingLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task IfAquisitionSucceeds_ItShouldReturnTrue()
            {
                this.SetupAquireMocks();
                var result = await this.target.TryAcquireLeaseAsync();
                Assert.IsTrue(result);
            }

            [TestMethod]
            public async Task IfAquisitionFailsWithConflict_ItShouldReturnFalse()
            {
                this.SetupAquireMocks();
                var webResponse = new Mock<HttpWebResponse>();
                webResponse.SetupGet(v => v.StatusCode).Returns(HttpStatusCode.Conflict);
                this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken))
                    .Throws(new WebException("Blah", new Exception(), WebExceptionStatus.UnknownError, webResponse.Object));

                var result = await this.target.TryAcquireLeaseAsync();
                Assert.IsFalse(result);
            }

            [TestMethod]
            [ExpectedException(typeof(DivideByZeroException))]
            public async Task IfAquisitionFails_ItShouldThrowAnException()
            {
                this.SetupAquireMocks();
                var webResponse = new Mock<HttpWebResponse>();
                webResponse.SetupGet(v => v.StatusCode).Returns(HttpStatusCode.Conflict);
                this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken))
                    .Throws(new DivideByZeroException());

                await this.target.TryAcquireLeaseAsync();
            }
        }

        [TestClass]
        public class AcquireLeaseAsync : PaymentProcessingLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldAcquireALease()
            {
                Assert.IsFalse(this.target.GetIsAcquired());
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();
                Assert.IsTrue(this.target.GetIsAcquired());
            }

            [TestMethod]
            [ExpectedException(typeof(InvalidOperationException))]
            public async Task ItShouldNotAcquireALeaseTwice()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();
                await this.target.AcquireLeaseAsync();
            }
        }

        [TestClass]
        public class RenewLeaseAsync : PaymentProcessingLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldRenewTheLease()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken))
                    .Returns(Task.FromResult(0));

                await this.target.RenewLeaseAsync();
            }
        }

        [TestClass]
        public class ReleaseLeaseAsync : PaymentProcessingLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldReleaseTheLease()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.ReleaseLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken))
                    .Returns(Task.FromResult(0));

                Assert.IsTrue(this.target.GetIsAcquired());
                await this.target.ReleaseLeaseAsync();
                Assert.IsFalse(this.target.GetIsAcquired());
            }

        }

        [TestClass]
        public class KeepAliveAsync : PaymentProcessingLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldRenewTheLease()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken))
                    .Returns(Task.FromResult(0));

                await this.target.KeepAliveAsync();
            }
        }
    }
}