namespace Fifthweek.Azure.Tests
{
    using System.Threading;

    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BlobLeaseFactoryTests
    {
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<IBlobLeaseHelper> blobLeaseHelper;

        private BlobLeaseFactory target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.blobLeaseHelper = new Mock<IBlobLeaseHelper>();
            this.target = new BlobLeaseFactory(this.timestampCreator.Object, this.blobLeaseHelper.Object);
        }

        [TestMethod]
        public void WhenCreateIsCalled_ItShouldReturnANewPaymentProcessingLease()
        {
            var result = this.target.Create("leaseObjectName", CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.GetIsAcquired());

            var result2 = this.target.Create("leaseObjectName", CancellationToken.None);

            Assert.AreNotSame(result, result2);
        }
    }
}