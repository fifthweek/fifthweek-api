namespace Fifthweek.Payments.Tests.Services.Refunds
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Transactions;

    using Dapper;

    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Refunds;
    using Fifthweek.Payments.Services.Refunds.Taxamo;
    using Fifthweek.Payments.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RefundIntegrationTests : PersistenceTestsBase
    {
        private const decimal Tax = 1.2m;

        public static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        public static readonly string StripeChargeId = "stripeChargeId";
        public static readonly string TaxamoTransactionKey = "taxamoTransactionKey";
        public static readonly string Comment = "comment";

        public static readonly UserId SubscriberId1 = UserId.Random();
        public static readonly UserId CreatorId1 = UserId.Random();

        private UpdateAccountBalancesDbStatement updateAccountBalances;
        private GetAccountSettingsDbStatement getAccountSettings;
        private GetCreatorRevenueDbStatement getCreatorRevenue;
        private PersistCreditRefund persistCreditRefund;
        private CreateTransactionRefund createTransactionRefund;
        private GetCreditTransactionInformation getCreditTransactionInformation;
        private GetCalculatedAccountBalancesDbStatement getCalculatedAccountBalances;

        private async Task IntegrationTestAsync(Func<TestDatabaseContext, Task<ExpectedSideEffects>> databaseTest)
        {
            var database = await TestDatabase.CreateNewAsync();
            var databaseSnapshot = new TestDatabaseSnapshot(database);
            var databaseContext = new TestDatabaseContext(database, databaseSnapshot);

            var transactionOptions = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadUncommitted,
                Timeout = TransactionManager.MaximumTimeout
            };

            var ts = new TransactionScope(
                TransactionScopeOption.Required,
                transactionOptions,
                TransactionScopeAsyncFlowOption.Enabled);

            using (ts)
            {
                var sideEffects = await databaseTest(databaseContext);
                await databaseSnapshot.AssertSideEffectsAsync(sideEffects);
            }
        }

        [TestMethod]
        public async Task ItShouldBeAbleToRefundCredit()
        {
            await this.IntegrationTestAsync(async testDatabase =>
            {
                this.InitializeServices(testDatabase);

                var random = new Random();

                TransactionReference transactionReference;
                using (var databaseContext = testDatabase.CreateContext())
                {
                    //var creator1 = CreateUser(databaseContext, random, CreatorId1);
                    var subscriber1 = CreateUser(databaseContext, random, SubscriberId1);

                    transactionReference = AddCredit(databaseContext, random, SubscriberId1, 1000);
                    //PayCreator(databaseContext, random, SubscriberId1, CreatorId1, 10m);
                    //PayCreatorUncommitted(databaseContext, random, SubscriberId2, CreatorId2, 5m);
                    await databaseContext.SaveChangesAsync();
                }

                await this.updateAccountBalances.ExecuteAsync(SubscriberId1, DateTime.UtcNow);
                Assert.AreEqual(1000, (await this.getCreditTransactionInformation.ExecuteAsync(transactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(1000, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(-1200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                await this.persistCreditRefund.ExecuteAsync(
                    UserId.Random(),
                    SubscriberId1,
                    DateTime.UtcNow,
                    PositiveInt.Parse((int)(100 * Tax)),
                    PositiveInt.Parse(100),
                    transactionReference,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    Comment);

                await this.updateAccountBalances.ExecuteAsync(SubscriberId1, DateTime.UtcNow);
                Assert.AreEqual(900, (await this.getCreditTransactionInformation.ExecuteAsync(transactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(900, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(180, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(-1080, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                await this.persistCreditRefund.ExecuteAsync(
                    UserId.Random(),
                    SubscriberId1,
                    DateTime.UtcNow,
                    PositiveInt.Parse((int)(500 * Tax)),
                    PositiveInt.Parse(500),
                    transactionReference,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    Comment);

                await this.updateAccountBalances.ExecuteAsync(SubscriberId1, DateTime.UtcNow);
                Assert.AreEqual(400, (await this.getCreditTransactionInformation.ExecuteAsync(transactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(400, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(80, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(-480, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                await this.persistCreditRefund.ExecuteAsync(
                    UserId.Random(),
                    SubscriberId1,
                    DateTime.UtcNow,
                    PositiveInt.Parse((int)(400 * Tax)),
                    PositiveInt.Parse(400),
                    transactionReference,
                    StripeChargeId,
                    TaxamoTransactionKey,
                    Comment);

                await this.updateAccountBalances.ExecuteAsync(SubscriberId1, DateTime.UtcNow);
                Assert.AreEqual(0, (await this.getCreditTransactionInformation.ExecuteAsync(transactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(0, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(0, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(0, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                await testDatabase.TakeSnapshotAsync();
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldBeAbleToRefundSubscriptions()
        {
            await this.IntegrationTestAsync(async testDatabase =>
            {
                this.InitializeServices(testDatabase);

                var random = new Random();

                await this.updateAccountBalances.ExecuteAsync(null, DateTime.UtcNow);
                decimal initialFifthweekRevenue = await this.GetAccountBalance(null, LedgerAccountType.FifthweekRevenue);

                TransactionReference creditTransactionReference;
                TransactionReference transactionReference;
                using (var databaseContext = testDatabase.CreateContext())
                {
                    CreateUser(databaseContext, random, CreatorId1);
                    CreateUser(databaseContext, random, SubscriberId1);

                    creditTransactionReference = AddCredit(databaseContext, random, SubscriberId1, 1000);
                    transactionReference = PayCreator(databaseContext, random, SubscriberId1, CreatorId1, 500m);
                    PayCreatorUncommitted(databaseContext, random, SubscriberId1, CreatorId1, 500m);

                    await databaseContext.SaveChangesAsync();
                }

                await this.updateAccountBalances.ExecuteAsync(null, DateTime.UtcNow);
                Assert.AreEqual(1000, (await this.getCreditTransactionInformation.ExecuteAsync(creditTransactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(0, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(-1200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                Assert.AreEqual(0, (await this.getAccountSettings.ExecuteAsync(CreatorId1)).AccountBalance);
                Assert.AreEqual(350, await this.GetAccountBalance(CreatorId1, LedgerAccountType.FifthweekRevenue));
                Assert.AreEqual(350, (await this.getCreatorRevenue.ExecuteAsync(CreatorId1, Now)).UnreleasedRevenue);

                Assert.AreEqual(initialFifthweekRevenue + 150, await this.GetAccountBalance(null, LedgerAccountType.FifthweekRevenue));

                var result = await this.createTransactionRefund.ExecuteAsync(UserId.Random(), transactionReference, DateTime.UtcNow, "something");

                Assert.AreEqual(CreatorId1, result.CreatorId);
                Assert.AreEqual(SubscriberId1, result.SubscriberId);

                await this.updateAccountBalances.ExecuteAsync(null, DateTime.UtcNow);
                Assert.AreEqual(1000, (await this.getCreditTransactionInformation.ExecuteAsync(creditTransactionReference)).CreditAmountAvailableForRefund);
                Assert.AreEqual(500, (await this.getAccountSettings.ExecuteAsync(SubscriberId1)).AccountBalance);
                Assert.AreEqual(200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.SalesTax));
                Assert.AreEqual(-1200, await this.GetAccountBalance(SubscriberId1, LedgerAccountType.Stripe));

                Assert.AreEqual(0, (await this.getAccountSettings.ExecuteAsync(CreatorId1)).AccountBalance);
                Assert.AreEqual(0, await this.GetAccountBalance(CreatorId1, LedgerAccountType.FifthweekRevenue));
                Assert.AreEqual(0, (await this.getCreatorRevenue.ExecuteAsync(CreatorId1, Now)).UnreleasedRevenue);

                Assert.AreEqual(initialFifthweekRevenue + 0, await this.GetAccountBalance(null, LedgerAccountType.FifthweekRevenue));

                await testDatabase.TakeSnapshotAsync();
                return ExpectedSideEffects.None;
            });
        }

        private async Task<decimal> GetAccountBalance(UserId userId, LedgerAccountType accountType)
        {
            var results = await this.getCalculatedAccountBalances.ExecuteAsync(
                userId, accountType, SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value);

            return results.OrderByDescending(v => v.Timestamp).First().Amount;
        }

        private void InitializeServices(TestDatabaseContext testDatabase)
        {
            this.updateAccountBalances = new UpdateAccountBalancesDbStatement(testDatabase);
            this.getAccountSettings = new GetAccountSettingsDbStatement(testDatabase);
            this.getCreatorRevenue = new GetCreatorRevenueDbStatement(testDatabase);

            this.persistCreditRefund = new PersistCreditRefund(
                new GuidCreator(),
                new PersistCommittedRecordsDbStatement(testDatabase));

            this.createTransactionRefund = new CreateTransactionRefund(
                new GuidCreator(),
                new GetRecordsForTransactionDbStatement(testDatabase),
                new PersistCommittedRecordsDbStatement(testDatabase));

            this.getCreditTransactionInformation = new GetCreditTransactionInformation(
                new GetRecordsForTransactionDbStatement(testDatabase));

            this.getCalculatedAccountBalances = new GetCalculatedAccountBalancesDbStatement(testDatabase);
        }

        private static async Task<decimal> GetExistingFifthweekBalance(TestDatabaseContext testDatabase)
        {
            decimal existingFifthweekBalance;
            using (var connection = testDatabase.CreateConnection())
            {
                existingFifthweekBalance =
                    await
                    connection.ExecuteScalarAsync<decimal>(
                        string.Format(
                            "SELECT TOP 1 {0} FROM {1} WHERE {2}='{5}' AND {3}={6} ORDER BY {4} DESC",
                            CalculatedAccountBalance.Fields.Amount,
                            CalculatedAccountBalance.Table,
                            CalculatedAccountBalance.Fields.UserId,
                            CalculatedAccountBalance.Fields.AccountType,
                            CalculatedAccountBalance.Fields.Timestamp,
                            Guid.Empty.ToString(),
                            (int)LedgerAccountType.FifthweekRevenue));
            }

            return existingFifthweekBalance;
        }

        private static TransactionReference PayCreator(
            FifthweekDbContext databaseContext,
            Random random,
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount)
        {
            var transactionReference = TransactionReference.Random();
            AddAppendOnlyLedgerRecord(databaseContext, random, sourceUserId, destinationUserId, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionReference);
            AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference);
            AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, -0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference);
            AddAppendOnlyLedgerRecord(databaseContext, random, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference);
            return transactionReference;
        }

        private static void PayCreatorUncommitted(
            FifthweekDbContext databaseContext,
            Random random,
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount)
        {
            AddUncommittedRecord(databaseContext, random, sourceUserId, destinationUserId, amount);
        }

        private static TransactionReference AddCredit(FifthweekDbContext databaseContext, Random random, UserId userId, decimal amount)
        {
            var transactionReference = TransactionReference.Random();
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, -1.2m * amount, LedgerAccountType.Stripe, LedgerTransactionType.CreditAddition, transactionReference);
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.CreditAddition, transactionReference);
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, (1.2m * amount) - amount, LedgerAccountType.SalesTax, LedgerTransactionType.CreditAddition, transactionReference);
            return transactionReference;
        }

        private static void AddAppendOnlyLedgerRecord(
            FifthweekDbContext databaseContext, Random random, UserId accountOwnerId, UserId counterpartyId, decimal amount, LedgerAccountType ledgerAccountType, LedgerTransactionType transactionType, TransactionReference transactionReference)
        {
            databaseContext.AppendOnlyLedgerRecords.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    accountOwnerId == null ? Guid.Empty : accountOwnerId.Value,
                    counterpartyId == null ? (Guid?)null : counterpartyId.Value,
                    Now.AddDays(random.Next(-100, 100)),
                    amount,
                    ledgerAccountType,
                    transactionType,
                    transactionReference.Value,
                    Guid.NewGuid(),
                    StripeChargeId,
                    TaxamoTransactionKey,
                    Comment));
        }

        private static void AddUncommittedRecord(
            FifthweekDbContext databaseContext,
            Random random,
            UserId subscriberId,
            UserId creatorId,
            decimal amount)
        {
            databaseContext.UncommittedSubscriptionPayments.Add(
                new UncommittedSubscriptionPayment(
                    subscriberId.Value,
                    creatorId.Value,
                    Now.AddDays(random.Next(-100, 100)),
                    Now.AddDays(random.Next(-100, 100)),
                    amount,
                    Guid.NewGuid()));
        }

        private static FifthweekUser CreateUser(FifthweekDbContext databaseContext, Random random, UserId userId)
        {
            var user = UserTests.UniqueEntity(random);
            user.Id = userId.Value;
            databaseContext.Users.Add(user);
            return user;
        }
    }
}