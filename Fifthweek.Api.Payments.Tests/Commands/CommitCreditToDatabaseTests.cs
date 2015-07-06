namespace Fifthweek.Api.Payments.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Payments.Commands;
    using Fifthweek.Api.Payments.Taxamo;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CommitCreditToDatabaseTests
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly InitializeCreditRequestResult InitializeResult = new InitializeCreditRequestResult(
            new TaxamoTransactionResult("key", new AmountInUsCents(10), new AmountInUsCents(20), new AmountInUsCents(30), 0.2m, "VAT", "GB", "England"),
            new UserPaymentOriginResult("stripeCustomerId", "GB", "12345", "1.1.1.1", "ttk"));

        private static readonly StripeTransactionResult StripeTransaction =
            new StripeTransactionResult(DateTime.UtcNow, Guid.NewGuid(), "stripeChargeId");

        private Mock<IUpdateAccountBalancesDbStatement> updateAccountBalances;
        private Mock<ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement> setUserPaymentOriginOriginalTaxamoTransactionKey;
        private Mock<ISaveCustomerCreditToLedgerDbStatement> saveCustomerCreditToLedger;

        private CommitCreditToDatabase target;

        [TestInitialize]
        public void Initialize()
        {
            this.updateAccountBalances = new Mock<IUpdateAccountBalancesDbStatement>(MockBehavior.Strict);
            this.setUserPaymentOriginOriginalTaxamoTransactionKey = new Mock<ISetUserPaymentOriginOriginalTaxamoTransactionKeyDbStatement>(MockBehavior.Strict);
            this.saveCustomerCreditToLedger = new Mock<ISaveCustomerCreditToLedgerDbStatement>(MockBehavior.Strict);

            this.target = new CommitCreditToDatabase(
                this.updateAccountBalances.Object,
                this.setUserPaymentOriginOriginalTaxamoTransactionKey.Object,
                this.saveCustomerCreditToLedger.Object);
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

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId, StripeTransaction.Timestamp))
                .Returns(Task.FromResult(0))
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

            this.setUserPaymentOriginOriginalTaxamoTransactionKey.Setup(v => v.ExecuteAsync(
                UserId, 
                InitializeResult.TaxamoTransaction.Key))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.updateAccountBalances.Setup(v => v.ExecuteAsync(UserId, StripeTransaction.Timestamp))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(
                UserId,
                InitializeResult.TaxamoTransaction,
                new UserPaymentOriginResult(
                    InitializeResult.Origin.StripeCustomerId,
                    InitializeResult.Origin.BillingCountryCode,
                    InitializeResult.Origin.CreditCardPrefix,
                    InitializeResult.Origin.IpAddress,
                    null),
                StripeTransaction);

            this.saveCustomerCreditToLedger.Verify();
            this.setUserPaymentOriginOriginalTaxamoTransactionKey.Verify();
            this.updateAccountBalances.Verify();
        }
    }
}