namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetFreeAccessUsersDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId1 = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId1 = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());

        private static readonly BlogId BlogId2 = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId2 = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId4 = new ChannelId(Guid.NewGuid());

        private static readonly UserId UserId1 = new UserId(Guid.NewGuid());
        private static readonly Username Username1 = new Username("userone");
        private static readonly Email UserEmail1 = new Email("one@test2.com");
        private static readonly UserId UserId2 = new UserId(Guid.NewGuid());
        private static readonly Username Username2 = new Username("usertwo");
        private static readonly Email UserEmail2 = new Email("two@test2.com");
        private static readonly IEnumerable<FreeAccessUser> InitialFreeAccessUsers = new List<FreeAccessUser> {
            new FreeAccessUser { BlogId = BlogId1.Value, Email = UserEmail1.Value },
            new FreeAccessUser { BlogId = BlogId1.Value, Email = UserEmail2.Value },
            new FreeAccessUser { BlogId = BlogId1.Value, Email = "three@test2.com" },
        };

        private readonly Random random = new Random();

        private GetFreeAccessUsersDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetFreeAccessUsersDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenBlogIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenBlogIdDoesNotExist_ItShouldReturnAnEmptyResultList()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetFreeAccessUsersDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new BlogId(Guid.NewGuid()));
                Assert.AreEqual(0, result.FreeAccessUsers.Count);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenBlogIdExists_ItShouldReturnTheUserForTheBlog()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetFreeAccessUsersDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(BlogId1);
                Assert.AreEqual(3, result.FreeAccessUsers.Count);

                var user1 = result.FreeAccessUsers.FirstOrDefault(v => v.Email.Value == "one@test2.com");
                var user2 = result.FreeAccessUsers.FirstOrDefault(v => v.Email.Value == "two@test2.com");
                var user3 = result.FreeAccessUsers.FirstOrDefault(v => v.Email.Value == "three@test2.com");

                Assert.IsNotNull(user1);
                Assert.IsNotNull(user2);
                Assert.IsNotNull(user3);

                Assert.AreEqual(UserId1, user1.UserId);
                Assert.AreEqual(Username1, user1.Username);
                Assert.AreEqual(0, user1.ChannelIds.Count);
                
                Assert.AreEqual(UserId2, user2.UserId);
                Assert.AreEqual(Username2, user2.Username);
                Assert.AreEqual(2, user2.ChannelIds.Count);
                Assert.IsTrue(user2.ChannelIds.Contains(ChannelId1));
                Assert.IsTrue(user2.ChannelIds.Contains(ChannelId2));

                Assert.IsNull(user3.UserId);
                Assert.IsNull(user3.Username);
                Assert.AreEqual(0, user3.ChannelIds.Count);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase, bool addEmailAddress)
        {

            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelsAsync(
                    CreatorId1.Value,
                    BlogId1.Value,
                    ChannelId1.Value,
                    ChannelId2.Value);

                await databaseContext.CreateTestChannelsAsync(
                    CreatorId2.Value,
                    BlogId2.Value,
                    ChannelId3.Value,
                    ChannelId4.Value);
            }

            await this.CreateUsersAsync(testDatabase);

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

        private async Task CreateUsersAsync(TestDatabaseContext testDatabase)
        {
            var user1 = UserTests.UniqueEntity(this.random);
            user1.Id = UserId1.Value;
            user1.Email = UserEmail1.Value;
            user1.UserName = Username1.Value;
            
            var user2 = UserTests.UniqueEntity(this.random);
            user2.Id = UserId2.Value;
            user2.Email = UserEmail2.Value;
            user2.UserName = Username2.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user1);
                databaseContext.Users.Add(user2);
                await databaseContext.SaveChangesAsync();

                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(UserId2.Value, ChannelId1.Value);
                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(UserId2.Value, ChannelId2.Value);
                await databaseContext.CreateTestChannelSubscriptionWithExistingReferences(UserId2.Value, ChannelId3.Value);
            }
        }
    }
}