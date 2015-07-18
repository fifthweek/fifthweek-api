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
    public class GetCalculatedAccountBalancesDbStatementTests : PersistenceTestsBase
    {
        private const int Days = 10;
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId SubscriberId1 = UserId.Random();
        private static readonly UserId SubscriberId2 = UserId.Random();

        private GetCalculatedAccountBalancesDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCalculatedAccountBalancesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCalculatedAccountBalancesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId1, LedgerAccountType.FifthweekCredit, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId1, result[i].UserId);
                    Assert.AreEqual(LedgerAccountType.FifthweekCredit, result[i].AccountType);
                    Assert.AreEqual(Now.AddDays(i), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i, result[i].Amount);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnAllResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCalculatedAccountBalancesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriberId2, LedgerAccountType.Stripe, Now, Now.AddDays(Days));

                Assert.AreEqual(Days, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId2, result[i].UserId);
                    Assert.AreEqual(LedgerAccountType.Stripe, result[i].AccountType);
                    Assert.AreEqual(Now.AddDays(i).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i + 0.75m, result[i].Amount);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber1()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCalculatedAccountBalancesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Two fewer results at the end, and one fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(SubscriberId1, LedgerAccountType.Stripe, Now.AddHours(18).AddDays(1), Now.AddDays(Days - 2));

                Assert.AreEqual(Days - 3, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId1, result[i].UserId);
                    Assert.AreEqual(LedgerAccountType.Stripe, result[i].AccountType);
                    Assert.AreEqual(Now.AddDays(i + 1), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i + 1 + 0.25m, result[i].Amount);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnSubsetOfResultsForSubscriber2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCalculatedAccountBalancesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                // Three fewer results at the end, and two fewer at the beginning as we select first 
                // result in front of timestamp.
                var result = await this.target.ExecuteAsync(SubscriberId2, LedgerAccountType.FifthweekCredit, Now.AddHours(18).AddDays(2), Now.AddDays(Days - 3));

                Assert.AreEqual(Days - 5, result.Count);

                result = result.OrderBy(v => v.Timestamp).ToList();

                for (int i = 0; i < result.Count; i++)
                {
                    Assert.AreEqual(SubscriberId2, result[i].UserId);
                    Assert.AreEqual(LedgerAccountType.FifthweekCredit, result[i].AccountType);
                    Assert.AreEqual(Now.AddDays(i + 2).AddHours(12), result[i].Timestamp);
                    Assert.IsTrue(result[i].Timestamp.Kind == DateTimeKind.Utc);
                    Assert.AreEqual(i + 2 + 0.5m, result[i].Amount);
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var snapshots = new List<CalculatedAccountBalance>();

                for (int i = 0; i < Days; i++)
                {
                    snapshots.Add(new CalculatedAccountBalance(SubscriberId1.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(i), i));
                    snapshots.Add(new CalculatedAccountBalance(SubscriberId1.Value, LedgerAccountType.Stripe, Now.AddDays(i), i + 0.25m));

                    snapshots.Add(new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.FifthweekCredit, Now.AddDays(i).AddHours(12), i + 0.5m));
                    snapshots.Add(new CalculatedAccountBalance(SubscriberId2.Value, LedgerAccountType.Stripe, Now.AddDays(i).AddHours(12), i + 0.75m));
                }

                await connection.InsertAsync(snapshots, false);
                return snapshots.Select(v => new UserId(v.UserId)).ToList();
            }
        }
    }
}