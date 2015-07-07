namespace Fifthweek.Api.Identity.Tests.Membership
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Controllers;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Payments.Tests.Shared;
    using Fifthweek.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RegisterUserDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly DateTime TimeStamp = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly RegistrationData.Parsed RegistrationData = new RegistrationData
        {
            Email = "test@testing.fifthweek.com",
            ExampleWork = "testing.fifthweek.com",
            Password = "TestPassword",
            Username = "test_username",
            CreatorName = "creator name"
        }.Parse();

        private Mock<IUserManager> userManager;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private Mock<IPasswordHasher> passwordHasher;
        private MockRequestSnapshotService requestSnapshot;
        private RegisterUserDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.userManager = new Mock<IUserManager>();
            this.passwordHasher = new Mock<IPasswordHasher>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(this.passwordHasher.Object);
            this.requestSnapshot = new MockRequestSnapshotService();

            // Give potentially side-effecting components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.target = new RegisterUserDbStatement(this.userManager.Object, this.connectionFactory.Object, this.requestSnapshot);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                    null,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUsernameIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                    UserId,
                    null,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenEmailIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    null,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPasswordIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    null,
                    TimeStamp);
        }

        [TestMethod]
        public async Task WhenRegisteringANewUser_ItShouldAddTheUserToTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);

                var expectedUser = new FifthweekUser
                {
                    Id = UserId.Value,
                    UserName = RegistrationData.Username.Value,
                    Email = RegistrationData.Email.Value,
                    ExampleWork = RegistrationData.ExampleWork,
                    Name = RegistrationData.CreatorName.Value,
                    RegistrationDate = TimeStamp,
                    LastSignInDate = SqlDateTime.MinValue.Value,
                    LastAccessTokenDate = SqlDateTime.MinValue.Value,
                    PasswordHash = hashedPassword,
                };

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<FifthweekUser>(expectedUser)
                    {
                        Expected = actual =>
                        {
                            expectedUser.SecurityStamp = actual.SecurityStamp;
                            return expectedUser;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenRegisteringANewUser_ItShouldRequestSnapshotAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                this.target = new RegisterUserDbStatement(this.userManager.Object, trackingDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.VerifyConnectionDisposed(trackingDatabase);

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);

                this.requestSnapshot.VerifyCalledWith(UserId, SnapshotType.Subscriber);

                var expectedUser = new FifthweekUser
                {
                    Id = UserId.Value,
                    UserName = RegistrationData.Username.Value,
                    Email = RegistrationData.Email.Value,
                    ExampleWork = RegistrationData.ExampleWork,
                    Name = RegistrationData.CreatorName.Value,
                    RegistrationDate = TimeStamp,
                    LastSignInDate = SqlDateTime.MinValue.Value,
                    LastAccessTokenDate = SqlDateTime.MinValue.Value,
                    PasswordHash = hashedPassword,
                };

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<FifthweekUser>(expectedUser)
                    {
                        Expected = actual =>
                        {
                            expectedUser.SecurityStamp = actual.SecurityStamp;
                            return expectedUser;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenRegisteringANewUser_ItShouldAbortUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await testDatabase.TakeSnapshotAsync();

                this.requestSnapshot.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.ExecuteAsync(
                        UserId,
                        RegistrationData.Username,
                        RegistrationData.Email,
                        RegistrationData.ExampleWork,
                        RegistrationData.CreatorName,
                        RegistrationData.Password,
                        TimeStamp));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        [TestMethod]
        public async Task WhenRegisteringANewUserWithNoExampleWork_ItShouldAddTheUserToTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    null,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);

                var expectedUser = new FifthweekUser
                {
                    Id = UserId.Value,
                    UserName = RegistrationData.Username.Value,
                    Email = RegistrationData.Email.Value,
                    ExampleWork = null,
                    Name = RegistrationData.CreatorName.Value,
                    RegistrationDate = TimeStamp,
                    LastSignInDate = SqlDateTime.MinValue.Value,
                    LastAccessTokenDate = SqlDateTime.MinValue.Value,
                    PasswordHash = hashedPassword,
                };

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<FifthweekUser>(expectedUser)
                    {
                        Expected = actual =>
                        {
                            expectedUser.SecurityStamp = actual.SecurityStamp;
                            return expectedUser;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenRegisteringANewUserWithNoCreatorName_ItShouldAddTheUserToTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    null,
                    RegistrationData.Password,
                    TimeStamp);

                var expectedUser = new FifthweekUser
                {
                    Id = UserId.Value,
                    UserName = RegistrationData.Username.Value,
                    Email = RegistrationData.Email.Value,
                    ExampleWork = RegistrationData.ExampleWork,
                    Name = null,
                    RegistrationDate = TimeStamp,
                    LastSignInDate = SqlDateTime.MinValue.Value,
                    LastAccessTokenDate = SqlDateTime.MinValue.Value,
                    PasswordHash = hashedPassword,
                };

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<FifthweekUser>(expectedUser)
                    {
                        Expected = actual =>
                        {
                            expectedUser.SecurityStamp = actual.SecurityStamp;
                            return expectedUser;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenCalledTwice_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);

                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    UserId,
                    RegistrationData.Username,
                    RegistrationData.Email,
                    RegistrationData.ExampleWork,
                    RegistrationData.CreatorName,
                    RegistrationData.Password,
                    TimeStamp);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenRegisteringWithNonUniqueEmail_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await this.CreateUserAsync(testDatabase, "blah", RegistrationData.Email.Value);

                await testDatabase.TakeSnapshotAsync();

                var exception = await ExpectedException.GetExceptionAsync<RecoverableException>(() =>
                {
                    return this.target.ExecuteAsync(
                        UserId,
                        RegistrationData.Username,
                        RegistrationData.Email,
                        RegistrationData.ExampleWork,
                        RegistrationData.CreatorName,
                        RegistrationData.Password,
                        TimeStamp);
                });

                Assert.IsNotNull(exception);
                Assert.IsTrue(exception.Message.Contains("email"));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        [TestMethod]
        public async Task WhenRegisteringWithNonUniqueUsername_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RegisterUserDbStatement(this.userManager.Object, testDatabase, this.requestSnapshot);

                var hashedPassword = RegistrationData.Password + "1";
                this.passwordHasher.Setup(v => v.HashPassword(RegistrationData.Password.Value)).Returns(hashedPassword);

                await this.CreateUserAsync(testDatabase, RegistrationData.Username.Value, RegistrationData.Email + "1");

                await testDatabase.TakeSnapshotAsync();

                var exception = await ExpectedException.GetExceptionAsync<RecoverableException>(() =>
                {
                    return this.target.ExecuteAsync(
                        UserId,
                        RegistrationData.Username,
                        RegistrationData.Email,
                        RegistrationData.ExampleWork,
                        RegistrationData.CreatorName,
                        RegistrationData.Password,
                        TimeStamp);
                });

                Assert.IsNotNull(exception);
                Assert.IsTrue(exception.Message.Contains("username"));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase, string username, string email)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Email = email;
            user.UserName = username;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}