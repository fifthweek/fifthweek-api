namespace Fifthweek.Api.Accounts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Email = Fifthweek.Api.Identity.Shared.Membership.Email;
    using FileId = Fifthweek.Api.FileManagement.Shared.FileId;
    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [TestClass]
    public class GetAccountSettingsDbRepositoryTests : PersistenceTestsBase
    {
        private readonly UserId userId = new UserId(Guid.NewGuid());
        private readonly FileId fileId = new FileId(Guid.NewGuid());
        private readonly Email email = new Email("accountrepositorytests@testing.fifthweek.com");
        private readonly FileId newFileId = new FileId(Guid.NewGuid());
        private GetAccountSettingsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Required for non-database tests.
            this.target = new GetAccountSettingsDbStatement(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalled_ItShouldGetAccountSettingsFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAccountSettingsDbStatement(testDatabase.NewContext());
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(this.userId);
                
                Assert.AreEqual(result.Email, this.email);
                Assert.AreEqual(result.ProfileImageFileId, this.fileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGetAccountSettingsCalledWithInvalidUserId_ItShouldThrowARecoverableException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetAccountSettingsDbStatement(testDatabase.NewContext());
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
