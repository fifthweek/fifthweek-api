namespace Fifthweek.WebJobs.Payments.Tests
{
    using System.Threading;

    using Fifthweek.Azure;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PaymentProcessingLeaseFactoryTests
    {
        private Mock<ICloudStorageAccount> cloudStorageAccount;

        private PaymentProcessingLeaseFactory target;

        [TestInitialize]
        public void Initialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>();
            this.target = new PaymentProcessingLeaseFactory(this.cloudStorageAccount.Object);
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