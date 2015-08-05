namespace Fifthweek.GarbageCollection.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteFileDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());

        private DeleteFileDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.target = new DeleteFileDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenFileIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenDeletingFile_ItShouldRemoveTheRequestedPostFromTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeleteFileDbStatement(testDatabase);
                var file = await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId);

                return new ExpectedSideEffects
                {
                    Delete = file,
                };
            });
        }

        [TestMethod]
        public async Task WhenDeletingNonExistantFile_ItShouldNotModifyTheDatabase()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                target = new DeleteFileDbStatement(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(FileId.Random());

                return ExpectedSideEffects.None;
            });
        }

        private async Task<File> CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestUserAsync(UserId.Value);
                return await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);
            }
        }
    }
}