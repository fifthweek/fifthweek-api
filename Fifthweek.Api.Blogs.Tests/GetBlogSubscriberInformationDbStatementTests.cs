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
    public class GetBlogSubscriberInformationDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId Blog1Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog1ChannelIds = new[] { new ChannelId(Blog1Id.Value), new ChannelId(Guid.NewGuid()) };
        private static readonly BlogId Blog2Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog2ChannelIds = new[] { new ChannelId(Blog2Id.Value) };
        private static readonly BlogId Blog3Id = new BlogId(Guid.NewGuid());
        private static readonly ChannelId[] Blog3ChannelIds = new[] { new ChannelId(Blog3Id.Value), new ChannelId(Guid.NewGuid()), new ChannelId(Guid.NewGuid()) };
        private static readonly UserId Creator1Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator2Id = new UserId(Guid.NewGuid());
        private static readonly UserId Creator3Id = new UserId(Guid.NewGuid());
        private static readonly UserId UserId1 = new UserId(Guid.NewGuid());
        private static readonly Username Username1 = new Username("userone");
        private static readonly Email UserEmail1 = new Email("one@test.com");
        private static readonly FileId ProfileImageFileId1 = new FileId(Guid.NewGuid());
        private static readonly UserId UserId2 = new UserId(Guid.NewGuid());
        private static readonly Username Username2 = new Username("usertwo");
        private static readonly Email UserEmail2 = new Email("two@test.com");
        private static readonly UserId UserId3 = new UserId(Guid.NewGuid());
        private static readonly Username Username3 = new Username("userthree");
        private static readonly Email UserEmail3 = new Email("three@test.com");
        private static readonly IEnumerable<FreeAccessUser> InitialFreeAccessUsers = new List<FreeAccessUser> {
            new FreeAccessUser { BlogId = Blog1Id.Value, Email = UserEmail1.Value },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = UserEmail1.Value },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = UserEmail2.Value },
            new FreeAccessUser { BlogId = Blog2Id.Value, Email = UserEmail3.Value },
            new FreeAccessUser { BlogId = Blog3Id.Value, Email = UserEmail1.Value },
            new FreeAccessUser { BlogId = Blog3Id.Value, Email = UserEmail3.Value },
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

        private GetBlogSubscriberInformationDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetBlogSubscriberInformationDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
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
                this.target = new GetBlogSubscriberInformationDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new BlogId(Guid.NewGuid()));
                Assert.AreEqual(0, result.Subscribers.Count);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIdExists_ItShouldReturnTheBlogSubscriptions()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogSubscriberInformationDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Blog1Id);

                CollectionAssert.AreEquivalent(
                    new List<GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber>
                    {
                        new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            Username1, UserId1, ProfileImageFileId1, Blog1ChannelIds[0], SubscriptionStartDate, 10, UserEmail1),
                        new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            Username1, UserId1, ProfileImageFileId1, Blog1ChannelIds[1], SubscriptionStartDate, 10, UserEmail1),
                        new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            Username2, UserId2, null, Blog1ChannelIds[0], SubscriptionStartDate, 20, null),
                        new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            Username2, UserId2, null, Blog1ChannelIds[1], SubscriptionStartDate, 20, null),
                        new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                            Username3, UserId3, null, Blog1ChannelIds[1], SubscriptionStartDate, 30, null),
                    }, 
                    result.Subscribers.ToList());


                return ExpectedSideEffects.None;
            });
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

                blog2Channels[0].Price = Blog2Channel1CurrentPrice;
                blog2Channels[0].PriceLastSetDate = PriceLastSetDate;
                blog2Channels[0].IsVisibleToNonSubscribers = true;
                blog3Channels[0].Price = Blog3Channel1CurrentPrice;
                blog3Channels[0].PriceLastSetDate = PriceLastSetDate;
                blog3Channels[0].IsVisibleToNonSubscribers = false;
                blog3Channels[2].Price = Blog3Channel3CurrentPrice;
                blog3Channels[2].PriceLastSetDate = PriceLastSetDate;
                blog3Channels[2].IsVisibleToNonSubscribers = true;

                await databaseContext.SaveChangesAsync();
            }

            await this.CreateUserAsync(testDatabase, UserId1, UserEmail1, Username1);
            await this.CreateUserAsync(testDatabase, UserId2, UserEmail2, Username2);
            await this.CreateUserAsync(testDatabase, UserId3, UserEmail3, Username3);

            await this.CreateProfileImagesAsync(testDatabase, ProfileImageFileId1, UserId1);

            using (var connection = testDatabase.CreateConnection())
            {
                foreach (var item in InitialFreeAccessUsers)
                {
                    await connection.InsertAsync(item);
                }

                await connection.InsertAsync(new ChannelSubscription(blog1Channels[0].Id, null, UserId1.Value, null, 10, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog1Channels[1].Id, null, UserId1.Value, null, 10, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog2Channels[0].Id, null, UserId1.Value, null, 10, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog3Channels[0].Id, null, UserId1.Value, null, 10, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog3Channels[2].Id, null, UserId1.Value, null, 10, PriceLastAcceptedDate, SubscriptionStartDate));
            
                await connection.InsertAsync(new ChannelSubscription(blog1Channels[0].Id, null, UserId2.Value, null, 20, PriceLastAcceptedDate, SubscriptionStartDate));
                await connection.InsertAsync(new ChannelSubscription(blog1Channels[1].Id, null, UserId2.Value, null, 20, PriceLastAcceptedDate, SubscriptionStartDate));

                await connection.InsertAsync(new ChannelSubscription(blog1Channels[1].Id, null, UserId3.Value, null, 30, PriceLastAcceptedDate, SubscriptionStartDate));
            }
        }

        private async Task CreateProfileImagesAsync(TestDatabaseContext testDatabase, FileId fileId, UserId userId)
        {
            FifthweekUser user;
            using (var databaseContext = testDatabase.CreateContext())
            {
                user = databaseContext.Users.First(v => v.Id == userId.Value);
            }

            var profileImageFile = FileTests.UniqueEntity(this.random);
            profileImageFile.Id = fileId.Value;
            profileImageFile.User = user;
            profileImageFile.UserId = user.Id;

            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.Database.Connection.InsertAsync(profileImageFile);

                user.ProfileImageFile = profileImageFile;
                user.ProfileImageFileId = profileImageFile.Id;

                await databaseContext.Database.Connection.UpdateAsync(user, FifthweekUser.Fields.ProfileImageFileId);
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase, UserId userId, Email email, Username username)
        {
            var user = UserTests.UniqueEntity(this.random);
            user.Id = userId.Value;
            user.Email = email.Value;
            user.UserName = username.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}