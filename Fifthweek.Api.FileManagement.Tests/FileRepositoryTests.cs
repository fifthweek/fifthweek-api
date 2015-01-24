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

        [TestInitialize]
        public void Initialize()
        {
            this.target = new FileRepository(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenAddingANewFile_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileRepository(testDatabase.NewContext());
                await this.CreateUserAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

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

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<File>(expectedFile)
                    {
                        Expected = actualFile =>
                        { 
                            expectedFile.UploadStartedDate = actualFile.UploadStartedDate;
                            return expectedFile;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenAddingANewFileTwice_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileRepository(testDatabase.NewContext());
                await this.CreateUserAsync(testDatabase);
                await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
                await testDatabase.TakeSnapshotAsync();

                await target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileId()
        {
            await this.target.AddNewFileAsync(null, UserId, FileNameWithoutExtension, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAUserId()
        {
            await this.target.AddNewFileAsync(FileId, null, FileNameWithoutExtension, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileNameWithoutExtension()
        {
            await this.target.AddNewFileAsync(FileId, UserId, null, FileExtension, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAFileExtension()
        {
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, null, Purpose);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAddingANewFile_ItShouldRequireAPurpose()
        {
            await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, null);
        }

        [TestMethod]
        public async Task WhenSetFileUploadCompleteCalled_ItShouldUpdateTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileRepository(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
                await testDatabase.TakeSnapshotAsync();

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

                using (var databaseContext = testDatabase.NewContext())
                {
                    var newFile = databaseContext.Files.Find(FileId.Value);
                    Assert.IsTrue(newFile.UploadStartedDate < newFile.UploadCompletedDate);
                    Assert.IsTrue((DateTime.UtcNow - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                    Assert.IsTrue((newFile.UploadCompletedDate - newFile.UploadStartedDate) < TimeSpan.FromMinutes(5));
                }

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<File>(expectedFile)
                    {
                        Expected = actualFile =>
                        {
                            expectedFile.UploadStartedDate = actualFile.UploadStartedDate;
                            expectedFile.UploadCompletedDate = actualFile.UploadCompletedDate;
                            return expectedFile;
                        }
                    }
                };
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenSetFileUploadCompleteCalled_ItShouldRequireAFileId()
        {
            await this.target.SetFileUploadComplete(null, 123);
        }

        [TestMethod]
        public async Task WhenGettingFileWaitingForUpload_ItShouldReturnTheFile()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new FileRepository(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.GetFileWaitingForUploadAsync(FileId);

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
                this.target = new FileRepository(testDatabase.NewContext());

                await this.CreateUserAsync(testDatabase);
                await this.target.AddNewFileAsync(FileId, UserId, FileNameWithoutExtension, FileExtension, Purpose);
                await testDatabase.TakeSnapshotAsync();

                await this.target.GetFileWaitingForUploadAsync(new FileId(Guid.NewGuid()));
                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenGettingFileWaitingForUpload_ItShouldRequireAFileId()
        {
            await this.target.GetFileWaitingForUploadAsync(null);
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
    }
}
