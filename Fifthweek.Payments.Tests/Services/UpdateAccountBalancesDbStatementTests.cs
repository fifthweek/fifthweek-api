namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateAccountBalancesDbStatementTests : PersistenceTestsBase
    {
        public static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        public static readonly UserId SubscriberId1 = new UserId(new Guid("00000000-0000-0000-0000-000000000001"));
        public static readonly UserId SubscriberId2 = new UserId(new Guid("00000000-0000-0000-0000-000000000002"));
        public static readonly UserId SubscriberId3 = new UserId(new Guid("00000000-0000-0000-0000-000000000003"));
        public static readonly UserId SubscriberId4 = new UserId(new Guid("00000000-0000-0000-0000-000000000004"));
        public static readonly UserId CreatorId1 = new UserId(new Guid("00000000-0000-0000-0001-000000000001"));
        public static readonly UserId CreatorId2 = new UserId(new Guid("00000000-0000-0000-0001-000000000002"));

        private UpdateAccountBalancesDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new UpdateAccountBalancesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenUserIdIsNull_ItShouldUpdateAllModifiedUsers()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountBalancesDbStatement(testDatabase);

                // We have to run it once to exclude for all the seed data that has been inserted.
                await this.target.ExecuteAsync(null, Now.AddSeconds(-0.1));

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var existingFifthweekBalance = await GetExistingFifthweekBalance(testDatabase);

                await this.target.ExecuteAsync(null, Now);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                        {
                            new CalculatedAccountBalance(SubscriberId1.Value, LedgerAccountType.Stripe, Now, -120m),
                            new CalculatedAccountBalance(SubscriberId1.Value, LedgerAccountType.SalesTax, Now, 20m),
                            new CalculatedAccountBalance(SubscriberId1.Value, LedgerAccountType.Fifthweek, Now, 70m),
                            
                            new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.Stripe, Now, -60m),
                            new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.SalesTax, Now, 10m),
                            new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.Fifthweek, Now, 10m),

                            new CalculatedAccountBalance(SubscriberId3.Value, LedgerAccountType.Stripe, Now, -12m),
                            new CalculatedAccountBalance(SubscriberId3.Value, LedgerAccountType.SalesTax, Now, 2m),
                            new CalculatedAccountBalance(SubscriberId3.Value, LedgerAccountType.Fifthweek, Now, 10m),

                            new CalculatedAccountBalance(CreatorId1.Value, LedgerAccountType.Stripe, Now, -6.6m),
                            new CalculatedAccountBalance(CreatorId1.Value, LedgerAccountType.SalesTax, Now, 1.1m),
                            new CalculatedAccountBalance(CreatorId1.Value, LedgerAccountType.Fifthweek, Now, 2.5m),

                            new CalculatedAccountBalance(CreatorId2.Value, LedgerAccountType.Fifthweek, Now, 49m),
                 
                            new CalculatedAccountBalance(Guid.Empty, LedgerAccountType.Fifthweek, Now, existingFifthweekBalance + 24m),
                        }
                };
            });
        }

        [TestMethod]
        public async Task WhenUserIdIsNotNull_ItShouldUpdateSpecifiedUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountBalancesDbStatement(testDatabase);

                // We have to run it once to exclude for all the seed data that has been inserted.
                await this.target.ExecuteAsync(null, Now.AddSeconds(-0.1));

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(SubscriberId2, Now);

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.Stripe, Now, -60m),
                        new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.SalesTax, Now, 10m),
                        new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.Fifthweek, Now, 10m),
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenDataHasNotChanged_ItShouldNotInsertAnyRecords()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountBalancesDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await this.target.ExecuteAsync(null, Now.AddSeconds(-0.1));

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(null, Now);

                return ExpectedSideEffects.None;
            });
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
                            (int)LedgerAccountType.Fifthweek));
            }
            return existingFifthweekBalance;
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var creator1 = CreateUser(databaseContext, random, CreatorId1);
                var creator2 = CreateUser(databaseContext, random, CreatorId2);
                var subscriber1 = CreateUser(databaseContext, random, SubscriberId1);
                var subscriber2 = CreateUser(databaseContext, random, SubscriberId2);
                var subscriber3 = CreateUser(databaseContext, random, SubscriberId3);
                var subscriber4 = CreateUser(databaseContext, random, SubscriberId4);

                // Add credit
                AddCredit(databaseContext, random, SubscriberId1, 100);
                AddCredit(databaseContext, random, SubscriberId2, 40);
                AddCredit(databaseContext, random, SubscriberId2, 10);
                AddCredit(databaseContext, random, SubscriberId3, 10);
                AddCredit(databaseContext, random, CreatorId1, 5.5m);

                PayCreator(databaseContext, random, SubscriberId1, CreatorId1, 10m);
                PayCreator(databaseContext, random, SubscriberId1, CreatorId2, 20m);
                PayCreator(databaseContext, random, SubscriberId2, CreatorId2, 40m);
                PayCreator(databaseContext, random, CreatorId1, CreatorId2, 10m);

                // TODO: add uncommitted records.

                await databaseContext.SaveChangesAsync();
            }
        }

        private static void PayCreator(
            FifthweekDbContext databaseContext,
            Random random,
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount)
        {
            AddAppendOnlyLedgerRecord(databaseContext, random, sourceUserId, destinationUserId, -amount, LedgerAccountType.Fifthweek);
            AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, amount, LedgerAccountType.Fifthweek);
            AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, -0.7m * amount, LedgerAccountType.Fifthweek);
            AddAppendOnlyLedgerRecord(databaseContext, random, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.Fifthweek);
        }

        private static void AddCredit(FifthweekDbContext databaseContext, Random random, UserId userId, decimal amount)
        {
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, -1.2m * amount, LedgerAccountType.Stripe);
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, amount, LedgerAccountType.Fifthweek);
            AddAppendOnlyLedgerRecord(databaseContext, random, userId, null, (1.2m * amount) - amount, LedgerAccountType.SalesTax);
        }

        private static void AddAppendOnlyLedgerRecord(FifthweekDbContext databaseContext, Random random, UserId accountOwnerId, UserId counterpartyId, decimal amount, LedgerAccountType ledgerAccountType)
        {
            databaseContext.AppendOnlyLedgerRecords.Add(
                new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    accountOwnerId == null ? Guid.Empty : accountOwnerId.Value,
                    counterpartyId == null ? (Guid?)null : counterpartyId.Value,
                    Now.AddDays(random.Next(-100, 100)),
                    amount,
                    ledgerAccountType,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null,
                    null,
                    null));
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