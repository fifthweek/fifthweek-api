namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileRepositoryTests : PersistenceTestsBase
    {
        private const string FileNameWithoutExtension = "myfile";
        private const string FileExtension = "jpeg";
        private const string Purpose = "profile-picture";
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private FileRepository target;

        public void InitializeWithoutDatabase()
        {
            this.target = new FileRepository(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.target = new FileRepository(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenAddingANewFile_ItShouldUpdateTheDatabase()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateUserAsync();
            await this.SnapshotDatabaseAsync();

            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);

            var expectedFile = new File(
                FileId.Value,
                null,
                UserId.Value,
                FileState.WaitingForUpload,
                DateTime.MinValue,
                null,
                null,
                null,
                FileNameWithoutExtension,
                FileExtension,
                0,
                Purpose);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<File>(expectedFile)
                {
                    AreEqual = actualFile =>
                    { 
                        expectedFile.UploadStartedDate = actualFile.UploadStartedDate;
                        return object.Equals(expectedFile, actualFile);
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenAddingANewFileTwice_ItShouldHaveNoEffect()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateUserAsync();
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
            await this.SnapshotDatabaseAsync();

            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
            
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileId()
        {
            this.InitializeWithoutDatabase();
            await this.target.AddNewFileAsync(null, UserId, FileNameWithoutExtension, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAUserId()
        {
            this.InitializeWithoutDatabase();
            await this.target.AddNewFileAsync(FileId, null, FileNameWithoutExtension, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileNameWithoutExtension()
        {
            this.InitializeWithoutDatabase();
            await this.target.AddNewFileAsync(FileId, UserId, null, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileExtension()
        {
            this.InitializeWithoutDatabase();
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, null, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAPurpose()
        {
            this.InitializeWithoutDatabase();
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, null);
        }

        [TestMethod]
        public async Task WhenSetFileUploadCompleteCalled_ItShouldUpdateTheDatabase()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateUserAsync();
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
            await this.SnapshotDatabaseAsync();

            // This is just to guarantee we get a different timestamp for when the upload completes.
            Thread.Sleep(1);

            const long NewLength = 11111L;

            await this.target.SetFileUploadComplete(FileId, NewLength);

            var expectedFile = new File(
                FileId.Value,
                null,
                UserId.Value,
                FileState.UploadComplete,
                DateTime.MinValue,
                null,
                null,
                null,
                FileNameWithoutExtension,
                FileExtension,
                NewLength,
                Purpose);

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Update = new WildcardEntity<File>(expectedFile)
                {
                    AreEqual = actualFile =>
                    {
                        expectedFile.UploadStartedDate = actualFile.UploadStartedDate;
                        expectedFile.UploadCompletedDate = actualFile.UploadCompletedDate;
                        return object.Equals(expectedFile, actualFile);
                    }
                }
            });

            using (var databaseContext = this.NewDbContext())
            {
                var newFile = databaseContext.Files.Find(FileId.Value);
                Assert.IsTrue(newFile.UploadStartedDate < newFile.UploadCompletedDate);
                Assert.IsTrue((DateTime.UtcNow - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                Assert.IsTrue((newFile.UploadCompletedDate - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
            }
        }

        private async Task CreateUserAsync()
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            using (var databaseContext = this.NewDbContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
