namespace Fifthweek.Payments.Tests.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PersistCreditRefundTests
    {
        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly UserId UserId = UserId.Random();
        private static readonly DateTime Timestamp = DateTime.UtcNow;
        private static readonly PositiveInt TotalAmount = PositiveInt.Parse(120);
        private static readonly PositiveInt CreditAmount = PositiveInt.Parse(100);
        private static readonly TransactionReference TransactionReference = TransactionReference.Random();
        private static readonly string StripeChargeId = "stripeChargeId";
        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        private static readonly string Comment = "comment";

        private Mock<IGuidCreator> guidCreator;
        private Mock<IPersistCommittedRecordsDbStatement> persistCommittedRecords;

        private PersistCreditRefund target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>(MockBehavior.Strict);
            this.persistCommittedRecords = new Mock<IPersistCommittedRecordsDbStatement>(MockBehavior.Strict);

            this.target = new PersistCreditRefund(
                this.guidCreator.Object,
                this.persistCommittedRecords.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEnactingUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                null,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                null,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTotalAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                null,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreditAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                null,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                null,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeChargeIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                null,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionKeyIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                null,
                Comment);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCommentIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenTotalAmountIsLessThanCreditAmount_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                PositiveInt.Parse(TotalAmount.Value + 1),
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);
        }

        [TestMethod]
        public async Task ItShouldPersistRecords()
        {
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(Guid.NewGuid).Verifiable();

            IReadOnlyList<AppendOnlyLedgerRecord> results = null;
            this.persistCommittedRecords
                .Setup(v => v.ExecuteAsync(It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>()))
                .Callback<IReadOnlyList<AppendOnlyLedgerRecord>>(r => { results = r; })
                .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);

            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);

            Assert.IsTrue(results.All(v => v.TransactionType == LedgerTransactionType.CreditRefund));
            Assert.IsTrue(results.Sum(v => v.Amount) == 0);
        }

        [TestMethod]
        public async Task WhenNoTax_ItShouldPersistRecords()
        {
            this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(Guid.NewGuid).Verifiable();

            IReadOnlyList<AppendOnlyLedgerRecord> results = null;
            this.persistCommittedRecords
                .Setup(v => v.ExecuteAsync(It.IsAny<IReadOnlyList<AppendOnlyLedgerRecord>>()))
                .Callback<IReadOnlyList<AppendOnlyLedgerRecord>>(r => { results = r; })
                .Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(
                EnactingUserId,
                UserId,
                Timestamp,
                TotalAmount,
                TotalAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey,
                Comment);

            Assert.IsNotNull(results);
            Assert.AreEqual(2, results.Count);

            Assert.IsTrue(results.All(v => v.TransactionType == LedgerTransactionType.CreditRefund));
            Assert.IsTrue(results.Sum(v => v.Amount) == 0);
        }
    }
}