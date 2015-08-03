namespace Fifthweek.Payments.Tests.Services
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

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAllSubscribersDbStatementTests : PersistenceTestsBase
    {
        private GetAllSubscribersDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetAllSubscribersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnAllSubscriberIdsWithSnapshots()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetAllSubscribersDbStatement(testDatabase);

                    var userIds = await this.CreateDataAsync(testDatabase, 100);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync();

                    Assert.AreEqual(100 + TestDatabaseSeed.SubscriberChannelSnapshots, result.Count);

                    foreach (var item in userIds)
                    {
                        Assert.IsTrue(result.Contains(item));
                    }

                    return ExpectedSideEffects.None;
                });
        }

        private async Task<IReadOnlyList<UserId>> CreateDataAsync(TestDatabaseContext testDatabase, int creatorChannelsSnapshotCount)
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
                for (int i = 0; i < creatorChannelsSnapshotCount / 2; i++)
                {
                    var user = UserTests.UniqueEntity(random);
                    normalUserIds.Add(user.Id);
                    databaseContext.Users.Add(user);
                }

                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                var creatorChannelsSnapshots = new List<SubscriberChannelsSnapshot>();
                var creatorChannelsSnapshotItems = new List<SubscriberChannelsSnapshotItem>();
                var userRoles = new List<FifthweekUserRole>();

                foreach (var userId in normalUserIds)
                {
                    // Add some normal users to creator roles.
                    if (random.NextDouble() > 0.5)
                    {
                        userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, userId));
                    }

                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.UtcNow));
                }

                for (int i = 0; i < creatorChannelsSnapshotCount - normalUserIds.Count; i++)
                {
                    // Add some deleted users (not in AspNetUsers table).
                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, Guid.NewGuid()));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.UtcNow));
                }

                foreach (var userId in testUserIds)
                {
                    // Add some test users.
                    userRoles.Add(new FifthweekUserRole(FifthweekRole.TestUserId, userId));
                    
                    if (random.NextDouble() > 0.5)
                    {
                        userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, userId));
                    }

                    creatorChannelsSnapshots.Add(new SubscriberChannelsSnapshot(Guid.NewGuid(), DateTime.UtcNow, userId));
                    creatorChannelsSnapshotItems.Add(new SubscriberChannelsSnapshotItem(creatorChannelsSnapshots.Last().Id, null, Guid.NewGuid(), Guid.NewGuid(), 100, DateTime.UtcNow));
                }

                await connection.InsertAsync(creatorChannelsSnapshots, false);
                await connection.InsertAsync(creatorChannelsSnapshotItems, false);
                await connection.InsertAsync(userRoles, false);
                return creatorChannelsSnapshots.Where(v => !testUserIds.Contains(v.SubscriberId)).Select(v => new UserId(v.SubscriberId)).ToList();
            }
        }
    }
}