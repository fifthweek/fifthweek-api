namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserRolesDbStatementTests : PersistenceTestsBase
    {
        private GetUserRolesDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetUserRolesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenUserIdHasNoRoles_ItReturnEmptyList()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserRolesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase, 20);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId.Random());
        
                Assert.AreEqual(
                    new GetUserRolesDbStatement.UserRoles(new List<GetUserRolesDbStatement.UserRoles.UserRole>()),
                    result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIsCreator_ItShouldReturnRoles()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserRolesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase, 20);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(userIds.Item2);

                CollectionAssert.AreEquivalent(
                    new List<GetUserRolesDbStatement.UserRoles.UserRole>
                    {
                        new GetUserRolesDbStatement.UserRoles.UserRole(FifthweekRole.CreatorId, FifthweekRole.Creator),
                    },
                    result.Roles.ToList());

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIsCreatorAndTestUser_ItShouldReturnRoles()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserRolesDbStatement(testDatabase);

                var userIds = await this.CreateDataAsync(testDatabase, 20);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(userIds.Item1);

                CollectionAssert.AreEquivalent(
                    new List<GetUserRolesDbStatement.UserRoles.UserRole>
                    {
                        new GetUserRolesDbStatement.UserRoles.UserRole(FifthweekRole.CreatorId, FifthweekRole.Creator),
                        new GetUserRolesDbStatement.UserRoles.UserRole(FifthweekRole.TestUserId, FifthweekRole.TestUser),
                    },
                    result.Roles.ToList());

                return ExpectedSideEffects.None;
            });
        }

        private async Task<Tuple<UserId, UserId>> CreateDataAsync(TestDatabaseContext testDatabase, int creatorChannelsSnapshotCount)
        {
            var random = new Random();
            var testUserId = Guid.NewGuid();
            var normalUserId = Guid.NewGuid();
            using (var databaseContext = testDatabase.CreateContext())
            {
                var testUser = UserTests.UniqueEntity(random);
                testUser.Id = testUserId;
                databaseContext.Users.Add(testUser);

                var normalUser = UserTests.UniqueEntity(random);
                normalUser.Id = normalUserId;
                databaseContext.Users.Add(normalUser);

                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                var userRoles = new List<FifthweekUserRole>();
                userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, normalUserId));
                userRoles.Add(new FifthweekUserRole(FifthweekRole.CreatorId, testUserId));
                userRoles.Add(new FifthweekUserRole(FifthweekRole.TestUserId, testUserId));

                await connection.InsertAsync(userRoles, false);
                return new Tuple<UserId, UserId>(
                    new UserId(testUserId),
                    new UserId(normalUserId));
            }
        }
    }
}