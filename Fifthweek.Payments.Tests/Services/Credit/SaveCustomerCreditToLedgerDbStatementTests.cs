namespace Fifthweek.Payments.Tests.Services.Credit
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services.Credit;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SaveCustomerCreditToLedgerDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly DateTime Timestamp = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly AmountInUsCents TotalAmount = new AmountInUsCents(12);
        private static readonly AmountInUsCents CreditAmount = new AmountInUsCents(10);
        private static readonly AmountInUsCents TaxAmount = new AmountInUsCents(2);
        private static readonly Guid TransactionReference = Guid.NewGuid();
        private static readonly string StripeChargeId = "stripeChargeId";
        private static readonly string TaxamoTransactionKey = "taxamoTransactionKey";

        private Mock<IGuidCreator> guidCreator;

        private SaveCustomerCreditToLedgerDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.guidCreator = new Mock<IGuidCreator>(MockBehavior.Strict);

            this.target = new SaveCustomerCreditToLedgerDbStatement(
                this.guidCreator.Object,
                new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                null,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTotalAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                null,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCreditAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                TotalAmount,
                null,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenStripeChargeIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                null,
                TaxamoTransactionKey);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTaxamoTransactionIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                TotalAmount,
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                null);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenTotalAmountIsLessThanCreditAmountIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                UserId,
                Timestamp,
                new AmountInUsCents(CreditAmount.Value - 1),
                CreditAmount,
                TransactionReference,
                StripeChargeId,
                TaxamoTransactionKey);
        }

        [TestMethod]
        public async Task WhenTotalAmountEqualsCreditAmount_ItShouldStoreLedgerRecords()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                byte sequentialGuidIndex = 0;
                this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(sequentialGuidIndex++));

                this.target = new SaveCustomerCreditToLedgerDbStatement(this.guidCreator.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    Timestamp,
                    CreditAmount,
                    CreditAmount,
                    TransactionReference,
                    StripeChargeId,
                    TaxamoTransactionKey);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        new AppendOnlyLedgerRecord(
                            this.CreateGuid(0),
                            UserId.Value,
                            null,
                            Timestamp,
                            -CreditAmount.Value,
                            LedgerAccountType.Stripe,
                            LedgerTransactionType.CreditAddition,
                            TransactionReference,
                            null,
                            null,
                            StripeChargeId,
                            TaxamoTransactionKey),
                        new AppendOnlyLedgerRecord(
                            this.CreateGuid(1),
                            UserId.Value,
                            null,
                            Timestamp,
                            CreditAmount.Value,
                            LedgerAccountType.FifthweekCredit,
                            LedgerTransactionType.CreditAddition,
                            TransactionReference,
                            null,
                            null,
                            StripeChargeId,
                            TaxamoTransactionKey),
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenTotalAmountIsGreaterCreditAmount_ItShouldStoreLedgerRecords()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                byte sequentialGuidIndex = 0;
                this.guidCreator.Setup(v => v.CreateSqlSequential()).Returns(() => this.CreateGuid(sequentialGuidIndex++));

                this.target = new SaveCustomerCreditToLedgerDbStatement(this.guidCreator.Object, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    Timestamp,
                    TotalAmount,
                    CreditAmount,
                    TransactionReference,
                    StripeChargeId,
                    TaxamoTransactionKey);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        new AppendOnlyLedgerRecord(
                            this.CreateGuid(0),
                            UserId.Value,
                            null,
                            Timestamp,
                            -TotalAmount.Value,
                            LedgerAccountType.Stripe,
                            LedgerTransactionType.CreditAddition,
                            TransactionReference,
                            null,
                            null,
                            StripeChargeId,
                            TaxamoTransactionKey),
                        new AppendOnlyLedgerRecord(
                            this.CreateGuid(1),
                            UserId.Value,
                            null,
                            Timestamp,
                            CreditAmount.Value,
                            LedgerAccountType.FifthweekCredit,
                            LedgerTransactionType.CreditAddition,
                            TransactionReference,
                            null,
                            null,
                            StripeChargeId,
                            TaxamoTransactionKey),
                        new AppendOnlyLedgerRecord(
                            this.CreateGuid(2),
                            UserId.Value,
                            null,
                            Timestamp,
                            TaxAmount.Value,
                            LedgerAccountType.SalesTax,
                            LedgerTransactionType.CreditAddition,
                            TransactionReference,
                            null,
                            null,
                            StripeChargeId,
                            TaxamoTransactionKey),
                    }
                };
            });
        }

        private Guid CreateGuid(byte a)
        {
            return new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, a);
        }
    }
}