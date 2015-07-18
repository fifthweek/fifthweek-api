namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Snapshots;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PersistPaymentProcessingResultsTests
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
            new PaymentProcessingResults(new List<PaymentProcessingResult> 
            {
                new PaymentProcessingResult(
                    StartTimeInclusive, 
                    StartTimeInclusive.AddDays(1), 
                    new AggregateCostSummary(1), 
                    Data.CreatorPercentageOverride, 
                    true),
                new PaymentProcessingResult(
                    StartTimeInclusive.AddDays(1), 
                    StartTimeInclusive.AddDays(2), 
                    new AggregateCostSummary(2), 
                    null, 
                    true),
                new PaymentProcessingResult(
                    StartTimeInclusive.AddDays(2), 
                    StartTimeInclusive.AddDays(3), 
                    new AggregateCostSummary(0), 
                    null, 
                    true),
                new PaymentProcessingResult(
                    StartTimeInclusive.AddDays(3), 
                    StartTimeInclusive.AddDays(4), 
                    new AggregateCostSummary(3), 
                    null, 
                    false),
            });

        private Mock<IGuidCreator> guidCreator;
        private Mock<IPersistPaymentProcessingDataStatement> persistPaymentProcessingData;
        private Mock<IPersistCommittedAndUncommittedRecordsDbStatement> persistCommittedAndUncommittedRecords;

        private PersistPaymentProcessingResults target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>();
            this.persistPaymentProcessingData = new Mock<IPersistPaymentProcessingDataStatement>(MockBehavior.Strict);
            this.persistCommittedAndUncommittedRecords = new Mock<IPersistCommittedAndUncommittedRecordsDbStatement>(MockBehavior.Strict);

            this.target = new PersistPaymentProcessingResults(
                this.guidCreator.Object,
                this.persistPaymentProcessingData.Object,
                this.persistCommittedAndUncommittedRecords.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenDataIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Results);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenResultIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(Data, null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenMultipleUncommittedRecords_ItShouldThrowAnException()
        {
            var resultItems = new List<PaymentProcessingResult>(Results.Items);
            resultItems.Add(resultItems.Last());
            var results = new PaymentProcessingResults(resultItems);

            await this.target.ExecuteAsync(Data, results);
        }

        private Guid CreateGuid(byte a, byte b)
        {
            return new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, a, b);
        }

        [TestMethod]
        public async Task ItShouldPersistTheData()
        {
            byte guidIndex = 0;
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.Create()).Returns(() => this.CreateGuid(0, guidIndex++));
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(1, sequentialGuidIndex++));

            var dataId = this.CreateGuid(0, 0);
            var transactionId1 = this.CreateGuid(0, 1);
            var transactionId2 = this.CreateGuid(0, 2);
            var transactionId3 = this.CreateGuid(0, 3);

            this.persistPaymentProcessingData.Setup(
                v => v.ExecuteAsync(new PersistedPaymentProcessingData(dataId, Data, Results)))
                .Returns(Task.FromResult(0));

            IReadOnlyList<AppendOnlyLedgerRecord> actualCommittedRecords = null;
            UncommittedSubscriptionPayment actualUncommittedRecord = null;
            this.persistCommittedAndUncommittedRecords.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId, It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>(), It.IsAny<UncommittedSubscriptionPayment>()))
                .Callback<UserId, UserId, IReadOnlyList<AppendOnlyLedgerRecord>, UncommittedSubscriptionPayment>(
                    (s, c, a, b) =>
                    {
                        actualCommittedRecords = a;
                        actualUncommittedRecord = b;
                    })
                    .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(Data, Results);

            CollectionAssert.AreEqual(
                new List<AppendOnlyLedgerRecord>
                {
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 0), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), -1m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 1), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), 1m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 2), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), -0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 3), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), 0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 4), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), -2m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 5), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), 2m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment ,transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 6), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), -1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 7), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), 1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 8), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(3), 0m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId3, dataId, null, null, null), 
                },
                actualCommittedRecords.ToList());

            Assert.AreEqual(
                new UncommittedSubscriptionPayment(
                    SubscriberId.Value,
                    CreatorId.Value,
                    StartTimeInclusive.AddDays(3),
                    StartTimeInclusive.AddDays(4),
                    3,
                    dataId),
                actualUncommittedRecord);
        }

        [TestMethod]
        public async Task WhenNoUncommittedRecord_ItShouldPersistTheData()
        {
            byte guidIndex = 0;
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.Create()).Returns(() => this.CreateGuid(0, guidIndex++));
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(1, sequentialGuidIndex++));

            var dataId = this.CreateGuid(0, 0);
            var transactionId1 = this.CreateGuid(0, 1);
            var transactionId2 = this.CreateGuid(0, 2);
            var transactionId3 = this.CreateGuid(0, 3);

            var resultItems = new List<PaymentProcessingResult>(Results.Items);
            resultItems.Remove(resultItems.Last());
            var results = new PaymentProcessingResults(resultItems);

            this.persistPaymentProcessingData.Setup(
                v => v.ExecuteAsync(new PersistedPaymentProcessingData(dataId, Data, results)))
                .Returns(Task.FromResult(0));

            IReadOnlyList<AppendOnlyLedgerRecord> actualCommittedRecords = null;
            UncommittedSubscriptionPayment actualUncommittedRecord = null;
            this.persistCommittedAndUncommittedRecords.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId, It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>(), It.IsAny<UncommittedSubscriptionPayment>()))
                .Callback<UserId, UserId, IReadOnlyList<AppendOnlyLedgerRecord>, UncommittedSubscriptionPayment>(
                    (s, c, a, b) =>
                    {
                        actualCommittedRecords = a;
                        actualUncommittedRecord = b;
                    })
                    .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(Data, results);

            CollectionAssert.AreEqual(
                new List<AppendOnlyLedgerRecord>
                {
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 0), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), -1m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 1), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), 1m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 2), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), -0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 3), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), 0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 4), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), -2m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 5), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), 2m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 6), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), -1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 7), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), 1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 8), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(3), 0m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId3, dataId, null, null, null), 
                },
                actualCommittedRecords.ToList());

            Assert.IsNull(actualUncommittedRecord);
        }

        [TestMethod]
        public async Task WhenUncommittedRecordHasZeroAmount_ItShouldPersistTheData()
        {
            byte guidIndex = 0;
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.Create()).Returns(() => this.CreateGuid(0, guidIndex++));
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(1, sequentialGuidIndex++));

            var dataId = this.CreateGuid(0, 0);
            var transactionId1 = this.CreateGuid(0, 1);
            var transactionId2 = this.CreateGuid(0, 2);
            var transactionId3 = this.CreateGuid(0, 3);

            var resultItems = new List<PaymentProcessingResult>(Results.Items);
            var uncommitted = resultItems.Last();
            resultItems[resultItems.Count - 1] = new PaymentProcessingResult(
                uncommitted.StartTimeInclusive,
                uncommitted.EndTimeExclusive,
                new AggregateCostSummary(0),
                uncommitted.CreatorPercentageOverride,
                false);
            var results = new PaymentProcessingResults(resultItems);

            this.persistPaymentProcessingData.Setup(
                v => v.ExecuteAsync(new PersistedPaymentProcessingData(dataId, Data, results)))
                .Returns(Task.FromResult(0));

            IReadOnlyList<AppendOnlyLedgerRecord> actualCommittedRecords = null;
            UncommittedSubscriptionPayment actualUncommittedRecord = null;
            this.persistCommittedAndUncommittedRecords.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId, It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>(), It.IsAny<UncommittedSubscriptionPayment>()))
                .Callback<UserId, UserId, IReadOnlyList<AppendOnlyLedgerRecord>, UncommittedSubscriptionPayment>(
                    (s, c, a, b) =>
                    {
                        actualCommittedRecords = a;
                        actualUncommittedRecord = b;
                    })
                    .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(Data, results);

            CollectionAssert.AreEqual(
                new List<AppendOnlyLedgerRecord>
                {
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 0), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), -1m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 1), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), 1m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 2), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(1), -0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 3), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(1), 0.9m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId1, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 4), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), -2m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 5), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), 2m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 6), Guid.Empty, CreatorId.Value, StartTimeInclusive.AddDays(2), -1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 7), CreatorId.Value, CreatorId.Value, StartTimeInclusive.AddDays(2), 1.4m, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionId2, dataId, null, null, null), 
                    new AppendOnlyLedgerRecord(this.CreateGuid(1, 8), SubscriberId.Value, CreatorId.Value, StartTimeInclusive.AddDays(3), 0m, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionId3, dataId, null, null, null), 
                },
                actualCommittedRecords.ToList());

            Assert.IsNull(actualUncommittedRecord);
        }

        [TestMethod]
        public async Task WhenNoCommittedRecords_ItShouldPersistTheData()
        {
            byte guidIndex = 0;
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.Create()).Returns(() => this.CreateGuid(0, guidIndex++));
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(1, sequentialGuidIndex++));

            var dataId = this.CreateGuid(0, 0);

            var resultItems = new List<PaymentProcessingResult>(Results.Items);
            resultItems.Remove(resultItems.First());
            resultItems.Remove(resultItems.First());
            resultItems.Remove(resultItems.First());
            var results = new PaymentProcessingResults(resultItems);

            this.persistPaymentProcessingData.Setup(
                v => v.ExecuteAsync(new PersistedPaymentProcessingData(dataId, Data, results)))
                .Returns(Task.FromResult(0));

            IReadOnlyList<AppendOnlyLedgerRecord> actualCommittedRecords = null;
            UncommittedSubscriptionPayment actualUncommittedRecord = null;
            this.persistCommittedAndUncommittedRecords.Setup(v => v.ExecuteAsync(SubscriberId, CreatorId, It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>(), It.IsAny<UncommittedSubscriptionPayment>()))
                .Callback<UserId, UserId, IReadOnlyList<AppendOnlyLedgerRecord>, UncommittedSubscriptionPayment>(
                    (s, c, a, b) =>
                    {
                        actualCommittedRecords = a;
                        actualUncommittedRecord = b;
                    })
                    .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(Data, results);

            CollectionAssert.AreEqual(
                new List<AppendOnlyLedgerRecord>(),
                actualCommittedRecords.ToList());

            Assert.AreEqual(
                new UncommittedSubscriptionPayment(
                    SubscriberId.Value,
                    CreatorId.Value,
                    StartTimeInclusive.AddDays(3),
                    StartTimeInclusive.AddDays(4),
                    3,
                    dataId),
                actualUncommittedRecord);
        }
    }
}