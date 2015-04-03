namespace Fifthweek.WebJobs.Thumbnails.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SetFileProcessingCompleteDbStatementTests : PersistenceTestsBase
    {
        private const string FileNameWithoutExtension = "myfile";
        private const string FileExtension = "jpeg";
        private const string Purpose = "profile-picture";
        private const long FileLength = 11111L;
        private const int ProcessingAttempts = 2;
        private const int RenderWidth = 1000;
        private const int RenderHeight = 800;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly DateTime ProcessingStartedTimeStamp = new SqlDateTime(DateTime.UtcNow.AddSeconds(-10)).Value;
        private static readonly DateTime ProcessingCompleteTimeStamp = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly DateTime UploadStartedTimeStamp = new SqlDateTime(DateTime.UtcNow.AddSeconds(-180)).Value;
        private static readonly DateTime UploadCompleteTimeStamp = new SqlDateTime(DateTime.UtcNow.AddSeconds(-100)).Value;

        private SetFileProcessingCompleteDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new SetFileProcessingCompleteDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenSetFileProcessingCompleteCalled_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetFileProcessingCompleteDbStatement(testDatabase);

                await this.CreateUserAsync(testDatabase);
                await this.AddFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                const long NewLength = 11111L;

                await this.target.ExecuteAsync(FileId, ProcessingAttempts, ProcessingStartedTimeStamp, ProcessingCompleteTimeStamp, RenderWidth, RenderWidth);

                var expectedFile = new File(
                    FileId.Value,
                    null,
                    UserId.Value,
                    FileState.ProcessingComplete,
                    UploadStartedTimeStamp,
                    UploadCompleteTimeStamp,
                    ProcessingStartedTimeStamp,
                    ProcessingCompleteTimeStamp,
                    ProcessingAttempts,
                    FileNameWithoutExtension,
                    FileExtension,
                    NewLength,
                    Purpose,
                    RenderWidth,
                    RenderWidth);

                //using (var databaseContext = testDatabase.CreateContext())
                //{
                //    var newFile = databaseContext.Files.Find(FileId.Value);
                //    Assert.IsTrue(newFile.UploadStartedDate < newFile.ProcessingCompletedDate);
                //    Assert.IsTrue((DateTime.UtcNow - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                //    Assert.IsTrue((newFile.ProcessingCompletedDate - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                //}

                return new ExpectedSideEffects
                {
                    Update = expectedFile,
                };
            });
        }

        [TestMethod]
        public async Task WhenSetFileProcessingCompleteCalledTwice_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new SetFileProcessingCompleteDbStatement(testDatabase);

                await this.CreateUserAsync(testDatabase);
                await this.AddFileAsync(testDatabase);

                await this.target.ExecuteAsync(FileId, ProcessingAttempts, ProcessingStartedTimeStamp, ProcessingCompleteTimeStamp, RenderWidth, RenderWidth);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId, ProcessingAttempts, ProcessingStartedTimeStamp, ProcessingCompleteTimeStamp, RenderWidth, RenderWidth);
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSetFileProcessingCompleteCalled_ItShouldRequireAFileId()
        {
            await this.target.ExecuteAsync(null, ProcessingAttempts, ProcessingStartedTimeStamp, ProcessingCompleteTimeStamp, RenderWidth, RenderWidth);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            using (var databaseContext = testDatabase.CreateContext())
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
                FileState.UploadComplete,
                UploadStartedTimeStamp,
                UploadCompleteTimeStamp,
                null,
                null,
                null,
                FileNameWithoutExtension,
                FileExtension,
                FileLength,
                Purpose,
                null,
                null);

            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.Database.Connection.InsertAsync(file);
            }
        }
    }
}