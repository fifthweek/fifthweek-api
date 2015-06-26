namespace Fifthweek.WebJobs.Payments.Tests
{
    using System.Threading;

    using Fifthweek.Azure;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentProcessingLeaseFactoryTests
    {
        private Mock<ITimestampCreator> timestampCreator;
        private Mock<ICloudStorageAccount> cloudStorageAccount;

        private PaymentProcessingLeaseFactory target;

        [TestInitialize]
        public void Initialize()
        {
            this.timestampCreator = new Mock<ITimestampCreator>();
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>();
            this.target = new PaymentProcessingLeaseFactory(this.timestampCreator.Object, this.cloudStorageAccount.Object);
        }

        [TestMethod]
        public void WhenCreateIsCalled_ItShouldReturnANewPaymentProcessingLease()
        {
            var result = this.target.Create(CancellationToken.None);

            Assert.IsNotNull(result);
            Assert.IsFalse(result.GetIsAcquired());

            var result2 = this.target.Create(CancellationToken.None);

            Assert.AreNotSame(result, result2);
        }
    }
}