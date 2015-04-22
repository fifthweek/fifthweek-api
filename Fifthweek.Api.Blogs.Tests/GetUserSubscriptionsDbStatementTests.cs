namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserSubscriptionsDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId Blog1Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog1ChannelIds = new[] { new ChannelId(Blog1Id.Value), new ChannelId(Guid.NewGuid()) };
        private static readonly BlogId Blog2Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog2ChannelIds = new[] { new ChannelId(Blog2Id.Value) };
        private static readonly BlogId Blog3Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog3ChannelIds = new[] { new ChannelId(Blog3Id.Value), new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly FileId Creator1ProfileImageFileId = new FileId(Guid.NewGuid());
        private static readonly UserId Creator1Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator2Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator3Id = new UserId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Username Username = new Username("usertwo");
        private static readonly Email UserEmail = new Email("two@test.com");
        private static readonly IEnumerable<FreeAccessUser> InitialFreeAccessUsers = new List<FreeAccessUser> {
            new FreeAccessUser { BlogId = Blog1Id.Value, Email = "one@test.com" },
            new FreeAccessUser { BlogId = Blog1Id.Value, Email = "two@test.com" },
            new FreeAccessUser { BlogId = Blog1Id.Value, Email = "three@test.com" },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = "one@test.com" },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = "two@test.com" },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = "three@test.com" },
            new FreeAccessUser { BlogId = Blog3Id.Value, Email = "one@test.com" },
            new FreeAccessUser { BlogId = Blog3Id.Value, Email = "three@test.com" },
        };

        private static readonly DateTime PriceLastAcceptedDate = new SqlDateTime(DateTime.UtcNow.AddDays(-8)).Value;
        private static readonly DateTime PriceLastSetDate = new SqlDateTime(DateTime.UtcNow.AddDays(-9)).Value;
        private static readonly DateTime SubscriptionStartDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;

        private static readonly int Blog2Channel1CurrentPrice = 10;
        private static readonly int Blog3Channel1CurrentPrice = 10;
        private static readonly int Blog3Channel3CurrentPrice = 20;

        private static readonly int Blog2Channel1AcceptedPrice = 0;
        private static readonly int Blog3Channel1AcceptedPrice = 10;
        private static readonly int Blog3Channel3AcceptedPrice = 20;

        private readonly Random random = new Random();

        private GetUserSubscriptionsDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetUserSubscriptionsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task WhenUserIdDoesNotExist_ItShouldReturnAnEmptyResultList()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserSubscriptionsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new UserId(Guid.NewGuid()));
                Assert.AreEqual(0, result.Count);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdExists_ItShouldReturnTheBlogSubscriptions()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetUserSubscriptionsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId);

                Assert.AreEqual(3, result.Count);

                var blog1 = result.First(v => v.BlogId.Equals(Blog1Id));
                var blog2 = result.First(v => v.BlogId.Equals(Blog2Id));
                var blog3 = result.First(v => v.BlogId.Equals(Blog3Id));

                Assert.IsTrue(blog1.FreeAccess);
                Assert.IsTrue(blog2.FreeAccess);
                Assert.IsFalse(blog3.FreeAccess);

                Assert.AreEqual(0, blog1.Channels.Count);
                Assert.AreEqual(1, blog2.Channels.Count);
                Assert.AreEqual(2, blog3.Channels.Count);

                Assert.IsTrue(blog1.Name.Length > 0);
                Assert.IsTrue(blog2.Name.Length > 0);
                Assert.IsTrue(blog3.Name.Length > 0);

                Assert.AreEqual(Creator1Id, blog1.CreatorId);
                Assert.AreEqual(Creator2Id, blog2.CreatorId);
                Assert.AreEqual(Creator3Id, blog3.CreatorId);

                Assert.AreEqual(blog1.ProfileImageFileId, Creator1ProfileImageFileId);
                Assert.IsNull(blog2.ProfileImageFileId);
                Assert.IsNull(blog3.ProfileImageFileId);

                Assert.IsTrue(blog1.CreatorUsername.Value.Length > 0);
                Assert.IsTrue(blog2.CreatorUsername.Value.Length > 0);
                Assert.IsTrue(blog3.CreatorUsername.Value.Length > 0);

                var blog2Channel1 = blog2.Channels.First(v => v.ChannelId.Equals(Blog2ChannelIds[0]));
                var blog3Channel1 = blog3.Channels.First(v => v.ChannelId.Equals(Blog3ChannelIds[0]));
                var blog3Channel3 = blog3.Channels.First(v => v.ChannelId.Equals(Blog3ChannelIds[2]));

                Assert.IsTrue(blog2Channel1.IsDefault);
                Assert.IsTrue(blog3Channel1.IsDefault);
                Assert.IsFalse(blog3Channel3.IsDefault);

                Assert.IsTrue(blog2Channel1.Name.Length > 0);
                Assert.IsTrue(blog3Channel1.Name.Length > 0);
                Assert.IsTrue(blog3Channel3.Name.Length > 0);

                Assert.AreEqual(Blog2Channel1CurrentPrice, blog2Channel1.PriceInUsCentsPerWeek);
                Assert.AreEqual(Blog2Channel1AcceptedPrice, blog2Channel1.AcceptedPrice);

                Assert.AreEqual(Blog3Channel1CurrentPrice, blog3Channel1.PriceInUsCentsPerWeek);
                Assert.AreEqual(Blog3Channel1AcceptedPrice, blog3Channel1.AcceptedPrice);

                Assert.AreEqual(Blog3Channel3CurrentPrice, blog3Channel3.PriceInUsCentsPerWeek);
                Assert.AreEqual(Blog3Channel3AcceptedPrice, blog3Channel3.AcceptedPrice);

                Assert.AreEqual(PriceLastSetDate, blog2Channel1.PriceLastSetDate);
                Assert.AreEqual(PriceLastSetDate, blog3Channel1.PriceLastSetDate);
                Assert.AreEqual(PriceLastSetDate, blog3Channel3.PriceLastSetDate);

                Assert.AreEqual(SubscriptionStartDate, blog2Channel1.SubscriptionStartDate);
                Assert.AreEqual(SubscriptionStartDate, blog3Channel1.SubscriptionStartDate);
                Assert.AreEqual(SubscriptionStartDate, blog3Channel3.SubscriptionStartDate);

                return ExpectedSideEffects.None;
            });
        }

        private static void OrderResults(GetUserSubscriptionsResult result)
        {
            ((List<BlogSubscriptionStatus>)result.Blogs).Sort((a, b) => a.BlogId.Value.CompareTo(b.BlogId.Value));

            foreach (var blog in result.Blogs)
            {
                ((List<ChannelSubscriptionStatus>)blog.Channels).Sort((a, b) => a.ChannelId.Value.CompareTo(b.ChannelId.Value));
            }
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            IReadOnlyList<Channel> blog1Channels;
            IReadOnlyList<Channel> blog2Channels;
            IReadOnlyList<Channel> blog3Channels;
            using (var databaseContext = testDatabase.CreateContext())
            {
                blog1Channels = await databaseContext.CreateTestChannelsAsync(Creator1Id.Value, Blog1Id.Value, Blog1ChannelIds.Select(v => v.Value).ToArray());
                blog2Channels = await databaseContext.CreateTestChannelsAsync(Creator2Id.Value, Blog2Id.Value, Blog2ChannelIds.Select(v => v.Value).ToArray());
                blog3Channels = await databaseContext.CreateTestChannelsAsync(Creator3Id.Value, Blog3Id.Value, Blog3ChannelIds.Select(v => v.Value).ToArray());

                blog2Channels[0].PriceInUsCentsPerWeek = Blog2Channel1CurrentPrice;
                blog2Channels[0].PriceLastSetDate = PriceLastSetDate;
                blog3Channels[0].PriceInUsCentsPerWeek = Blog3Channel1CurrentPrice;
                blog3Channels[0].PriceLastSetDate = PriceLastSetDate;
                blog3Channels[2].PriceInUsCentsPerWeek = Blog3Channel3CurrentPrice;
                blog3Channels[2].PriceLastSetDate = PriceLastSetDate;

                await databaseContext.SaveChangesAsync();
            }

            await this.CreateUserAsync(testDatabase);

            await this.CreateProfileImagesAsync(testDatabase);

            using (var connection = testDatabase.CreateConnection())
            {
                foreach (var item in InitialFreeAccessUsers)
                {
                    await connection.InsertAsync(item);
                }

                await connection.InsertAsync(new ChannelSubscription(blog2Channels[0].Id, null, UserId.Value, null, Blog2Channel1AcceptedPrice, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog3Channels[0].Id, null, UserId.Value, null, Blog3Channel1AcceptedPrice, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog3Channels[2].Id, null, UserId.Value, null, Blog3Channel3AcceptedPrice, PriceLastAcceptedDate, SubscriptionStartDate));
            }
        }

        private async Task CreateProfileImagesAsync(TestDatabaseContext testDatabase)
        {
            FifthweekUser creator1;
            using (var databaseContext = testDatabase.CreateContext())
            {
                creator1 = databaseContext.Users.First(v => v.Id == Creator1Id.Value);
            }

            var profileImageFile = FileTests.UniqueEntity(this.random);
            profileImageFile.Id = Creator1ProfileImageFileId.Value;
            profileImageFile.User = creator1;
            profileImageFile.UserId = creator1.Id;

            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.Database.Connection.InsertAsync(profileImageFile);

                creator1.ProfileImageFile = profileImageFile;
                creator1.ProfileImageFileId = profileImageFile.Id;

                await databaseContext.Database.Connection.UpdateAsync(creator1, FifthweekUser.Fields.ProfileImageFileId);
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
    }
}