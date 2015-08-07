namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorRevenueDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId UserId1 = UserId.Random();
        private static readonly UserId UserId2 = UserId.Random();

        private GetCreatorRevenueDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetCreatorRevenueDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Now);
        }

        [TestMethod]
        public async Task ItShouldReturnTotalRevenueForUser1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorRevenueDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId1, Now.AddDays(5));

                Assert.AreEqual(
                    new GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult(
                        Days * 2,
                        Days * 4,
                        (Days * 2) - 5), 
                        result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnTotalRevenueForUser2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorRevenueDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId2, Now.AddDays(5));

                Assert.AreEqual(
                    new GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult(
                        Days * 3,
                        Days * 5,
                        (Days * 3) - 10), result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnZeroTotalRevenueForUnknownUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorRevenueDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId.Random(), Now.AddDays(5));

                Assert.AreEqual(new GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult(
                    0,
                    0,
                    0), 
                    result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var snapshots = new List<CalculatedAccountBalance>();
                var ledgerRecords = new List<AppendOnlyLedgerRecord>();

                for (int i = 0; i < Days; i++)
                {
                    snapshots.Add(new CalculatedAccountBalance(UserId1.Value, LedgerAccountType.FifthweekRevenue, Now.AddDays(i), (i + 1) * 2));
                    snapshots.Add(new CalculatedAccountBalance(UserId1.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(i), i));
                    snapshots.Add(new CalculatedAccountBalance(UserId1.Value, LedgerAccountType.Stripe, Now.AddDays(i), i + 0.25m));
                    snapshots.Add(new CalculatedAccountBalance(UserId1.Value, LedgerAccountType.ReleasedRevenue, Now.AddDays(i), (i + 1) * 4));
                    ledgerRecords.Add(
                        new AppendOnlyLedgerRecord(
                            Guid.NewGuid(),
                            UserId1.Value,
                            null,
                            Now.AddDays(i),
                            1,
                            LedgerAccountType.FifthweekRevenue,
                            LedgerTransactionType.SubscriptionPayment,
                            Guid.NewGuid(),
                            null,
                            null,
                            null,
                            null));

                    snapshots.Add(new CalculatedAccountBalance(UserId2.Value, LedgerAccountType.FifthweekRevenue, Now.AddDays(i), (i + 1) * 3));
                    snapshots.Add(new CalculatedAccountBalance(UserId2.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(i).AddHours(12), i + 0.5m));
                    snapshots.Add(new CalculatedAccountBalance(UserId2.Value, LedgerAccountType.Stripe, Now.AddDays(i).AddHours(12), i + 0.75m));
                    snapshots.Add(new CalculatedAccountBalance(UserId2.Value, LedgerAccountType.ReleasedRevenue, Now.AddDays(i), (i + 1) * 5));
                    ledgerRecords.Add(
                        new AppendOnlyLedgerRecord(
                            Guid.NewGuid(),
                            UserId2.Value,
                            null,
                            Now.AddDays(i),
                            2,
                            LedgerAccountType.FifthweekRevenue,
                            LedgerTransactionType.SubscriptionPayment,
                            Guid.NewGuid(),
                            null,
                            null,
                            null,
                            null));
                }

                await connection.InsertAsync(snapshots, false);
                await connection.InsertAsync(ledgerRecords, false);
                return snapshots.Select(v => new UserId(v.UserId)).ToList();
            }
        }
    }
}