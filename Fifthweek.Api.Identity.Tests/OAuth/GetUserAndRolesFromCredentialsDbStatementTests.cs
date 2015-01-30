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
        private Mock<IFifthweekDbContext> fifthweekDbContext;
        private Mock<IUserManager> userManager;

        [TestInitialize]
        public void TestInitialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.fifthweekDbContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);
            this.userManager = new Mock<IUserManager>(MockBehavior.Strict);

            var passwordHasher = new Mock<IPasswordHasher>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(passwordHasher.Object);
            passwordHasher.Setup(v => v.HashPassword(Password.Value)).Returns(PasswordHash);
            passwordHasher.Setup(v => v.VerifyHashedPassword(PasswordHash, Password.Value)).Returns(PasswordVerificationResult.Success);

            this.target = new GetUserAndRolesFromCredentialsDbStatement(
                this.userManager.Object,
                this.fifthweekDbContext.Object);
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
                    testDatabase.NewContext());

                var userId = await this.CreateUser(testDatabase.NewContext());

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
        public async Task WhenCalledWithIncorrectPassword_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserAndRolesFromCredentialsDbStatement(
                    this.userManager.Object,
                    testDatabase.NewContext());

                await this.CreateUser(testDatabase.NewContext());

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
                    testDatabase.NewContext());

                await this.CreateUser(testDatabase.NewContext());

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new Username(Username + "Incorrect"), Password);

                Assert.IsNull(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task<UserId> CreateUser(IFifthweekDbContext databaseContext)
        {
            var random = new Random();
            var user1 = UserTests.UniqueEntity(random);
            user1.UserName = Username.Value;
            user1.PasswordHash = PasswordHash;

            var user2 = UserTests.UniqueEntity(random);
            user2.UserName = Username + "2";
            user2.PasswordHash = PasswordHash;

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

            return new UserId(user1.Id);
        }
    }
}