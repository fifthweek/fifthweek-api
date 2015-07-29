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
    public class PersistCommittedRecordsDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();

        private static readonly UserId EnactingUserId = UserId.Random();
        private static readonly string Comment = "comment";

        private PersistCommittedRecordsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new PersistCommittedRecordsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRecordsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldSaveTheSpecifiedRecords()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PersistCommittedRecordsDbStatement(testDatabase);

                var data = await this.CreateDataAsync(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(data);

                return new ExpectedSideEffects
                {
                    Inserts = data
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotSaveAnyRecordsIfListIsEmpty()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PersistCommittedRecordsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new List<AppendOnlyLedgerRecord>());

                return ExpectedSideEffects.None;
            });
        }

        private async Task<List<AppendOnlyLedgerRecord>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();

                CreateUser(databaseContext, random, SubscriberId);
                CreateUser(databaseContext, random, CreatorId);

                PayCreator(databaseContext, random, SubscriberId, CreatorId, 20m);
                var result = PayCreator(databaseContext, random, SubscriberId, CreatorId, 10m, false);

                await databaseContext.SaveChangesAsync();

                return result;
            }
        }

        private static FifthweekUser CreateUser(FifthweekDbContext databaseContext, Random random, UserId userId)
        {
            var user = UserTests.UniqueEntity(random);
            user.Id = userId.Value;
            databaseContext.Users.Add(user);
            return user;
        }

        private static List<AppendOnlyLedgerRecord> PayCreator(
            FifthweekDbContext databaseContext,
            Random random,
            UserId sourceUserId,
            UserId destinationUserId,
            decimal amount,
            bool save = true)
        {
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = Guid.NewGuid();
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, sourceUserId, destinationUserId, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionReference, save));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference, save));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, -0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference, save));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference, save));
            return result;
        }

        private static AppendOnlyLedgerRecord AddAppendOnlyLedgerRecord(FifthweekDbContext databaseContext, Random random, UserId accountOwnerId, UserId counterpartyId, decimal amount, LedgerAccountType ledgerAccountType, LedgerTransactionType transactionType, Guid transactionReference, bool save)
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

            if (save)
            {
                databaseContext.AppendOnlyLedgerRecords.Add(record);
            }

            return record;
        }

        private Guid CreateGuid(byte a, byte b)
        {
            return new Guid(0, 0, 0, 0, 0, 0, 0, 0, 0, a, b);
        }
    }
}