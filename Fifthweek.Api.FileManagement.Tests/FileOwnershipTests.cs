namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    
    

    [TestClass]
    public class FileOwnershipTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private FileOwnership target;

        [TestMethod]
        public async Task WhenCheckingFileOwnership_ItShouldPassIfAtLeastOneFileMatchesFileAndUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileOwnership(testDatabase.NewContext());
                await this.CreateFileAsync(UserId, FileId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, FileId);

                Assert.IsTrue(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingFileOwnership_ItShouldFailIfNoFilesExist()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileOwnership(testDatabase.NewContext());

                using (var databaseContext = testDatabase.NewContext())
                {
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Posts");
                    await databaseContext.Database.Connection.ExecuteAsync("DELETE FROM Files");
                }

                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, FileId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingFileOwnership_ItShouldFailIfNoFilesMatchFileOrUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileOwnership(testDatabase.NewContext());
                await this.CreateFileAsync(new UserId(Guid.NewGuid()), new FileId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, FileId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingFileOwnership_ItShouldFailIfNoFilesMatchFile()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileOwnership(testDatabase.NewContext());
                await this.CreateFileAsync(UserId, new FileId(Guid.NewGuid()), testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, FileId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingFileOwnership_ItShouldFailIfNoFilesMatchUser()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileOwnership(testDatabase.NewContext());
                await this.CreateFileAsync(new UserId(Guid.NewGuid()), FileId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.IsOwnerAsync(UserId, FileId);

                Assert.IsFalse(result);
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateFileAsync(UserId newUserId, FileId newFileId, TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = newUserId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = newFileId.Value;
            file.User = user;
            file.UserId = newFileId.Value;

            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Users.Add(user);
                databaseContext.Files.Add(file);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}