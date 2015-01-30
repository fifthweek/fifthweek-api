namespace Fifthweek.Api.Identity.Tests.OAuth
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserAndRolesFromUserIdDbStatementTests : PersistenceTestsBase
    {
        private const string Username = "username";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private GetUserAndRolesFromUserIdDbStatement target;
        private Mock<IFifthweekDbContext> fifthweekDbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.target = new GetUserAndRolesFromUserIdDbStatement(
                this.fifthweekDbContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldReturnResult()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromUserIdDbStatement(
                    testDatabase.NewContext());

                await this.CreateUser(testDatabase.NewContext());

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.IsNotNull(result);
                Assert.AreEqual(Username, result.Username.Value);
                Assert.AreEqual(2, result.Roles.Count);
                Assert.IsTrue(result.Roles.Any(v => v == "One"));
                Assert.IsTrue(result.Roles.Any(v => v == "Two"));
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCalledWithIncorrectUserId_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromUserIdDbStatement(
                    testDatabase.NewContext());

                await this.CreateUser(testDatabase.NewContext());

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new UserId(Guid.NewGuid()));

                Assert.IsNull(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateUser(IFifthweekDbContext databaseContext)
        {
            var random = new Random();
            var user1 = UserTests.UniqueEntity(random);
            user1.Id = UserId.Value;
            user1.UserName = Username;
            user1.PasswordHash = user1.UserName;

            var user2 = UserTests.UniqueEntity(random);
            user2.Id = Guid.NewGuid();
            user2.PasswordHash = user1.UserName;

            var role1 = new FifthweekRole(Guid.NewGuid());
            role1.Name = "One";

            var role2 = new FifthweekRole(Guid.NewGuid());
            role2.Name = "Two";

            var role3 = new FifthweekRole(Guid.NewGuid());
            role3.Name = "Three";

            var userRole1 = new FifthweekUserRole();
            userRole1.UserId = user1.Id;
            userRole1.RoleId = role1.Id;

            var userRole2 = new FifthweekUserRole();
            userRole2.UserId = user1.Id;
            userRole2.RoleId = role2.Id;

            var userRole3 = new FifthweekUserRole();
            userRole3.UserId = user2.Id;
            userRole3.RoleId = role2.Id;

            var userRole4 = new FifthweekUserRole();
            userRole4.UserId = user2.Id;
            userRole4.RoleId = role3.Id;

            await databaseContext.Database.Connection.InsertAsync(user1);
            await databaseContext.Database.Connection.InsertAsync(user2);
            await databaseContext.Database.Connection.InsertAsync(role1);
            await databaseContext.Database.Connection.InsertAsync(role2);
            await databaseContext.Database.Connection.InsertAsync(role3);
            await databaseContext.Database.Connection.InsertAsync(userRole1);
            await databaseContext.Database.Connection.InsertAsync(userRole2);
            await databaseContext.Database.Connection.InsertAsync(userRole3);
            await databaseContext.Database.Connection.InsertAsync(userRole4);
        }
    }
}