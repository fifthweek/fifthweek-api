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

        [TestInitialize]
        public void Initialize()
        {
            this.target = new FileSecurity(new Mock<IFifthweekDbContext>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new FileSecurity(testDatabase.NewContext());
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.AssertFileBelongsToUserAsync(UserId, FileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenAssertingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new FileSecurity(testDatabase.NewContext());
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.AssertFileBelongsToUserAsync(new UserId(Guid.NewGuid()), FileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            await this.target.AssertFileBelongsToUserAsync(null, FileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenAssertingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            await this.target.AssertFileBelongsToUserAsync(UserId, null);
        }

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldCompleteSuccessfullyIfThePermissionsAreOk()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new FileSecurity(testDatabase.NewContext());
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.CheckFileBelongsToUserAsync(UserId, FileId);
                Assert.IsTrue(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenCheckingAFileBelongsToAUser_ItShouldThrowAnExceptionIfThePermissionsAreViolated()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new FileSecurity(testDatabase.NewContext());
                await this.CreateFileAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.CheckFileBelongsToUserAsync(new UserId(Guid.NewGuid()), FileId);
                Assert.IsFalse(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_UserIdMustNotBeNull()
        {
            await this.target.CheckFileBelongsToUserAsync(null, FileId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCheckingAFileBelongsToAUser_FileIdMustNotBeNull()
        {
            await this.target.CheckFileBelongsToUserAsync(UserId, null);
        }

        private async Task CreateFileAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = UserId.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = FileId.Value;
            file.User = user;
            file.UserId = UserId.Value;

            using (var databaseContext = testDatabase.NewContext())
            {
                databaseContext.Users.Add(user);
                databaseContext.Files.Add(file);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}
