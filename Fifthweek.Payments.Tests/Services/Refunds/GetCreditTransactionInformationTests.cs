namespace Fifthweek.Payments.Tests.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreditTransactionInformationTests
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly UserId UserId = UserId.Random();

        private static readonly string StripeChargeId = Guid.NewGuid().ToString();
        private static readonly string TaxamoTransactionKey = Guid.NewGuid().ToString();

        private Mock<IGetRecordsForTransactionDbStatement> getRecordsForTransaction;

        private GetCreditTransactionInformation target;

        [TestInitialize]
        public void Initialize()
        {
            this.getRecordsForTransaction = new Mock<IGetRecordsForTransactionDbStatement>(MockBehavior.Strict);

            this.target = new GetCreditTransactionInformation(this.getRecordsForTransaction.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEnactingUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldReturnCreditTransactionInformation()
        {
            var data = AddCredit(UserId, 100);
            var transactionReference = new TransactionReference(data.First().TransactionReference);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(data);

            var result = await this.target.ExecuteAsync(transactionReference);

            Assert.AreEqual(
                new GetCreditTransactionInformation.GetCreditTransactionResult(
                    UserId,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    100,
                    100),
                result);
        }

        [TestMethod]
        public async Task WhenPartialRefundHasOccured_ItShouldReturnCreditTransactionInformation()
        {
            var data = AddCredit(UserId, 100);
            var transactionReference = new TransactionReference(data.First().TransactionReference);
            var refundData = AddRefund(UserId, transactionReference.Value, 80);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(data.Concat(refundData).ToList());

            var result = await this.target.ExecuteAsync(transactionReference);

            Assert.AreEqual(
                new GetCreditTransactionInformation.GetCreditTransactionResult(
                    UserId,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    100,
                    20),
                result);
        }

        [TestMethod]
        public async Task WhenCompleteRefundHasOccured_ItShouldReturnCreditTransactionInformation()
        {
            var data = AddCredit(UserId, 100);
            var transactionReference = new TransactionReference(data.First().TransactionReference);
            var refundData = AddRefund(UserId, transactionReference.Value, 100);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(data.Concat(refundData).ToList());

            var result = await this.target.ExecuteAsync(transactionReference);

            Assert.AreEqual(
                new GetCreditTransactionInformation.GetCreditTransactionResult(
                    UserId,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    100,
                    0),
                result);
        }

        [TestMethod]
        public async Task WhenNoRecords_ItShouldReturnNull()
        {
            var transactionReference = TransactionReference.Random();
            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(new List<AppendOnlyLedgerRecord>());

            var result = await this.target.ExecuteAsync(transactionReference);

            Assert.IsNull(result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenNotCreditRecord_ItThrowAnException()
        {
            var transactionReference = TransactionReference.Random();
            var refundData = AddRefund(UserId, transactionReference.Value, 100);

            var record = CreateAppendOnlyLedgerRecord(
                new Random(),
                UserId,
                UserId.Random(),
                -100,
                LedgerAccountType.FifthweekCredit,
                LedgerTransactionType.SubscriptionPayment,
                transactionReference.Value);

            this.getRecordsForTransaction.Setup(v => v.ExecuteAsync(transactionReference)).ReturnsAsync(new List<AppendOnlyLedgerRecord> { record });

            await this.target.ExecuteAsync(transactionReference);
        }

        private static List<AppendOnlyLedgerRecord> AddCredit(UserId userId, decimal amount)
        {
            var random = new Random();
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = Guid.NewGuid();
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, -1.2m * amount, LedgerAccountType.Stripe, LedgerTransactionType.CreditAddition, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.CreditAddition, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, (1.2m * amount) - amount, LedgerAccountType.SalesTax, LedgerTransactionType.CreditAddition, transactionReference));
            return result;
        }

        private static List<AppendOnlyLedgerRecord> AddRefund(UserId userId, Guid transactionReference, decimal amount)
        {
            var random = new Random();
            var result = new List<AppendOnlyLedgerRecord>();
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, 1.2m * amount, LedgerAccountType.Stripe, LedgerTransactionType.CreditRefund, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.CreditRefund, transactionReference));
            result.Add(CreateAppendOnlyLedgerRecord(random, userId, null, -((1.2m * amount) - amount), LedgerAccountType.SalesTax, LedgerTransactionType.CreditRefund, transactionReference));
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
                StripeChargeId,
                TaxamoTransactionKey);

            return record;
        }
    }
}