namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class FileRepositoryTests : PersistenceTestsBase
    {
        [TestMethod]
        public async Task WhenAddingANewFile_ItShouldUpdateTheDatabase()
        {
            await this.CreateUserAsync(this.userId);

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

        [TestInitialize]
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

        private async Task CreateUserAsync(UserId newUserId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(creator);
                await dbContext.SaveChangesAsync();
            }
        }

        private readonly string fileNameWithoutExtension = "myfile";

        private readonly string fileExtension = "jpeg";

        private readonly string purpose = "profile-picture";

        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private FileRepository target;
    }
}
