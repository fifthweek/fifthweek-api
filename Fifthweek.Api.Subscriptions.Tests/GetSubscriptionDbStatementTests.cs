namespace Fifthweek.Api.Subscriptions.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetSubscriptionDbStatementTests : PersistenceTestsBase
    {
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());
        
        private GetSubscriptionDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetSubscriptionDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSubscriptionIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenGettingSubscription_ItShouldReturnTheResult()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriptionDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(SubscriptionId);

                Assert.AreEqual(SubscriptionId, result.SubscriptionId);
                Assert.AreEqual(CreatorId, result.CreatorId);
                Assert.AreEqual(HeaderFileId, result.HeaderImageFileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGettingSubscription_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriptionDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);

                await this.target.ExecuteAsync(SubscriptionId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(SubscriptionId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingANonExistantSubscription_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetSubscriptionDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new SubscriptionId(Guid.NewGuid()));

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            await this.CreateUserAsync(testDatabase);
            await this.CreateFileAsync(testDatabase);
            await this.CreateSubscriptionAsync(testDatabase);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = CreatorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateFileAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var file = FileTests.UniqueEntity(random);
            file.Id = HeaderFileId.Value;
            file.UserId = CreatorId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(file);
            }
        }

        private async Task CreateSubscriptionAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = SubscriptionId.Value;
            subscription.CreatorId = CreatorId.Value;
            subscription.HeaderImageFileId = HeaderFileId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(subscription);
            }
        }
    }
}