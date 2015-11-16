namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestFreePostDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly int MaximumPosts = 2;

        private static readonly UserId RequestorId = UserId.Random();
        private static readonly PostId PostId1 = PostId.Random();
        private static readonly PostId PostId2 = PostId.Random();
        private static readonly PostId PostId3 = PostId.Random();

        private RequestFreePostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Required for non-database tests.
            this.target = new RequestFreePostDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenRequestorIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, PostId1, Now, MaximumPosts);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(RequestorId, null, Now, MaximumPosts);
        }

        [TestMethod]
        public async Task WhenInsertSucceeds_ItShouldReturnTrue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RequestFreePostDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();
                
                var result = await this.target.ExecuteAsync(RequestorId, PostId1, Now, MaximumPosts);
                Assert.IsTrue(result);
      
                return new ExpectedSideEffects
                {
                    Insert = new FreePost(RequestorId.Value, PostId1.Value, null, Now),
                };
            });
        }

        [TestMethod]
        public async Task WhenInsertFailsWithDuplicateKey_ItShouldReturnTrue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RequestFreePostDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);

                using (var connection = testDatabase.CreateConnection())
                {
                    await connection.InsertAsync(new FreePost(RequestorId.Value, PostId1.Value, null, Now));
                }
                
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(RequestorId, PostId1, Now, MaximumPosts);
                Assert.IsTrue(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenInsertFailsBecauseMaximumPostsReached_ItShouldReturnFalse()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RequestFreePostDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);

                await testDatabase.TakeSnapshotAsync();

                Assert.IsTrue(await this.target.ExecuteAsync(RequestorId, PostId1, Now, MaximumPosts));
                Assert.IsTrue(await this.target.ExecuteAsync(RequestorId, PostId2, Now, MaximumPosts));
                Assert.IsFalse(await this.target.ExecuteAsync(RequestorId, PostId3, Now, MaximumPosts));

                return new ExpectedSideEffects
                {
                    Inserts = new List<IIdentityEquatable>
                    {
                        new FreePost(RequestorId.Value, PostId1.Value, null, Now),
                        new FreePost(RequestorId.Value, PostId2.Value, null, Now),
                    },
                };
            });
        }

        [TestMethod]
        public async Task WhenInsertFailsBecauseOtherError_ItShouldRaiseError()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new RequestFreePostDbStatement(testDatabase);
                await this.CreateDataAsync(testDatabase);
                
                await testDatabase.TakeSnapshotAsync();

                await ExpectedException.AssertExceptionAsync<Exception>(
                    () => this.target.ExecuteAsync(RequestorId, PostId.Random(), Now, MaximumPosts));

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = RequestorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);

                await databaseContext.CreateTestNoteAsync(Guid.NewGuid(), PostId1.Value, random);
                await databaseContext.CreateTestNoteAsync(Guid.NewGuid(), PostId2.Value, random);
                await databaseContext.CreateTestNoteAsync(Guid.NewGuid(), PostId3.Value, random);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}