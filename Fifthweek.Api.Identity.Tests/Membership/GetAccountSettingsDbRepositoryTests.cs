namespace Fifthweek.Api.Identity.Membership.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetAccountSettingsDbRepositoryTests : PersistenceTestsBase
    {
        private readonly Username username = new Username("username");
        private readonly Username username2 = new Username("username2");
        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly UserId userId2 = new UserId(Guid.NewGuid());
        private readonly FileId fileId = new FileId(Guid.NewGuid());
        private readonly Email email = new Email("accountrepositorytests@testing.fifthweek.com");
        private readonly Email email2 = new Email("accountrepositorytests2@testing.fifthweek.com");
        private readonly FileId newFileId = new FileId(Guid.NewGuid());
        private GetAccountSettingsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Required for non-database tests.
            this.target = new GetAccountSettingsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalled_ItShouldGetAccountSettingsFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAccountSettingsDbStatement(testDatabase);
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(this.userId);

                Assert.AreEqual(result.Username, this.username);
                Assert.AreEqual(result.Email, this.email);
                Assert.AreEqual(result.ProfileImageFileId, this.fileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledAndNoProfileImageExists_ItShouldGetAccountSettingsFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAccountSettingsDbStatement(testDatabase);
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(this.userId2);

                Assert.AreEqual(this.username2, result.Username);
                Assert.AreEqual(this.email2, result.Email);
                Assert.AreEqual(null, result.ProfileImageFileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithInvalidUserId_ItShouldThrowARecoverableException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAccountSettingsDbStatement(testDatabase);
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.ExecuteAsync(new UserId(Guid.NewGuid()));

                await badMethodCall.AssertExceptionAsync<DetailedRecoverableException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithNullUserId_ItShouldThrowAnAugumentException()
        {
            Func<Task> badMethodCall = () => this.target.ExecuteAsync(null);

            await badMethodCall.AssertExceptionAsync<ArgumentNullException>();
        }

        private async Task CreateFileAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = this.userId.Value;
            user.Email = this.email.Value;
            user.UserName = this.username.Value;

            var user2 = UserTests.UniqueEntity(random);
            user2.Id = this.userId2.Value;
            user2.Email = this.email2.Value;
            user2.UserName = this.username2.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                databaseContext.Users.Add(user2);
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
