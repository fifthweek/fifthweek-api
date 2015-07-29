namespace Fifthweek.Payments.Tests.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateTransactionRefundTests
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();

        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly string Comment = "comment";

        private Mock<IGuidCreator> guidCreator;
        private Mock<IGetRecordsForTransactionDbStatement> getRecordsForTransaction;
        private Mock<IPersistCommittedRecordsDbStatement> persistCommittedRecords;

        private CreateTransactionRefund target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>(MockBehavior.Strict);
            this.getRecordsForTransaction = new Mock<IGetRecordsForTransactionDbStatement>(MockBehavior.Strict);
            this.persistCommittedRecords = new Mock<IPersistCommittedRecordsDbStatement>(MockBehavior.Strict);
            this.target = new CreateTransactionRefund(
                this.guidCreator.Object,
                this.getRecordsForTransaction.Object,
                this.persistCommittedRecords.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEnactingUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, TransactionReference.Random(), Now, Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(EnactingUserId, null, Now, Comment);
        }

        [TestMethod]
        public async Task ItShouldReverseSpecifiedTransaction()
        {
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => CreateGuid(0, sequentialGuidIndex++));

            var data = PayCreator(SubscriberId, CreatorId, 100);
            var transactionReference = new TransactionReference(data.First().TransactionReference);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(data);

            int index = 0;
            var expectedInserts = data.Select(
                v => new AppendOnlyLedgerRecord(
                    CreateGuid(0, (byte)(index++)),
                    v.AccountOwnerId,
                    v.CounterpartyId,
                    Now,
                    -v.Amount,
                    v.AccountType,
                    LedgerTransactionType.SubscriptionRefund,
                    transactionReference.Value,
                    v.InputDataReference,
                    "Performed by " + EnactingUserId + " - comment",
                    null,
                    null)).ToList();

            IReadOnlyList<AppendOnlyLedgerRecord> actualInserts = null;
            this.persistCommittedRecords.Setup(v => v.ExecuteAsync(It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>()))
                .Callback<IReadOnlyList<AppendOnlyLedgerRecord>>(v => actualInserts = v)
                .Returns(Task.FromResult(0))
                .Verifiable();

            var result = await this.target.ExecuteAsync(EnactingUserId, transactionReference, Now, Comment);

            Assert.AreEqual(
                new CreateTransactionRefund.CreateTransactionRefundResult(SubscriberId, CreatorId),
                result);

            CollectionAssert.AreEquivalent(expectedInserts, actualInserts.ToList());

            this.persistCommittedRecords.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenRefundRecordsAlreadyExist_ItShouldThrowAnException()
        {
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => CreateGuid(0, sequentialGuidIndex++));

            var data = PayCreator(SubscriberId, CreatorId, 100);
            var transactionReference = new TransactionReference(data.First().TransactionReference);

            var expectedInserts = data.Select(
                v => new AppendOnlyLedgerRecord(
                    CreateGuid(0, 0),
                    SubscriberId.Value,
                    CreatorId.Value,
                    Now,
                    -v.Amount,
                    v.AccountType,
                    LedgerTransactionType.SubscriptionRefund,
                    transactionReference.Value,
                    v.InputDataReference,
                    "Performed by " + EnactingUserId + " - comment",
                    null,
                    null)).ToList();

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(data.Concat(expectedInserts).ToList());

            await this.target.ExecuteAsync(EnactingUserId, transactionReference, Now, Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenRecordsAreNotForSubscriptionPayment_ItShouldThrowAnException()
        {
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => CreateGuid(0, sequentialGuidIndex++));

            var transactionReference = TransactionReference.Random();

            var record = new AppendOnlyLedgerRecord(
                CreateGuid(0, 0),
                SubscriberId.Value,
                CreatorId.Value,
                Now,
                100,
                LedgerAccountType.FifthweekCredit,
                LedgerTransactionType.CreditAddition,
                transactionReference.Value,
                null,
                "Performed by " + EnactingUserId + " - comment",
                null,
                null);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference))
                .ReturnsAsync(new List<AppendOnlyLedgerRecord> { record });

            await this.target.ExecuteAsync(EnactingUserId, transactionReference, Now, Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenNoMatchingRecords_ItShouldThrowAnException()
        {
            byte sequentialGuidIndex = 0;
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => CreateGuid(0, sequentialGuidIndex++));

            var transactionReference = TransactionReference.Random();

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference))
                .ReturnsAsync(new List<AppendOnlyLedgerRecord>());

            await this.target.ExecuteAsync(EnactingUserId, transactionReference, Now, Comment);
        }

        private static List<AppendOnlyLedgerRecord> PayCreator(
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount)
        {
            var random = new Random();
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = Guid.NewGuid();
            result.Add(CreateAppendOnlyLedgerRecord(random, sourceUserId, destinationUserId, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, null, destinationUserId, amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, null, destinationUserId, -0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            return result;
        }

        private static AppendOnlyLedgerRecord CreateAppendOnlyLedgerRecord(Random random, UserId accountOwnerId, UserId counterpartyId, decimal amount, LedgerAccountType ledgerAccountType, LedgerTransactionType transactionType, Guid transactionReference)
        {
            var record = new AppendOnlyLedgerRecord(
                Guid.NewGuid(),
                accountOwnerId == null ? Guid.Empty : accountOwnerId.Value,
                counterpartyId == null ? (Guid?)null : counterpartyId.Value,
                Now.AddDays(random.Next(-100, 100)),
                amount,
                ledgerAccountType,
                transactionType,
                transactionReference,
                Guid.NewGuid(),
                null,
                null,
                null);

            return record;
        }

        private static Guid CreateGuid(byte a, byte b)
        {
            return new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, a, b);
        }
    }
}