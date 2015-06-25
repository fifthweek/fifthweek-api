namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PersistCommittedAndUncommittedRecordsDbStatementTests : PersistenceTestsBase
    {
        public static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        public static readonly UserId SubscriberId = UserId.Random();
        public static readonly UserId CreatorId = UserId.Random();

        private static readonly List<AppendOnlyLedgerRecord> LedgerRecords = new List<AppendOnlyLedgerRecord>
        {
            new AppendOnlyLedgerRecord(
                Guid.NewGuid(),
                SubscriberId.Value,
                CreatorId.Value,
                Now,
                100,
                LedgerAccountType.Fifthweek,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                "comment", 
                "stripe",
                "taxamo"),
            new AppendOnlyLedgerRecord(
                Guid.NewGuid(),
                SubscriberId.Value,
                null,
                Now,
                -200,
                LedgerAccountType.Stripe,
                Guid.NewGuid(),
                Guid.NewGuid(), 
                "comment",
                "stripe", 
                "taxamo"),
            new AppendOnlyLedgerRecord(
                Guid.NewGuid(),
                Guid.Empty,
                null,
                Now,
                20,
                LedgerAccountType.SalesTax,
                Guid.NewGuid(),
                Guid.NewGuid(),
                "comment",
                "stripe",
                "taxamo")
        };

        private static readonly UncommittedSubscriptionPayment UncommittedRecord = new UncommittedSubscriptionPayment(
            SubscriberId.Value, CreatorId.Value, Now, Now.AddDays(1), 10, Guid.NewGuid());

        private PersistCommittedAndUncommittedRecordsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new PersistCommittedAndUncommittedRecordsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenLedgerRecordsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, UncommittedRecord);
        }

        [TestMethod]
        public async Task ItShouldInsertIfEntriesDoNotExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PersistCommittedAndUncommittedRecordsDbStatement(testDatabase);
                
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(LedgerRecords, UncommittedRecord);

                var sideEffects = new List<IIdentityEquatable>();
                sideEffects.AddRange(LedgerRecords);
                sideEffects.Add(UncommittedRecord);

                return new ExpectedSideEffects
                {
                    Inserts = sideEffects
                };
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateIfUncommittedEntryAlreadyExists()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PersistCommittedAndUncommittedRecordsDbStatement(testDatabase);

                await this.target.ExecuteAsync(
                    LedgerRecords.Select(v => new AppendOnlyLedgerRecord(
                        Guid.NewGuid(),
                        v.AccountOwnerId,
                        v.CounterpartyId,
                        Now.AddDays(-1),
                        v.Amount,
                        v.AccountType,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        null,
                        null, 
                        null)).ToList(),
                    new UncommittedSubscriptionPayment(
                        UncommittedRecord.SubscriberId,
                        UncommittedRecord.CreatorId,
                        UncommittedRecord.StartTimestampInclusive.AddDays(-1),
                        UncommittedRecord.EndTimestampExclusive.AddDays(-1),
                        UncommittedRecord.Amount,
                        Guid.NewGuid()));

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(LedgerRecords, UncommittedRecord);

                var sideEffects = new List<IIdentityEquatable>();
                sideEffects.AddRange(LedgerRecords);
                sideEffects.Add(UncommittedRecord);

                return new ExpectedSideEffects
                {
                    Inserts = LedgerRecords,
                    Update = UncommittedRecord
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotAllowDuplicateLedgerEntries()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PersistCommittedAndUncommittedRecordsDbStatement(testDatabase);

                await this.target.ExecuteAsync(
                    LedgerRecords.Select(v => new AppendOnlyLedgerRecord(
                        Guid.NewGuid(),
                        v.AccountOwnerId,
                        v.CounterpartyId,
                        v.Timestamp,
                        v.Amount,
                        v.AccountType,
                        Guid.NewGuid(),
                        Guid.NewGuid(),
                        null, 
                        null, 
                        null)).ToList(),
                    UncommittedRecord);

                await testDatabase.TakeSnapshotAsync();

                Exception expectedException = null;
                try
                {
                    await this.target.ExecuteAsync(LedgerRecords, UncommittedRecord);
                }
                catch (SqlException t)
                {
                    if (t.Number == 2601)
                    {
                        expectedException = t;
                    }
                }

                Assert.IsNotNull(expectedException);

                return ExpectedSideEffects.TransactionAborted;
            });
        }
    }
}