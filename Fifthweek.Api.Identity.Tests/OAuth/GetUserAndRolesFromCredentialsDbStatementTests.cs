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
    public class GetUserAndRolesFromCredentialsDbStatementTests : PersistenceTestsBase
    {
        private const string PasswordHash = "password1";
        private static readonly Username Username = new Username("username");
        private static readonly Password Password = new Password("password");

        private GetUserAndRolesFromCredentialsDbStatement target;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private Mock<IUserManager> userManager;

        [TestInitialize]
        public void TestInitialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);

            var passwordHasher = new Mock<IPasswordHasher>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(passwordHasher.Object);
            passwordHasher.Setup(v => v.HashPassword(Password.Value)).Returns(PasswordHash);
            passwordHasher.Setup(v => v.VerifyHashedPassword(PasswordHash, Password.Value)).Returns(PasswordVerificationResult.Success);

            this.target = new GetUserAndRolesFromCredentialsDbStatement(
                this.userManager.Object,
                this.connectionFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUsernameIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Password);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPasswordIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(Username, null);
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldReturnResult()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromCredentialsDbStatement(
                    this.userManager.Object,
                    testDatabase);

                var userId = await this.CreateUser(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username, Password);

                Assert.IsNotNull(result);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(2, result.Roles.Count);
                Assert.IsTrue(result.Roles.Any(v => v == "One"));
                Assert.IsTrue(result.Roles.Any(v => v == "Two"));
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCalled_AndUserHasNoRoles_ItShouldReturnResult()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromCredentialsDbStatement(
                    this.userManager.Object,
                    testDatabase);

                var userId = await this.CreateUser(testDatabase, false);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username, Password);

                Assert.IsNotNull(result);
                Assert.AreEqual(userId, result.UserId);
                Assert.AreEqual(0, result.Roles.Count);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCalledWithIncorrectPassword_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromCredentialsDbStatement(
                    this.userManager.Object,
                    testDatabase);

                await this.CreateUser(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username, new Password(Password.Value + "Incorrect"));

                Assert.IsNull(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCalledWithIncorrectUsername_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromCredentialsDbStatement(
                    this.userManager.Object,
                    testDatabase);

                await this.CreateUser(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new Username(Username + "Incorrect"), Password);

                Assert.IsNull(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task<UserId> CreateUser(TestDatabaseContext testDatabase, bool addUser1Roles = true)
        {
            using (var connection = testDatabase.CreateConnection())
            {
                var random = new Random();
                var user1 = UserTests.UniqueEntity(random);
                user1.UserName = Username.Value;
                user1.PasswordHash = PasswordHash;

                var user2 = UserTests.UniqueEntity(random);
                user2.UserName = Username + "2";
                user2.PasswordHash = PasswordHash;

                await connection.InsertAsync(user1);
                await connection.InsertAsync(user2);

                var role1 = new FifthweekRole(Guid.NewGuid());
                role1.Name = "One";

                var role2 = new FifthweekRole(Guid.NewGuid());
                role2.Name = "Two";

                var role3 = new FifthweekRole(Guid.NewGuid());
                role3.Name = "Three";

                await connection.InsertAsync(role1);
                await connection.InsertAsync(role2);
                await connection.InsertAsync(role3);

                if (addUser1Roles)
                {
                    var userRole1 = new FifthweekUserRole();
                    userRole1.UserId = user1.Id;
                    userRole1.RoleId = role1.Id;

                    var userRole2 = new FifthweekUserRole();
                    userRole2.UserId = user1.Id;
                    userRole2.RoleId = role2.Id;

                    await connection.InsertAsync(userRole1);
                    await connection.InsertAsync(userRole2);
                }

                var userRole3 = new FifthweekUserRole();
                userRole3.UserId = user2.Id;
                userRole3.RoleId = role2.Id;

                var userRole4 = new FifthweekUserRole();
                userRole4.UserId = user2.Id;
                userRole4.RoleId = role3.Id;

                await connection.InsertAsync(userRole3);
                await connection.InsertAsync(userRole4);

                return new UserId(user1.Id);
            }
        }
    }
}