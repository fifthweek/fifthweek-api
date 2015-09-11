namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllCreatorRevenuesDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId UserId1 = UserId.Random();
        private static readonly UserId UserId2 = UserId.Random();
        private static readonly UserId UserId3 = UserId.Random();

        private static readonly Username Username1 = new Username("Username");
        private static readonly Email Email1 = new Email("a@b.com");
        private static readonly bool EmailConfirmed1 = true;

        private GetAllCreatorRevenuesDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetAllCreatorRevenuesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnRevenueForAllUsers()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllCreatorRevenuesDbStatement(testDatabase);

                var initialResult = await this.target.ExecuteAsync(Now.AddDays(5));

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Now.AddDays(5));

                CollectionAssert.AreEquivalent(
                    new List<GetAllCreatorRevenuesResult.Creator>
                    {
                        new GetAllCreatorRevenuesResult.Creator(
                            UserId1,
                            Days * 2,
                            Days * 4,
                            (Days * 2) - 5,
                            Username1,
                            Email1,
                            EmailConfirmed1),
                        new GetAllCreatorRevenuesResult.Creator(
                            UserId2,
                            Days * 3,
                            Days * 5,
                            (Days * 3) - 10,
                            null,
                            null,
                            false),
                        new GetAllCreatorRevenuesResult.Creator(
                            UserId3,
                            100,
                            0,
                            100,
                            null,
                            null,
                            false)
                    },
                    result.Creators.Except(initialResult.Creators).ToList());

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNoResultsWhenNoUsersWithRevenue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllCreatorRevenuesDbStatement(testDatabase);

                var initialResult = await this.target.ExecuteAsync(Now.AddDays(5));

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Now.AddDays(5));

                Assert.AreEqual(initialResult.Creators.Count, result.Creators.Count);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var context = testDatabase.CreateContext())
            {
                var random = new Random();
                var user = UserTests.UniqueEntity(random);
                user.Id = UserId1.Value;
                user.UserName = Username1.Value;
                user.Email = Email1.Value;
                user.EmailConfirmed = EmailConfirmed1;
                context.Users.Add(user);
                await context.SaveChangesAsync();
            }

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

                snapshots.Add(new CalculatedAccountBalance(UserId3.Value, LedgerAccountType.FifthweekRevenue, Now, 100));

                await connection.InsertAsync(snapshots, false);
                await connection.InsertAsync(ledgerRecords, false);
                return snapshots.Select(v => new UserId(v.UserId)).ToList();
            }
        }
    }
}