namespace Fifthweek.Payments.Tests.Services.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.Services.Administration;
    using Fifthweek.Payments.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetTransactionsDbStatementTests : PersistenceTestsBase
    {
        public static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        public static readonly UserId SubscriberId1 = new UserId(new Guid("00000000-0000-0000-0000-000000000001"));
        public static readonly UserId SubscriberId2 = new UserId(new Guid("00000000-0000-0000-0000-000000000002"));
        public static readonly UserId SubscriberId3 = new UserId(new Guid("00000000-0000-0000-0000-000000000003"));
        public static readonly UserId SubscriberId4 = new UserId(new Guid("00000000-0000-0000-0000-000000000004"));
        public static readonly UserId CreatorId1 = new UserId(new Guid("00000000-0000-0000-0001-000000000001"));
        public static readonly UserId CreatorId2 = new UserId(new Guid("00000000-0000-0000-0001-000000000002"));

        private GetTransactionsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetTransactionsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenUserIdIsNull_ItShouldReturnAllTransactions()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(null, SqlDateTime.MinValue.Value, SqlDateTime.MaxValue.Value, data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenDateIsSpecified_ItShouldReturnAllTransactionsInDate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(null, Now.AddDays(-15), Now, data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserAndDateIsSpecified1_ItShouldReturnAllTransactionsInDateInvolvingUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(SubscriberId1, Now.AddDays(-15), Now, data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserAndDateIsSpecified2_ItShouldReturnAllTransactionsInDateInvolvingUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(SubscriberId1, Now.AddDays(-40), Now.AddDays(-15), data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserAndDateIsSpecified3_ItShouldReturnAllTransactionsInDateInvolvingUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(CreatorId1, Now.AddDays(-15), Now, data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserAndDateIsSpecified4_ItShouldReturnAllTransactionsInDateInvolvingUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(CreatorId1, Now.AddDays(-40), Now.AddDays(-15), data);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserAndDateIsSpecified5_ItShouldReturnAllTransactionsInDateInvolvingUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var data = await this.SetupTest(testDatabase);
                await this.PerformTest(new UserId(Guid.Empty), Now.AddDays(-40), Now.AddDays(-15), data);
                return ExpectedSideEffects.None;
            });
        }

        private async Task<GetTransactionsResult> SetupTest(TestDatabaseContext testDatabase)
        {
            this.target = new GetTransactionsDbStatement(testDatabase);

            await ClearLedger(testDatabase);

            var data = await this.CreateDataAsync(testDatabase);
            await testDatabase.TakeSnapshotAsync();
            return data;
        }

        private async Task PerformTest(UserId userId, DateTime startDate, DateTime endDate, GetTransactionsResult data)
        {
            var result = await this.target.ExecuteAsync(userId, startDate, endDate);

            if (userId != null)
            {
                var recordsInvolvingUser =
                    data.Records.Where(
                        v => userId.Equals(v.AccountOwnerId) || userId.Equals(v.CounterpartyId));

                var transactionsInvolvingUser =
                    recordsInvolvingUser.Select(v => v.TransactionReference).Distinct().ToList();

                var expectedRecords =
                    data.Records.Where(
                        v =>
                        v.Timestamp >= startDate && v.Timestamp < endDate
                        && transactionsInvolvingUser.Contains(v.TransactionReference)).ToList();

                CollectionAssert.AreEquivalent(
                    expectedRecords,
                    result.Records.ToList());
            }
            else
            {
                CollectionAssert.AreEquivalent(
                    data.Records.Where(v => v.Timestamp >= startDate && v.Timestamp < endDate).ToList(),
                    result.Records.ToList());
            }
        }

        private static async Task ClearLedger(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                await connection.ExecuteAsync(string.Format("DELETE FROM {0}", AppendOnlyLedgerRecord.Table));
            }
        }

        private async Task<GetTransactionsResult> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var users = new List<FifthweekUser>();
                users.Add(CreateUser(databaseContext, random, CreatorId1));
                users.Add(CreateUser(databaseContext, random, CreatorId2));
                users.Add(CreateUser(databaseContext, random, SubscriberId1));
                users.Add(CreateUser(databaseContext, random, SubscriberId2));
                users.Add(CreateUser(databaseContext, random, SubscriberId3));
                users.Add(CreateUser(databaseContext, random, SubscriberId4));

                var records = new List<AppendOnlyLedgerRecord>();

                // Add credit
                records.AddRange(AddCredit(databaseContext, Now.AddDays(-30), SubscriberId1, 100));
                records.AddRange(AddCredit(databaseContext, Now.AddDays(-30), SubscriberId2, 40));
                records.AddRange(AddCredit(databaseContext, Now.AddDays(-30), SubscriberId2, 10));
                records.AddRange(AddCredit(databaseContext, Now.AddDays(-10), SubscriberId3, 10));
                records.AddRange(AddCredit(databaseContext, Now.AddDays(-10), CreatorId1, 5.5m));

                records.AddRange(PayCreator(databaseContext, Now.AddDays(-30), SubscriberId1, CreatorId1, 10m));
                records.AddRange(PayCreator(databaseContext, Now.AddDays(-30), SubscriberId1, CreatorId2, 20m));
                records.AddRange(PayCreator(databaseContext, Now.AddDays(-10), SubscriberId2, CreatorId2, 40m));
                records.AddRange(PayCreator(databaseContext, Now.AddDays(-10), CreatorId1, CreatorId2, 10m));

                PayCreatorUncommitted(databaseContext, random, SubscriberId2, CreatorId2, 5m);

                await databaseContext.SaveChangesAsync();

                var results = new List<GetTransactionsResult.Item>();
                foreach (var item in records)
                {
                    var accountOwnerUsername = item.AccountOwnerId == Guid.Empty ? null : users.First(v => item.AccountOwnerId == v.Id).UserName;
                    var counterpartyUsername = item.CounterpartyId == Guid.Empty || item.CounterpartyId == null ? null : users.First(v => item.CounterpartyId == v.Id).UserName;

                    results.Add(
                        new GetTransactionsResult.Item(
                            item.Id,
                            new UserId(item.AccountOwnerId),
                            accountOwnerUsername == null ? null : new Username(accountOwnerUsername),
                            item.CounterpartyId == null ? null : new UserId(item.CounterpartyId.Value),
                            counterpartyUsername == null ? null : new Username(counterpartyUsername),
                            item.Timestamp,
                            item.Amount,
                            item.AccountType,
                            item.TransactionType,
                            new TransactionReference(item.TransactionReference),
                            item.InputDataReference,
                            item.Comment,
                            item.StripeChargeId,
                            item.TaxamoTransactionKey));
                }

                return new GetTransactionsResult(results);
            }
        }

        private static List<AppendOnlyLedgerRecord> PayCreator(
            FifthweekDbContext databaseContext,
            DateTime timestamp,
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount)
        {
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = TransactionReference.Random();
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, sourceUserId, destinationUserId, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, null, destinationUserId, amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, null, destinationUserId, -0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            return result;
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

        private static List<AppendOnlyLedgerRecord> AddCredit(FifthweekDbContext databaseContext, DateTime timestamp, UserId userId, decimal amount)
        {
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = TransactionReference.Random();
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, userId, null, -1.2m * amount, LedgerAccountType.Stripe, LedgerTransactionType.CreditAddition, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, userId, null, amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.CreditAddition, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, timestamp, userId, null, (1.2m * amount) - amount, LedgerAccountType.SalesTax, LedgerTransactionType.CreditAddition, transactionReference));
            return result;
        }

        private static AppendOnlyLedgerRecord AddAppendOnlyLedgerRecord(
            FifthweekDbContext databaseContext, DateTime timestamp, UserId accountOwnerId, UserId counterpartyId, decimal amount, 
            LedgerAccountType ledgerAccountType, LedgerTransactionType transactionType, TransactionReference transactionReference)
        {
            var item = new AppendOnlyLedgerRecord(
                Guid.NewGuid(),
                accountOwnerId == null ? Guid.Empty : accountOwnerId.Value,
                counterpartyId == null ? (Guid?)null : counterpartyId.Value,
                timestamp,
                amount,
                ledgerAccountType,
                transactionType,
                transactionReference.Value,
                Guid.NewGuid(),
                null,
                null,
                null);
            databaseContext.AppendOnlyLedgerRecords.Add(item);

            return item;
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

        private static List<CalculatedAccountBalanceResult> ToExpectedResult(IEnumerable<CalculatedAccountBalance> inserts)
        {
            return inserts.Select(v => new CalculatedAccountBalanceResult(
                v.Timestamp,
                new UserId(v.UserId),
                v.AccountType,
                v.Amount)).ToList();
        }
    }
}