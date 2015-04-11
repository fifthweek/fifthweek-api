namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class IsUserInFreeAccessListDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId1 = new BlogId(Guid.NewGuid());
        private static readonly BlogId BlogId2 = new BlogId(Guid.NewGuid());
        private static readonly BlogId BlogId3 = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId2 = new UserId(Guid.NewGuid());
        private static readonly UserId CreatorId3 = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Username Username = new Username("usertwo");
        private static readonly Email UserEmail = new Email("two@test2.com");
        private static readonly IEnumerable<FreeAccessUser> InitialFreeAccessUsers = new List<FreeAccessUser> {
            new FreeAccessUser { BlogId = BlogId1.Value, Email = "one@test.com" },
            new FreeAccessUser { BlogId = BlogId1.Value, Email = "two@test.com" },
            new FreeAccessUser { BlogId = BlogId1.Value, Email = "three@test.com" },
            new FreeAccessUser { BlogId = BlogId2.Value, Email = "one@test2.com" },
            new FreeAccessUser { BlogId = BlogId2.Value, Email = "two@test2.com" },
            new FreeAccessUser { BlogId = BlogId2.Value, Email = "three@test2.com" },
            new FreeAccessUser { BlogId = BlogId3.Value, Email = "one@test3.com" },
            new FreeAccessUser { BlogId = BlogId3.Value, Email = "two@test3.com" },
            new FreeAccessUser { BlogId = BlogId3.Value, Email = "three@test3.com" },
        };

        private readonly Random random = new Random();

        private DoesUserHaveFreeAccessDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new DoesUserHaveFreeAccessDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(BlogId1, null);
        }

        [TestMethod]
        public async Task WhenBlogIdDoesNotExist_ItShouldReturnFalse()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DoesUserHaveFreeAccessDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new BlogId(Guid.NewGuid()), UserId);
                Assert.IsFalse(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdDoesNotExist_ItShouldReturnFalse()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DoesUserHaveFreeAccessDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId2, new UserId(Guid.NewGuid()));
                Assert.IsFalse(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdExistsButIsNotInAnyFreeAccessList_ItShouldReturnFalse()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DoesUserHaveFreeAccessDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId2, CreatorId1);
                Assert.IsFalse(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdExistsButIsNotInFreeAccessListForBlog_ItShouldReturnFalse()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DoesUserHaveFreeAccessDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId1, UserId);
                Assert.IsFalse(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdExistsAndIsInFreeAccessListForBlog_ItShouldReturnTrue()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new DoesUserHaveFreeAccessDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId2, UserId);
                Assert.IsTrue(result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase, bool addEmailAddress)
        {
            await this.CreateCreatorAsync(testDatabase, CreatorId1);
            await this.CreateBlogAsync(testDatabase, CreatorId1, BlogId1);
            await this.CreateCreatorAsync(testDatabase, CreatorId2);
            await this.CreateBlogAsync(testDatabase, CreatorId2, BlogId2);
            await this.CreateCreatorAsync(testDatabase, CreatorId3);
            await this.CreateBlogAsync(testDatabase, CreatorId3, BlogId3);

            await this.CreateUserAsync(testDatabase);

            if (addEmailAddress)
            {
                using (var connection = testDatabase.CreateConnection())
                {
                    foreach (var item in InitialFreeAccessUsers)
                    {
                        await connection.InsertAsync(item);
                    }
                }
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var user = UserTests.UniqueEntity(this.random);
            user.Id = UserId.Value;
            user.Email = UserEmail.Value;
            user.UserName = Username.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateCreatorAsync(TestDatabaseContext testDatabase, UserId creatorId)
        {
            var user = UserTests.UniqueEntity(this.random);
            user.Id = creatorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateBlogAsync(TestDatabaseContext testDatabase, UserId creatorId, BlogId blogId)
        {
            var blog = BlogTests.UniqueEntity(this.random);
            blog.Id = blogId.Value;
            blog.CreatorId = creatorId.Value;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(blog);
            }
        }
    }
}