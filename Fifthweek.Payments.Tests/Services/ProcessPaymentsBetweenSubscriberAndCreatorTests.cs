namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ProcessPaymentsBetweenSubscriberAndCreatorTests
    {
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();

        private static readonly DateTime StartTimeInclusive = DateTime.UtcNow;
        private static readonly DateTime EndTimeExclusive = StartTimeInclusive.AddDays(7);

        private static readonly PaymentProcessingData Data = new PaymentProcessingData(
            SubscriberId,
            CreatorId,
            StartTimeInclusive,
            EndTimeExclusive,
            new List<SubscriberChannelsSnapshot>(),
            new List<SubscriberSnapshot>(),
            new List<CalculatedAccountBalanceSnapshot>(),
            new List<CreatorChannelsSnapshot>(),
            new List<CreatorFreeAccessUsersSnapshot>(),
            new List<CreatorPost>(),
            new CreatorPercentageOverrideData(0.9m, DateTime.UtcNow));
       
        private static readonly PaymentProcessingResults Results =
            new PaymentProcessingResults(new List<PaymentProcessingResult>());

        private Mock<IGetPaymentProcessingData> getPaymentProcessingData;
        private Mock<IProcessPaymentProcessingData> processPaymentProcessingData;
        private Mock<IPersistPaymentProcessingResults> persistPaymentProcessingResults;

        private ProcessPaymentsBetweenSubscriberAndCreator target;

        [TestInitialize]
        public void Initialize()
        {
            this.getPaymentProcessingData = new Mock<IGetPaymentProcessingData>(MockBehavior.Strict);
            this.processPaymentProcessingData = new Mock<IProcessPaymentProcessingData>(MockBehavior.Strict);
            this.persistPaymentProcessingResults = new Mock<IPersistPaymentProcessingResults>(MockBehavior.Strict);

            this.target = new ProcessPaymentsBetweenSubscriberAndCreator(
                this.getPaymentProcessingData.Object,
                this.processPaymentProcessingData.Object,
                this.persistPaymentProcessingResults.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSubscriberIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, CreatorId, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreatorIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(SubscriberId, null, StartTimeInclusive, EndTimeExclusive);
        }

        [TestMethod]
        public async Task ItShouldProcessPayments()
        {
            this.getPaymentProcessingData.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive))
                .ReturnsAsync(Data);

            this.processPaymentProcessingData.Setup(v => v.ExecuteAsync(Data)).ReturnsAsync(Results);

            this.persistPaymentProcessingResults.Setup(v => v.ExecuteAsync(Data, Results))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync(SubscriberId, CreatorId, StartTimeInclusive, EndTimeExclusive);

            this.persistPaymentProcessingResults.Verify();
        }
    }
}