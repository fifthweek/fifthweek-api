namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetLatestCommittedLedgerDateDbStatementTests : PersistenceTestsBase
    {
        public static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        public static readonly UserId SubscriberId = UserId.Random();
        public static readonly UserId CreatorId = UserId.Random();

        private GetLatestCommittedLedgerDateDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetLatestCommittedLedgerDateDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnTimestampIfLedgerEntryFound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLatestCommittedLedgerDateDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId, CreatorId);

                Assert.AreEqual(Now.AddDays(1), result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNullIfLedgerEntryNotFound()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLatestCommittedLedgerDateDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId, SubscriberId);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var ledgerEntry = new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    SubscriberId.Value,
                    CreatorId.Value,
                    Now,
                    10,
                    LedgerAccountType.FifthweekCredit,
                    LedgerTransactionType.SubscriptionPayment,
                    Guid.NewGuid(),
                    Guid.NewGuid(),
                    null,
                    null,
                    null);

                var ledgerEntry2 = new AppendOnlyLedgerRecord(
                    Guid.NewGuid(),
                    SubscriberId.Value,
                    CreatorId.Value,
                    Now.AddDays(1),
                    10,
                    LedgerAccountType.FifthweekCredit,
                    LedgerTransactionType.SubscriptionPayment,
                    Guid.NewGuid(),
                    Guid.NewGuid(), 
                    null,
                    null, 
                    null);

                await connection.InsertAsync(ledgerEntry, false);
                await connection.InsertAsync(ledgerEntry2, false);
            }
        }
    }
}