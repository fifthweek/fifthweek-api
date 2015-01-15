namespace Fifthweek.Api.FileManagement.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class FileSecurityTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private FileSecurity target;

        public void InitializeWithoutDatabase()
        {
            this.target = new FileSecurity(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        public override void Initialize()
        {
            base.Initialize();
            this.target = new FileSecurity(this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateFileAsync();
            await this.SnapshotDatabaseAsync();

            await this.target.AssertFileBelongsToUserAsync(UserId, FileId);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateFileAsync();
            await this.SnapshotDatabaseAsync();

            await this.target.AssertFileBelongsToUserAsync(new UserId(Guid.NewGuid()), FileId);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            this.InitializeWithoutDatabase();
            await this.target.AssertFileBelongsToUserAsync(null, FileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            this.InitializeWithoutDatabase();
            await this.target.AssertFileBelongsToUserAsync(UserId, null);
        }

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateFileAsync();
            await this.SnapshotDatabaseAsync();

            var result = await this.target.CheckFileBelongsToUserAsync(UserId, FileId);
            Assert.IsTrue(result);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            this.Initialize();
            await this.InitializeDatabaseAsync();
            await this.CreateFileAsync();
            await this.SnapshotDatabaseAsync();

            var result = await this.target.CheckFileBelongsToUserAsync(new UserId(Guid.NewGuid()), FileId);
            Assert.IsFalse(result);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            this.InitializeWithoutDatabase();
            await this.target.CheckFileBelongsToUserAsync(null, FileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            this.InitializeWithoutDatabase();
            await this.target.CheckFileBelongsToUserAsync(UserId, null);
        }

        private async Task CreateFileAsync()
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = FileId.Value;
            file.User = user;
            file.UserId = UserId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(user);
                dbContext.Files.Add(file);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}
