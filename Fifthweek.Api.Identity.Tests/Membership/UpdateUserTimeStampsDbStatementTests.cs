namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateUserTimeStampsDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly DateTime TimeStamp = new SqlDateTime(DateTime.UtcNow).Value;

        private Mock<IFifthweekDbContext> fifthweekDbContext;
        private UpdateUserTimeStampsDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.target = new UpdateUserTimeStampsDbStatement(this.fifthweekDbContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_AndUpdatingOneTimestamp_ItShouldThrowAnException()
        {
            await this.target.UpdateAccessTokenAsync(null, TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_AndUpdatingBothTimestamps_ItShouldThrowAnException()
        {
            await this.target.UpdateSignInAndAccessTokenAsync(null, TimeStamp);
        }

        [TestMethod]
        public async Task WhenTheUserIdIsValid_AndUpdatingOneTimestamp_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.LastAccessTokenDate = TimeStamp;

                await this.target.UpdateAccessTokenAsync(UserId, TimeStamp);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenTheUserIdIsValid_AndUpdatingBothTimestamps_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.LastSignInDate = TimeStamp;
                expectedUser.LastAccessTokenDate = TimeStamp;

                await this.target.UpdateSignInAndAccessTokenAsync(UserId, TimeStamp);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenRunTwice_AndUpdatingOneTimestamp_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await this.target.UpdateAccessTokenAsync(UserId, TimeStamp);
                await testDatabase.TakeSnapshotAsync();
                await this.target.UpdateAccessTokenAsync(UserId, TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRunTwice_AndUpdatingBothTimestamps_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await this.target.UpdateSignInAndAccessTokenAsync(UserId, TimeStamp);
                await testDatabase.TakeSnapshotAsync();
                await this.target.UpdateSignInAndAccessTokenAsync(UserId, TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenTheUserIdIsInvalid_AndUpdatingOneTimestamp_ItShouldNotUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.UpdateAccessTokenAsync(new UserId(Guid.NewGuid()), TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenTheUserIdIsInvalid_AndUpdatingBothTimestamps_ItShouldNotUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateUserTimeStampsDbStatement(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.UpdateSignInAndAccessTokenAsync(new UserId(Guid.NewGuid()), TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        private async Task<FifthweekUser> GetUserAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Configuration.ProxyCreationEnabled = false;
                return await databaseContext.Users.SingleAsync(v => v.Id == UserId.Value);
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;
            user.LastSignInDate = TimeStamp.AddSeconds(-360);
            user.LastAccessTokenDate = TimeStamp.AddSeconds(-180);

            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}