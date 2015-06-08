namespace Fifthweek.Api.Identity.Membership.Tests
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Payments.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.AspNet.Identity;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateAccountSettingsDbRepositoryTests : PersistenceTestsBase
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly FileId fileId = new FileId(Guid.NewGuid());
        private readonly Email email = new Email("accountrepositorytests@testing.fifthweek.com");
        private readonly ValidEmail newEmail = ValidEmail.Parse("newtestemail@testing.fifthweek.com");
        private readonly ValidUsername newUsername = ValidUsername.Parse("newtestusername");
        private readonly ValidPassword newPassword = ValidPassword.Parse("newtestpassword");
        private readonly FileId newFileId = new FileId(Guid.NewGuid());
        private readonly string securityStamp = Guid.NewGuid().ToString();
        private UpdateAccountSettingsDbStatement target;
        private Mock<IUserManager> userManager;
        private Mock<IPasswordHasher> passwordHasher;
        private MockRequestSnapshotService requestSnapshot;

        [TestInitialize]
        public void Initialize()
        {
            this.passwordHasher = new Mock<IPasswordHasher>();
            this.userManager = new Mock<IUserManager>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(this.passwordHasher.Object);
            this.requestSnapshot = new MockRequestSnapshotService();

            // Required for non-database tests.
            this.target = new UpdateAccountSettingsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object, this.userManager.Object, this.requestSnapshot);
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalled_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.Email = this.newEmail.Value;
                expectedUser.EmailConfirmed = false;
                expectedUser.PasswordHash = hashedNewPassword;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp);

                Assert.AreEqual(false, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalled_ItShouldRequestSnapshotAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                var trackingDatabase = new TrackingConnectionFactory(testDatabase);
                this.target = new UpdateAccountSettingsDbStatement(trackingDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.Email = this.newEmail.Value;
                expectedUser.EmailConfirmed = false;
                expectedUser.PasswordHash = hashedNewPassword;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                this.requestSnapshot.VerifyConnectionDisposed(trackingDatabase);

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp);

                this.requestSnapshot.VerifyCalledWith(this.userId, SnapshotType.Subscriber);

                Assert.AreEqual(false, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalled_ItShouldAbortUpdateIfSnapshotFails()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.Email = this.newEmail.Value;
                expectedUser.EmailConfirmed = false;
                expectedUser.PasswordHash = hashedNewPassword;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                this.requestSnapshot.ThrowException();

                await ExpectedException.AssertExceptionAsync<SnapshotException>(
                    () => this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp));

                return ExpectedSideEffects.TransactionAborted;
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledTwice_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase);

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp);

                await testDatabase.TakeSnapshotAsync();

                var result2 = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp);

                Assert.AreEqual(result, result2);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledAndEmailWasNotConfirmed_TheEmailShouldStillNotBeConfirmedAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.Email = this.newEmail.Value;
                expectedUser.PasswordHash = hashedNewPassword;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId,
                    this.securityStamp);

                Assert.AreEqual(false, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }


        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithoutANewPassword_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                    await this.CreateUserAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var expectedUser = await this.GetUserAsync(testDatabase);
                    expectedUser.UserName = this.newUsername.Value;
                    expectedUser.Email = this.newEmail.Value;
                    expectedUser.EmailConfirmed = false;
                    expectedUser.ProfileImageFileId = this.newFileId.Value;
                    expectedUser.SecurityStamp = this.securityStamp;

                    var result = await this.target.ExecuteAsync(
                        this.userId,
                        this.newUsername,
                        this.newEmail,
                        null,
                        this.newFileId,
                        this.securityStamp);

                    Assert.AreEqual(false, result.EmailConfirmed);

                    return new ExpectedSideEffects
                               {
                                   Update = expectedUser
                               };
                });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithSameEMail_ItShouldDetectTheEmailHasNotChanged()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    ValidEmail.Parse(this.email.Value),
                    null,
                    this.newFileId,
                    this.securityStamp);

                Assert.AreEqual(true, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithSameEMailAndEmailWasNotConfirmed_ItShouldStillNotBeConfirmed()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                await this.CreateUserAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.ProfileImageFileId = this.newFileId.Value;
                expectedUser.SecurityStamp = this.securityStamp;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    ValidEmail.Parse(this.email.Value),
                    null,
                    this.newFileId,
                    this.securityStamp);

                Assert.AreEqual(false, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithoutAProfileImageFileId_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new UpdateAccountSettingsDbStatement(testDatabase, this.userManager.Object, this.requestSnapshot);
                    await this.CreateUserAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var expectedUser = await this.GetUserAsync(testDatabase);
                    expectedUser.UserName = this.newUsername.Value;
                    expectedUser.Email = this.newEmail.Value;
                    expectedUser.EmailConfirmed = false;
                    expectedUser.ProfileImageFileId = null;
                    expectedUser.SecurityStamp = this.securityStamp;

                    var result = await this.target.ExecuteAsync(
                        this.userId,
                        this.newUsername,
                        this.newEmail,
                        null,
                        null,
                        this.securityStamp);

                    Assert.AreEqual(false, result.EmailConfirmed);

                    return new ExpectedSideEffects
                    {
                        Update = expectedUser
                    };
                });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullUserId_ItShouldThrowAnAugumentException()
        {
            Func<Task> badMethodCall = () => this.target.ExecuteAsync(
                null,
                this.newUsername,
                this.newEmail,
                this.newPassword,
                this.newFileId,
                this.securityStamp);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullEmail_ItShouldThrowAnAugumentException()
        {
            Func<Task> badMethodCall = () => this.target.ExecuteAsync(
                this.userId,
                this.newUsername,
                null,
                this.newPassword,
                this.newFileId,
                this.securityStamp);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullUsername_ItShouldThrowAnAugumentException()
        {
            Func<Task> badMethodCall = () => this.target.ExecuteAsync(
                this.userId,
                null,
                this.newEmail,
                this.newPassword,
                this.newFileId,
                this.securityStamp);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        private async Task<FifthweekUser> GetUserAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Configuration.ProxyCreationEnabled = false;
                return await databaseContext.Users.SingleAsync(v => v.Id == this.userId.Value);
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase, bool emailConfirmed = true)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = this.userId.Value;
            user.Email = this.email.Value;
            user.EmailConfirmed = emailConfirmed;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }

            var profileImageFile = FileTests.UniqueEntity(random);
            profileImageFile.Id = this.fileId.Value;
            profileImageFile.User = user;
            profileImageFile.UserId = user.Id;

            var otherFile = FileTests.UniqueEntity(random);
            otherFile.Id = this.newFileId.Value;
            otherFile.User = user;
            otherFile.UserId = user.Id;

            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.Database.Connection.InsertAsync(profileImageFile);
                await databaseContext.Database.Connection.InsertAsync(otherFile);

                user.ProfileImageFile = profileImageFile;
                user.ProfileImageFileId = profileImageFile.Id;

                await databaseContext.Database.Connection.UpdateAsync(user, FifthweekUser.Fields.ProfileImageFileId);
            }
        }
    }
}