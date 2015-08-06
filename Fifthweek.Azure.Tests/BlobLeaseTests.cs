namespace Fifthweek.Azure.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Microsoft.WindowsAzure.Storage;

    using Moq;

    [TestClass]
    public class BlobLeaseTests
    {
        public const string LeaseId = "lease-id";
        public const string LeaseObjectName = "lease-object-name";

        private static readonly DateTime Now = DateTime.UtcNow;

        private CancellationToken cancellationToken;
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IBlobLeaseHelper> blobLeaseHelper;
        private Mock<ICloudBlockBlob> blob;

        private BlobLease target;

        public virtual void Initialize()
        {
            this.cancellationToken = new CancellationTokenSource().Token;
            this.blobLeaseHelper = new Mock<IBlobLeaseHelper>(MockBehavior.Strict);
            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);

            this.target = new BlobLease(this.timestampCreator.Object, this.blobLeaseHelper.Object, this.cancellationToken, LeaseObjectName);
        }

        protected void SetupAquireMocks()
        {
            this.blob = new Mock<ICloudBlockBlob>(MockBehavior.Strict);
            this.blobLeaseHelper.Setup(v => v.GetLeaseBlob(LeaseObjectName)).Returns(this.blob.Object);

            this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken)).ReturnsAsync(LeaseId);

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);
        }

        [TestClass]
        public class TryAcquireLeaseAsync : BlobLeaseTests
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

                var exception = new DivideByZeroException();
                this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken))
                    .Throws(exception);

                this.blobLeaseHelper.Setup(v => v.IsLeaseConflictException(exception)).Returns(true);

                var result = await this.target.TryAcquireLeaseAsync();
                Assert.IsFalse(result);
            }

            [TestMethod]
            [ExpectedException(typeof(DivideByZeroException))]
            public async Task IfAquisitionFails_ItShouldThrowAnException()
            {
                this.SetupAquireMocks();

                var exception = new DivideByZeroException();
                this.blob.Setup(v => v.AcquireLeaseAsync(TimeSpan.FromMinutes(1), null, this.cancellationToken))
                    .Throws(exception);

                this.blobLeaseHelper.Setup(v => v.IsLeaseConflictException(exception)).Returns(false);

                await this.target.TryAcquireLeaseAsync();
            }
        }

        [TestClass]
        public class AcquireLeaseAsync : BlobLeaseTests
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
        public class RenewLeaseAsync : BlobLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldRenewTheLeaseWithRateLimiter()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken))
                    .Returns(Task.FromResult(0)).Verifiable();

                await this.target.RenewLeaseAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(0));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds - 1));
                await this.target.RenewLeaseAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(0));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds));
                await this.target.RenewLeaseAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(1));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds((BlobLease.RenewRateLimitSeconds * 2) - 1));
                await this.target.RenewLeaseAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(1));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds * 2));
                await this.target.RenewLeaseAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(2));

                this.blob.Verify();
            }
        }

        [TestClass]
        public class ReleaseLeaseAsync : BlobLeaseTests
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
        public class KeepAliveAsync : BlobLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldRenewTheLeaseWithRateLimiter()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken))
                    .Returns(Task.FromResult(0)).Verifiable();

                await this.target.KeepAliveAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(0));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds - 1));
                await this.target.KeepAliveAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(0));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds));
                await this.target.KeepAliveAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(1));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds((BlobLease.RenewRateLimitSeconds * 2) - 1));
                await this.target.KeepAliveAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(1));

                this.timestampCreator.Setup(v => v.Now()).Returns(Now.AddSeconds(BlobLease.RenewRateLimitSeconds * 2));
                await this.target.KeepAliveAsync();
                this.blob.Verify(v => v.RenewLeaseAsync(It.IsAny<AccessCondition>(), this.cancellationToken), Times.Exactly(2));

                this.blob.Verify();
            }
        }

        [TestClass]
        public class GetTimeSinceLastLeaseAsync : BlobLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task WhenNoAttributes_ItShouldReturnMaxTimeSpan()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.FetchAttributesAsync(this.cancellationToken)).Returns(Task.FromResult(0)).Verifiable();

                var metadata = new Dictionary<string, string>();
                this.blob.SetupGet(v => v.Metadata).Returns(metadata);

                var result = await this.target.GetTimeSinceLastLeaseAsync();

                this.blob.Verify();

                Assert.AreEqual(TimeSpan.MaxValue, result);
            }

            [TestMethod]
            public async Task WhenAttributes_ItShouldReturnTimeSinceLastLeaseEndTime()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();

                this.blob.Setup(v => v.FetchAttributesAsync(this.cancellationToken)).Returns(Task.FromResult(0)).Verifiable();

                var metadata = new Dictionary<string, string>();
                metadata.Add(Azure.Constants.LeaseEndTimestampMetadataKey, Now.AddMinutes(-12).ToIso8601String());
                this.blob.SetupGet(v => v.Metadata).Returns(metadata);

                var result = await this.target.GetTimeSinceLastLeaseAsync();

                this.blob.Verify();

                Assert.AreEqual(TimeSpan.FromMinutes(12), result);
            }
        }

        [TestClass]
        public class UpdateTimestampsAsync : BlobLeaseTests
        {
            [TestInitialize]
            public override void Initialize()
            {
                base.Initialize();
            }

            [TestMethod]
            public async Task ItShouldUpdateTheTimestamps()
            {
                this.SetupAquireMocks();
                await this.target.AcquireLeaseAsync();
                await this.target.RenewLeaseAsync();
                await this.target.KeepAliveAsync();
                await this.target.RenewLeaseAsync();

                var end = Now.AddDays(1);
                this.timestampCreator.Setup(v => v.Now()).Returns(end);

                this.blob.Setup(v => v.FetchAttributesAsync(this.cancellationToken)).Returns(Task.FromResult(0)).Verifiable();

                var metadata = new Dictionary<string, string>();
                this.blob.SetupGet(v => v.Metadata).Returns(metadata);

                this.blob.Setup(v => v.SetMetadataAsync(It.IsAny<AccessCondition>(), null, null, this.cancellationToken)).Returns(Task.FromResult(0)).Verifiable();

                await this.target.UpdateTimestampsAsync();

                this.blob.Verify();

                Assert.AreEqual<DateTime>(Now, metadata[Azure.Constants.LeaseStartTimestampMetadataKey].FromIso8601String());
                Assert.AreEqual<DateTime>(end, metadata[Azure.Constants.LeaseEndTimestampMetadataKey].FromIso8601String());
                Assert.AreEqual(3, int.Parse(metadata[Azure.Constants.LeaseRenewCountMetadataKey]));
            }
        }
    }
}