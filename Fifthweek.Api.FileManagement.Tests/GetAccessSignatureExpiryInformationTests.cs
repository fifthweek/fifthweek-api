namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetAccessSignatureExpiryInformationTests
    {
        private GetAccessSignatureExpiryInformation target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetAccessSignatureExpiryInformation();
        }

        [TestMethod]
        public async Task WhenGettingNextPublicExpiry_ItShouldReturnTheNextWholeWeekTakingMinimumExpiryIntoAccount()
        {
            DateTime result;
            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 30, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 49, 59, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 23, 00, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 22, 23, 50, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(new DateTime(2015, 3, 30, 00, 00, 00, DateTimeKind.Utc), result);
        }

        [TestMethod]
        public async Task WhenGettingNextPrivateExpiry_ItShouldReturnTheNextWholeHourTakingMinimumExpiryIntoAccount()
        {
            DateTime result;
            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 30, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 49, 59, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 11, 00, 00, DateTimeKind.Utc), result);

            result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 50, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(new DateTime(2015, 3, 18, 12, 00, 00, DateTimeKind.Utc), result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingNextPublicExpiry_ItShouldExpectTimesAsUtc()
        {
            this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00), true);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingNextPrivateExpiry_ItShouldExpectTimesAsUtc()
        {
            this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00), false);
        }

        [TestMethod]
        public async Task WhenGettingNextPublicExpiry_ItShouldReturnTimesAsUtc()
        {
            var result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), true);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }

        [TestMethod]
        public async Task WhenGettingNextPrivateExpiry_ItShouldReturnTimesAsUtc()
        {
            var result = this.target.GetNextExpiry(new DateTime(2015, 3, 18, 10, 00, 00, DateTimeKind.Utc), false);
            Assert.AreEqual(DateTimeKind.Utc, result.Kind);
        }
    }
}