namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetBlogDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());
        
        private GetBlogDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetBlogDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenGettingBlog_ItShouldReturnTheResult()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId);

                Assert.AreEqual(BlogId, result.BlogId);
                Assert.AreEqual(CreatorId, result.CreatorId);
                Assert.AreEqual(HeaderFileId, result.HeaderImageFileId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGettingBlog_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);

                await this.target.ExecuteAsync(BlogId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(BlogId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingANonExistantBlog_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new BlogId(Guid.NewGuid()));

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            await this.CreateUserAsync(testDatabase);
            await this.CreateFileAsync(testDatabase);
            await this.CreateBlogAsync(testDatabase);
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = CreatorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateFileAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var file = FileTests.UniqueEntity(random);
            file.Id = HeaderFileId.Value;
            file.UserId = CreatorId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(file);
            }
        }

        private async Task CreateBlogAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var blog = BlogTests.UniqueEntity(random);
            blog.Id = BlogId.Value;
            blog.CreatorId = CreatorId.Value;
            blog.HeaderImageFileId = HeaderFileId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(blog);
            }
        }
    }
}