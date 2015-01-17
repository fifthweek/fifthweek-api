namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class GetFileExtensionDbStatementTests : PersistenceTestsBase
    {
        private const string FileExtension = "jpeg";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private GetFileExtensionDbStatement target;

        [TestMethod]
        public async Task WhenFileExists_ItShouldReturnExtension()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetFileExtensionDbStatement(testDatabase.NewContext());
                await this.CreateFileAsync(UserId, FileId, FileExtension, testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var extension = await this.target.ExecuteAsync(FileId);

                Assert.AreEqual(extension, FileExtension);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenFileDoesNotExist_ItShouldThrowException()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new GetFileExtensionDbStatement(testDatabase.NewContext());
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.ExecuteAsync(FileId);

                await badMethodCall.AssertExceptionAsync<Exception>();
                
                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateFileAsync(UserId newUserId, FileId newFileId, string extension, TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = newUserId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = newFileId.Value;
            file.User = user;
            file.UserId = newFileId.Value;
            file.FileExtension = extension;

            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Users.Add(user);
                databaseContext.Files.Add(file);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
