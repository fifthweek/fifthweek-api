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
    public class GetRecordsForTransactionDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly UserId SubscriberId = UserId.Random();
        private static readonly UserId CreatorId = UserId.Random();

        private GetRecordsForTransactionDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetRecordsForTransactionDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenTransactionReferenceIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldReturnRecordsForTransaction()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetRecordsForTransactionDbStatement(testDatabase);

                var data = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var transactionReference = data.First().TransactionReference;

                var result = await this.target.ExecuteAsync(new TransactionReference(transactionReference));

                CollectionAssert.AreEquivalent(
                    data,
                    result.ToList());

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
                var result = PayCreator(databaseContext, random, SubscriberId, CreatorId, 10m);

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
            decimal amount)
        {
            var result = new List<AppendOnlyLedgerRecord>();
            var transactionReference = Guid.NewGuid();
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, sourceUserId, destinationUserId, -amount, LedgerAccountType.FifthweekCredit, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, null, destinationUserId, -0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            result.Add(AddAppendOnlyLedgerRecord(databaseContext, random, destinationUserId, destinationUserId, 0.7m * amount, LedgerAccountType.FifthweekRevenue, LedgerTransactionType.SubscriptionPayment, transactionReference));
            return result;
        }

        private static AppendOnlyLedgerRecord AddAppendOnlyLedgerRecord(FifthweekDbContext databaseContext, Random random, UserId accountOwnerId, UserId counterpartyId, decimal amount, LedgerAccountType ledgerAccountType, LedgerTransactionType transactionType, Guid transactionReference)
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

            databaseContext.AppendOnlyLedgerRecords.Add(record);

            return record;
        }
    }
}