using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Fifthweek.Api.Accounts.Tests
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Tests.Shared;

    using Microsoft.AspNet.Identity;

    using Moq;

    [TestClass]
    public class AccountRepositoryTests : PersistenceTestsBase
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private readonly Email email = new Email("accountrepositorytests@testing.fifthweek.com");

        private readonly ValidEmail newEmail = ValidEmail.Parse("newtestemail@testing.fifthweek.com");

        private readonly ValidUsername newUsername = ValidUsername.Parse("newtestusername");

        private readonly ValidPassword newPassword = ValidPassword.Parse("newtestpassword");

        private readonly FileId newFileId = new FileId(Guid.NewGuid());

        private AccountRepository target;

        private Mock<IUserManager> userManager;

        private Mock<IPasswordHasher> passwordHasher;

        [TestInitialize]
        public void TestInitialize()
        {
            this.passwordHasher = new Mock<IPasswordHasher>();
            this.userManager = new Mock<IUserManager>();
            this.userManager.Setup(v => v.PasswordHasher).Returns(this.passwordHasher.Object);
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalled_ItShouldGetAccountSettingsFromTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            var result = await this.target.GetAccountSettingsAsync(this.userId);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);

            Assert.AreEqual(result.Email, this.email);
            Assert.AreEqual(result.ProfileImageFileId, this.fileId);
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithInvalidUserId_ItShouldThrowARecoverableException()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            await ExpectedException<DetailedRecoverableException>.AssertAsync(
                () => this.target.GetAccountSettingsAsync(new UserId(Guid.NewGuid())));

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithNullUserId_ItShouldThrowAnAugumentException()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            await ExpectedException<ArgumentNullException>.AssertAsync(
                () => this.target.GetAccountSettingsAsync(null));

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalled_ItShouldUpdateTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();
            var currentUser = await this.GetUser();

            var hashedNewPassword = this.newPassword.Value + "1";
            this.passwordHasher.Setup(v => v.HashPassword(this.newPassword.Value)).Returns(hashedNewPassword);

            var result = await this.target.UpdateAccountSettingsAsync(
                this.userId,
                this.newUsername,
                this.newEmail,
                this.newPassword,
                this.newFileId);

            Assert.AreEqual(true, result.EmailChanged);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = new WildcardEntity<FifthweekUser>(currentUser)
                {
                    AreEqual = actualFile =>
                    {
                        return actualFile.Id == currentUser.Id
                            && actualFile.UserName == this.newUsername.Value
                            && actualFile.Email == this.newEmail.Value
                            && actualFile.PasswordHash == hashedNewPassword
                            && actualFile.ProfileImageFileId == this.newFileId.Value;
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithoutANewPassword_ItShouldUpdateTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();
            var currentUser = await this.GetUser();

            var result = await this.target.UpdateAccountSettingsAsync(
                this.userId,
                this.newUsername,
                this.newEmail,
                null,
                this.newFileId);

            Assert.AreEqual(true, result.EmailChanged);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = new WildcardEntity<FifthweekUser>(currentUser)
                {
                    AreEqual = actualFile =>
                    {
                        return actualFile.Id == currentUser.Id
                            && actualFile.UserName == this.newUsername.Value
                            && actualFile.Email == this.newEmail.Value
                            && actualFile.PasswordHash == currentUser.PasswordHash
                            && actualFile.ProfileImageFileId == this.newFileId.Value;
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithSameEMail_ItShouldDetectTheEmailHasNotChanged()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();
            var currentUser = await this.GetUser();

            var result = await this.target.UpdateAccountSettingsAsync(
                this.userId,
                this.newUsername,
                ValidEmail.Parse(this.email.Value),
                null,
                this.newFileId);

            Assert.AreEqual(false, result.EmailChanged);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = new WildcardEntity<FifthweekUser>(currentUser)
                {
                    AreEqual = actualFile =>
                    {
                        return actualFile.Id == currentUser.Id
                            && actualFile.UserName == this.newUsername.Value
                            && actualFile.Email == this.email.Value
                            && actualFile.PasswordHash == currentUser.PasswordHash
                            && actualFile.ProfileImageFileId == this.newFileId.Value;
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithoutAProfileImageFileId_ItShouldUpdateTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();
            var currentUser = await this.GetUser();

            var result = await this.target.UpdateAccountSettingsAsync(
                this.userId,
                this.newUsername,
                this.newEmail,
                null,
                null);

            Assert.AreEqual(true, result.EmailChanged);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = new WildcardEntity<FifthweekUser>(currentUser)
                {
                    AreEqual = actualFile =>
                    {
                        return actualFile.Id == currentUser.Id
                            && actualFile.UserName == this.newUsername.Value
                            && actualFile.Email == this.newEmail.Value
                            && actualFile.PasswordHash == currentUser.PasswordHash
                            && actualFile.ProfileImageFileId == null;
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullUserId_ItShouldThrowAnAugumentException()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            await ExpectedException<ArgumentNullException>.AssertAsync(
                () => this.target.UpdateAccountSettingsAsync(
                    null,
                    this.newUsername,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId));

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullEmail_ItShouldThrowAnAugumentException()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            await ExpectedException<ArgumentNullException>.AssertAsync(
                () => this.target.UpdateAccountSettingsAsync(
                    this.userId,
                    this.newUsername,
                    null,
                    this.newPassword,
                    this.newFileId));

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenUpdateAccountSettingsCalledWithNullUsername_ItShouldThrowAnAugumentException()
        {
            await this.InitializeWithDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            await ExpectedException<ArgumentNullException>.AssertAsync(
                () => this.target.UpdateAccountSettingsAsync(
                    this.userId,
                    null,
                    this.newEmail,
                    this.newPassword,
                    this.newFileId));

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        private async Task<FifthweekUser> GetUser()
        {
            using (var databaseContext = this.NewDbContext())
            {
                return await this.GetUser(databaseContext);
            }
        }

        private async Task<FifthweekUser> GetUser(IFifthweekDbContext databaseContext)
        {
            return await databaseContext.Users.SingleAsync(v => v.Id == this.userId.Value);
        }

        private async Task InitializeWithoutDatabaseAsync()
        {
            this.target = new AccountRepository(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object, this.userManager.Object);
        }

        private async Task InitializeWithDatabaseAsync()
        {
            await this.InitializeDatabaseAsync();

            this.target = new AccountRepository(this.NewDbContext(), this.userManager.Object);

            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = this.userId.Value;

            user.Email = this.email.Value;

            user.ProfileImageFile = FileTests.UniqueEntity(random);
            user.ProfileImageFile.Id = this.fileId.Value;
            user.ProfileImageFileId = this.fileId.Value;

            var otherFile = FileTests.UniqueEntity(random);
            otherFile.Id = this.newFileId.Value;
            otherFile.User = user;
            otherFile.UserId = user.Id;

            using (var databaseContext = this.NewDbContext())
            {
                databaseContext.Files.Add(user.ProfileImageFile);
                databaseContext.Files.Add(otherFile);
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
