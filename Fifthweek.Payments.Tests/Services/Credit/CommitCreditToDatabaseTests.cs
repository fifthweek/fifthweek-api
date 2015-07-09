namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Payments.Services.Credit.Taxamo;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CommitCreditToDatabaseTests
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(20), new AmountInUsCents(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk", PaymentStatus.Retry1));

        private static readonly StripeTransactionResult StripeTransaction =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IUpdateAccountBalancesDbStatement> updateAccountBalances;
        private Mock<ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement> setUserPaymentOriginOriginalTaxamoTransactionKey;
        private Mock<ISaveCustomerCreditToLedgerDbStatement> saveCustomerCreditToLedger;
        private Mock<IClearPaymentStatusDbStatement> clearPaymentStatus;

        private CommitCreditToDatabase target;

        [TestInitialize]
        public void Initialize()
        {
            this.updateAccountBalances = new Mock<IUpdateAccountBalancesDbStatement>(MockBehavior.Strict);
            this.setUserPaymentOriginOriginalTaxamoTransactionKey = new Mock<ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement>(MockBehavior.Strict);
            this.saveCustomerCreditToLedger = new Mock<ISaveCustomerCreditToLedgerDbStatement>(MockBehavior.Strict);
            this.clearPaymentStatus = new Mock<IClearPaymentStatusDbStatement>(MockBehavior.Strict);

            this.target = new CommitCreditToDatabase(
                this.updateAccountBalances.Object,
                this.setUserPaymentOriginOriginalTaxamoTransactionKey.Object,
                this.saveCustomerCreditToLedger.Object,
                this.clearPaymentStatus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                null,
                InitializeResult.TaxamoTransaction,
                InitializeResult.Origin,
                StripeTransaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                UserId,
                null,
                InitializeResult.Origin,
                StripeTransaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenOriginIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                null,
                StripeTransaction);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeTransactionResultIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                InitializeResult.Origin,
                null);
        }

        [TestMethod]
        public async Task ItShouldPersistCreditToLedgerAndUpdateAccountBalance()
        {
            this.saveCustomerCreditToLedger.Setup(v => v.ExecuteAsync(
                UserId,
                StripeTransaction.Timestamp,
                InitializeResult.TaxamoTransaction.TotalAmount,
                InitializeResult.TaxamoTransaction.Amount,
                StripeTransaction.TransactionReference,
                StripeTransaction.StripeChargeId,
                InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.clearPaymentStatus.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId, StripeTransaction.Timestamp))
                .ReturnsAsync(new List<CalculatedAccountBalanceResult>())
                .Verifiable();

            await this.target.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                InitializeResult.Origin,
                StripeTransaction);

            this.saveCustomerCreditToLedger.Verify();
            this.updateAccountBalances.Verify();
        }

        [TestMethod]
        public async Task WhenOriginalTaxamoTransactionKeyIsNull_ItShouldPersistNewTransactionKeyBalance()
        {
            this.saveCustomerCreditToLedger.Setup(v => v.ExecuteAsync(
                UserId,
                StripeTransaction.Timestamp,
                InitializeResult.TaxamoTransaction.TotalAmount,
                InitializeResult.TaxamoTransaction.Amount,
                StripeTransaction.TransactionReference,
                StripeTransaction.StripeChargeId,
                InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.clearPaymentStatus.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            this.setUserPaymentOriginOriginalTaxamoTransactionKey.Setup(v => v.ExecuteAsync(
                UserId, 
                InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId, StripeTransaction.Timestamp))
                .ReturnsAsync(new List<CalculatedAccountBalanceResult>())
                .Verifiable();

            await this.target.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                new UserPaymentOriginResult(
                    InitializeResult.Origin.StripeCustomerId,
                    InitializeResult.Origin.CountryCode,
                    InitializeResult.Origin.CreditCardPrefix,
                    InitializeResult.Origin.IpAddress,
                    null,
                    InitializeResult.Origin.PaymentStatus),
                StripeTransaction);

            this.saveCustomerCreditToLedger.Verify();
            this.setUserPaymentOriginOriginalTaxamoTransactionKey.Verify();
            this.updateAccountBalances.Verify();
        }
    }
}