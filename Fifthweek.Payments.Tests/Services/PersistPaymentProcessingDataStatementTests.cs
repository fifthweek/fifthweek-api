namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Azure;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Newtonsoft.Json;

    [TestClass]
    public class PersistPaymentProcessingDataStatementTests
    {
        private Mock<ICloudStorageAccount> cloudStorageAccount;

        private PersistPaymentProcessingDataStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.cloudStorageAccount = new Mock<ICloudStorageAccount>(MockBehavior.Strict);

            this.target = new PersistPaymentProcessingDataStatement(this.cloudStorageAccount.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenDataIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldStoreTheDataInABlob()
        {
            var data = new PersistedPaymentProcessingData(
                Guid.NewGuid(),
                new PaymentProcessingData(
                    UserId.Random(),
                    UserId.Random(),
                    DateTime.UtcNow,
                    DateTime.UtcNow,
                    new CommittedAccountBalance(100m),
                    new List<SubscriberChannelsSnapshot> 
                    {
                        new SubscriberChannelsSnapshot(
                            DateTime.UtcNow, 
                            UserId.Random(),
                            new List<SubscriberChannelsSnapshotItem>
                            {
                                new SubscriberChannelsSnapshotItem(ChannelId.Random(), 100, DateTime.UtcNow),
                                new SubscriberChannelsSnapshotItem(ChannelId.Random(), 110, DateTime.UtcNow),
                            }),
                    },
                    new List<SubscriberSnapshot>
                    {
                        new SubscriberSnapshot(DateTime.UtcNow, UserId.Random(), "a@b.com"),
                        new SubscriberSnapshot(DateTime.UtcNow, UserId.Random(), "x@y.com"),
                    },
                    new List<CalculatedAccountBalanceSnapshot>
                    {
                        new CalculatedAccountBalanceSnapshot(DateTime.UtcNow, UserId.Random(), LedgerAccountType.FifthweekCredit, 10),
                    },
                    new List<CreatorChannelsSnapshot>
                    {
                        new CreatorChannelsSnapshot(
                            DateTime.UtcNow, 
                            UserId.Random(),
                            new List<CreatorChannelsSnapshotItem>
                            {
                                new CreatorChannelsSnapshotItem(ChannelId.Random(), 200),
                                new CreatorChannelsSnapshotItem(ChannelId.Random(), 300),
                            }),
                    },
                    new List<CreatorFreeAccessUsersSnapshot>
                    {
                        new CreatorFreeAccessUsersSnapshot(
                            DateTime.UtcNow, 
                            UserId.Random(), 
                            new List<string> { "b@c.com", "c@d.com" }),
                    },
                    new List<CreatorPost>
                    {
                        new CreatorPost(ChannelId.Random(), DateTime.UtcNow),
                        new CreatorPost(ChannelId.Random(), DateTime.UtcNow),
                    },
                    new CreatorPercentageOverrideData(0.3m, DateTime.UtcNow)),
                new PaymentProcessingResults(
                    new CommittedAccountBalance(80m),
                    new List<PaymentProcessingResult>
                    {
                        new PaymentProcessingResult(DateTime.UtcNow, DateTime.UtcNow.AddDays(1), new AggregateCostSummary(24), null, true),
                        new PaymentProcessingResult(DateTime.UtcNow, DateTime.UtcNow.AddDays(2), new AggregateCostSummary(44), new CreatorPercentageOverrideData(0.5m, DateTime.UtcNow), false),
                    }));

            var client = new Mock<ICloudBlobClient>();
            this.cloudStorageAccount.Setup(v => v.CreateCloudBlobClient()).Returns(client.Object);
            var container = new Mock<ICloudBlobContainer>();
            client.Setup(v => v.GetContainerReference(Constants.PaymentProcessingDataContainerName)).Returns(container.Object);
            var blob = new Mock<ICloudBlockBlob>();
            container.Setup(v => v.GetBlockBlobReference(data.Id.ToString())).Returns(blob.Object);

            string blobString = null;
            blob.Setup(v => v.UploadTextAsync(It.IsAny<string>())).Callback<string>(s => blobString = s).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(data);

            Assert.IsNotNull(blobString);
            Assert.AreEqual(data, JsonConvert.DeserializeObject<PersistedPaymentProcessingData>(blobString));
        }
    }
}