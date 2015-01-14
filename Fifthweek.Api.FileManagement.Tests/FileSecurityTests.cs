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
    public class FileSecurityTests : PersistenceTestsBase
    {
        [TestMethod]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            await this.InitializeWithDatabaseAsync();
            await this.CreateFileAsync(this.fileId, this.userId);
            await this.target.AssertFileBelongsToUserAsync(this.userId, this.fileId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            await this.InitializeWithDatabaseAsync();
            await this.CreateFileAsync(this.fileId, this.userId);
            await this.target.AssertFileBelongsToUserAsync(new UserId(Guid.NewGuid()), this.fileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AssertFileBelongsToUserAsync(null, this.fileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.AssertFileBelongsToUserAsync(this.userId, null);
        }

        private async Task InitializeWithoutDatabaseAsync()
        {
            this.target = new FileSecurity(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        private async Task InitializeWithDatabaseAsync()
        {
            this.Initialize();
            this.target = new FileSecurity(this.NewDbContext());

            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = this.userId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(creator);
                await dbContext.SaveChangesAsync();
            }
        }

        private async Task CreateFileAsync(FileId fileId, UserId userId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = userId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = fileId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Files.Add(file);
                await dbContext.SaveChangesAsync();
            }
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        private readonly UserId userId = new UserId(Guid.NewGuid());

        private readonly FileId fileId = new FileId(Guid.NewGuid());

        private FileSecurity target;
    }
}
