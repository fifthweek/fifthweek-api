namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorPercentageOverrideDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly UserId CreatorId2 = UserId.Random();
        private static readonly UserId CreatorId3 = UserId.Random();

        private GetCreatorPercentageOverrideDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCreatorPercentageOverrideDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnNullIfUserDoesNotExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPercentageOverrideDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId.Random(), Now);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnDataIfUserExistsAndOverrideNotExpired()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPercentageOverrideDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId1, Now);

                Assert.AreEqual(new CreatorPercentageOverrideData(0.9m, Now.AddMonths(1)), result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNullIfUserExistsButOverrideExpired()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPercentageOverrideDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId1, Now.AddMonths(2));

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnDataIfUserExistsAndNoExpiryDate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPercentageOverrideDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId2, Now.AddYears(10));

                Assert.AreEqual(new CreatorPercentageOverrideData(0.8m, null), result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnNullIfUserExistsButNoOverride()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPercentageOverrideDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId3, Now);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();

                var creator1 = UserTests.UniqueEntity(random);
                creator1.Id = CreatorId1.Value;

                var creator2 = UserTests.UniqueEntity(random);
                creator2.Id = CreatorId2.Value;

                var creator3 = UserTests.UniqueEntity(random);
                creator3.Id = CreatorId3.Value;

                var override1 = new CreatorPercentageOverride(
                    CreatorId1.Value,
                    0.9m,
                    Now.AddMonths(1));

                var override2 = new CreatorPercentageOverride(
                    CreatorId2.Value,
                    0.8m,
                    null);

                databaseContext.CreatorPercentageOverrides.Add(override1);
                databaseContext.CreatorPercentageOverrides.Add(override2);
                databaseContext.Users.Add(creator3);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}