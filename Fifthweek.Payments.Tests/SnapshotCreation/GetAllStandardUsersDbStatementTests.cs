namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;
    using Fifthweek.Payments.SnapshotCreation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllStandardUsersDbStatementTests : PersistenceTestsBase
    {
        private GetAllStandardUsersDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetAllStandardUsersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnAllStandardUserIds()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAllStandardUsersDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase, 100);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync();

                Assert.AreEqual(userIds.Count + TestDatabaseSeed.Users, result.Count);

                foreach (var item in userIds)
                {
                    Assert.IsTrue(result.Contains(item));
                }

                return ExpectedSideEffects.None;
            });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase, int normalUserCount)
        {
            var random = new Random();
            var testUserIds = new List<Guid>();
            var normalUserIds = new List<Guid>();
            using (var databaseContext = testDatabase.CreateContext())
            {
                // Create some test users which should be ignored.
                for (int i = 0; i < 3; i++)
                {
                    var user = UserTests.UniqueEntity(random);
                    testUserIds.Add(user.Id);
                    databaseContext.Users.Add(user);
                }

                // Create some normal users which shouldn't be ignored.
                for (int i = 0; i < normalUserCount; i++)
                {
                    var user = UserTests.UniqueEntity(random);
                    normalUserIds.Add(user.Id);
                    databaseContext.Users.Add(user);
                }

                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                var userRoles = new List<FifthweekUserRole>();

                foreach (var userId in normalUserIds)
                {
                    // Add some normal users to creator roles.
                    if (random.NextDouble() > 0.5)
                    {
                        userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, userId));
                    }
                }

                foreach (var userId in testUserIds)
                {
                    // Add some test users.
                    userRoles.Add(new FifthweekUserRole(FifthweekRole.TestUserId, userId));

                    if (random.NextDouble() > 0.5)
                    {
                        userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, userId));
                    }
                }

                await connection.InsertAsync(userRoles, false);
                return normalUserIds.Select(v => new UserId(v)).ToList();
            }
        }
    }
}