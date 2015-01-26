namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetFileWaitingForUploadDbStatementTests : PersistenceTestsBase
    {
        private const string FileNameWithoutExtension = "myfile";
        private const string FileExtension = "jpeg";
        private const string Purpose = "profile-picture";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private GetFileWaitingForUploadDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new GetFileWaitingForUploadDbStatement(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenGettingFileWaitingForUpload_ItShouldReturnTheFile()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetFileWaitingForUploadDbStatement(testDatabase.NewContext());

                    await this.CreateUserAsync(testDatabase);
                    await this.AddFileAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    var result = await this.target.ExecuteAsync(FileId);

                    Assert.AreEqual(FileId, result.FileId);
                    Assert.AreEqual(UserId, result.UserId);
                    Assert.AreEqual(FileNameWithoutExtension, result.FileNameWithoutExtension);
                    Assert.AreEqual(FileExtension, result.FileExtension);
                    Assert.AreEqual(Purpose, result.Purpose);

                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingANonExistantFileWaitingForUpload_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.target = new GetFileWaitingForUploadDbStatement(testDatabase.NewContext());

                    await this.CreateUserAsync(testDatabase);
                    await this.AddFileAsync(testDatabase);
                    await testDatabase.TakeSnapshotAsync();

                    await this.target.ExecuteAsync(new FileId(Guid.NewGuid()));
                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGettingFileWaitingForUpload_ItShouldRequireAFileId()
        {
            await this.target.ExecuteAsync(null);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task AddFileAsync(TestDatabaseContext testDatabase)
        {
            var file = new File(
                FileId.Value,
                null,
                UserId.Value,
                FileState.WaitingForUpload,
                DateTime.UtcNow,
                null,
                null,
                null,
                FileNameWithoutExtension,
                FileExtension,
                0,
                Purpose);

            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.Database.Connection.InsertAsync(file);
            }
        }
    }
}