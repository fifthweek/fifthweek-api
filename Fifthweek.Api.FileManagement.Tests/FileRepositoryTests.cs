namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileRepositoryTests : PersistenceTestsBase
    {
        private readonly string fileNameWithoutExtension = "myfile";

        private readonly string fileExtension = "jpeg";

        private readonly string purpose = "profile-picture";

        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private FileRepository target;

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenAddingANewFile_ItShouldUpdateTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();

            await this.SnapshotDatabaseAsync();

            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, this.fileExtension, this.purpose);

            var expectedFile = new File(
                this.fileId.Value,
                null,
                this.userId.Value,
                FileState.WaitingForUpload,
                DateTime.MinValue,
                null,
                null,
                null,
                this.fileNameWithoutExtension,
                this.fileExtension,
                0,
                this.purpose);

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
            await this.InitializeWithDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, this.fileExtension, this.purpose);

            await this.SnapshotDatabaseAsync();

            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, this.fileExtension, this.purpose);
            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileId()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AddNewFileAsync(null, this.userId, this.fileNameWithoutExtension, this.fileExtension, this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAUserId()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, null, this.fileNameWithoutExtension, this.fileExtension, this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileNameWithoutExtension()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, this.userId, null, this.fileExtension, this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileExtension()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, null, this.purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAPurpose()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, this.fileExtension, null);
        }

        [TestMethod]
        public async Task WhenSetFileUploadCompleteCalled_ItShouldUpdateTheDatabase()
        {
            await this.InitializeWithDatabaseAsync();
            await this.target.AddNewFileAsync(this.fileId, this.userId, this.fileNameWithoutExtension, this.fileExtension, this.purpose);
            await this.SnapshotDatabaseAsync();

            // This is just to guarantee we get a different timestamp for when the upload completes.
            Thread.Sleep(1);

            var newLength = 11111L;

            await this.target.SetFileUploadComplete(this.fileId, newLength);

            var expectedFile = new File(
                this.fileId.Value,
                null,
                this.userId.Value,
                FileState.UploadComplete,
                DateTime.MinValue,
                null,
                null,
                null,
                this.fileNameWithoutExtension,
                this.fileExtension,
                newLength,
                this.purpose);

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
                var newFile = databaseContext.Files.Find(this.fileId.Value);
                Assert.IsTrue(newFile.UploadStartedDate < newFile.UploadCompletedDate);
                Assert.IsTrue((DateTime.UtcNow - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                Assert.IsTrue((newFile.UploadCompletedDate - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
            }
        }

        private async Task InitializeWithoutDatabaseAsync()
        {
            this.target = new FileRepository(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        private async Task InitializeWithDatabaseAsync()
        {
            this.Initialize();
            this.target = new FileRepository(this.NewDbContext());

            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = this.userId.Value;

            using (var databaseContext = this.NewDbContext())
            {
                databaseContext.Users.Add(creator);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
