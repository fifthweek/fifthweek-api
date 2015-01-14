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
            await this.target.AssertFileBelongsToUserAsync(this.userId, this.fileId);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            await this.InitializeWithDatabaseAsync();
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

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            await this.InitializeWithDatabaseAsync();
            var result = await this.target.CheckFileBelongsToUserAsync(this.userId, this.fileId);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            await this.InitializeWithDatabaseAsync();
            var result = await this.target.CheckFileBelongsToUserAsync(new UserId(Guid.NewGuid()), this.fileId);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.CheckFileBelongsToUserAsync(null, this.fileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            await this.InitializeWithoutDatabaseAsync();
            await this.target.CheckFileBelongsToUserAsync(this.userId, null);
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
            var user = UserTests.UniqueEntity(random);
            user.Id = this.userId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = this.fileId.Value;
            file.User = user;
            file.UserId = this.userId.Value;

            using (var dbContext = this.NewDbContext())
            {
                dbContext.Users.Add(user);
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
