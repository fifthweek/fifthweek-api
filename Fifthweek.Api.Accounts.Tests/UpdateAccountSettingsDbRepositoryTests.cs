namespace Fifthweek.Api.Accounts.Tests
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
        private UpdateAccountSettingsDbStatement target;
        private Mock<IUserManager> userManager;
        private Mock<IPasswordHasher> passwordHasher;

        [TestInitialize]
        public void Initialize()
        {
            this.passwordHasher = new Mock<IPasswordHasher>();
            this.userManager = new Mock<IUserManager>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(this.passwordHasher.Object);

            // Required for non-database tests.
            this.target = new UpdateAccountSettingsDbStatement(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object, this.userManager.Object);
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalled_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
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

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId);

                Assert.AreEqual(false, result.EmailConfirmed);

                return new ExpectedSideEffects
                {
                    Update = expectedUser
                };
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledTwice_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                await this.CreateUserAsync(testDatabase);

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId);

                await testDatabase.TakeSnapshotAsync();

                var result2 = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId);

                Assert.AreEqual(result, result2);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledAndEmailWasNotConfirmed_TheEmailShouldStillNotBeConfirmedAfterUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                await this.CreateUserAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                var hashedNewPassword = this.newPassword.Value + "1";
                this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.Email = this.newEmail.Value;
                expectedUser.PasswordHash = hashedNewPassword;
                expectedUser.ProfileImageFileId = this.newFileId.Value;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId);

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
                    this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                    await this.CreateUserAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var expectedUser = await this.GetUserAsync(testDatabase);
                    expectedUser.UserName = this.newUsername.Value;
                    expectedUser.Email = this.newEmail.Value;
                    expectedUser.EmailConfirmed = false;
                    expectedUser.ProfileImageFileId = this.newFileId.Value;

                    var result = await this.target.ExecuteAsync(
                        this.userId,
                        this.newUsername,
                        this.newEmail,
                        null,
                        this.newFileId);

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
                this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.ProfileImageFileId = this.newFileId.Value;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    ValidEmail.Parse(this.email.Value),
                    null,
                    this.newFileId);

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
                this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                await this.CreateUserAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                var expectedUser = await this.GetUserAsync(testDatabase);
                expectedUser.UserName = this.newUsername.Value;
                expectedUser.ProfileImageFileId = this.newFileId.Value;

                var result = await this.target.ExecuteAsync(
                    this.userId,
                    this.newUsername,
                    ValidEmail.Parse(this.email.Value),
                    null,
                    this.newFileId);

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
                    this.target = new UpdateAccountSettingsDbStatement(testDatabase.NewContext(), this.userManager.Object);
                    await this.CreateUserAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var expectedUser = await this.GetUserAsync(testDatabase);
                    expectedUser.UserName = this.newUsername.Value;
                    expectedUser.Email = this.newEmail.Value;
                    expectedUser.EmailConfirmed = false;
                    expectedUser.ProfileImageFileId = null;

                    var result = await this.target.ExecuteAsync(
                        this.userId,
                        this.newUsername,
                        this.newEmail,
                        null,
                        null);

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
                this.newFileId);

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
                this.newFileId);

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
                this.newFileId);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        private async Task<FifthweekUser> GetUserAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                return await this.GetUserAsync(databaseContext);
            }
        }

        private async Task<FifthweekUser> GetUserAsync(IFifthweekDbContext databaseContext)
        {
            return await databaseContext.Users.SingleAsync(v => v.Id == this.userId.Value);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase, bool emailConfirmed = true)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = this.userId.Value;
            user.Email = this.email.Value;
            user.EmailConfirmed = emailConfirmed;

            using (var databaseContext = testDatabase.NewContext())
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

            using (var databaseContext = testDatabase.NewContext())
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