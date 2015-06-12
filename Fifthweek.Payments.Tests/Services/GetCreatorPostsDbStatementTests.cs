namespace Fifthweek.Payments.Tests.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Snapshots;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatorPostsDbStatementTests : PersistenceTestsBase
    {
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private static readonly UserId CreatorId1 = UserId.Random();
        private static readonly ChannelId ChannelId1 = ChannelId.Random();
        private static readonly ChannelId ChannelId2 = ChannelId.Random();
        private static readonly PostId PostId1 = new PostId(Guid.NewGuid());
        private static readonly PostId PostId2 = new PostId(Guid.NewGuid());
        private static readonly DateTime PostDate1 = Now;
        private static readonly DateTime PostDate2 = Now.AddDays(2);

        private GetCreatorPostsDbStatement target;

        [TestInitialize]
        public void Test()
        {
            this.target = new GetCreatorPostsDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
        }

        [TestMethod]
        public async Task ItShouldReturnResultsAllPostsMatchingChannelsAndTimeRange()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPostsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1, ChannelId2 }, PostDate1, PostDate2.AddSeconds(1));

                Assert.AreEqual(2, result.Count);

                Assert.AreEqual(ChannelId1, result[0].ChannelId);
                Assert.AreEqual(PostDate1, result[0].LiveDate);
                Assert.AreEqual(ChannelId2, result[1].ChannelId);
                Assert.AreEqual(PostDate2, result[1].LiveDate);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnResultsAllPostsMatchingChannelsAndTimeRange2()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPostsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1, ChannelId2 }, PostDate1, PostDate2);

                Assert.AreEqual(1, result.Count);

                Assert.AreEqual(ChannelId1, result[0].ChannelId);
                Assert.AreEqual(PostDate1, result[0].LiveDate);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnResultsAllPostsMatchingChannelsAndTimeRange3()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPostsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1, ChannelId2 }, PostDate1.AddSeconds(1), PostDate2);

                Assert.AreEqual(0, result.Count);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnResultsAllPostsMatchingChannelsAndTimeRange4()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPostsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId1 }, PostDate1, PostDate2.AddSeconds(1));

                Assert.AreEqual(1, result.Count);

                Assert.AreEqual(ChannelId1, result[0].ChannelId);
                Assert.AreEqual(PostDate1, result[0].LiveDate);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldReturnResultsAllPostsMatchingChannelsAndTimeRange5()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetCreatorPostsDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new List<ChannelId> { ChannelId2 }, PostDate1, PostDate2.AddSeconds(1));

                Assert.AreEqual(1, result.Count);

                Assert.AreEqual(ChannelId2, result[0].ChannelId);
                Assert.AreEqual(PostDate2, result[0].LiveDate);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var creator = UserTests.UniqueEntity(random);
                creator.Id = CreatorId1.Value;

                var subscription = BlogTests.UniqueEntity(random);
                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;

                var channel1 = ChannelTests.UniqueEntity(random);
                channel1.Id = ChannelId1.Value;
                channel1.Blog = subscription;
                channel1.BlogId = subscription.Id;

                var channel2 = ChannelTests.UniqueEntity(random);
                channel2.Id = ChannelId2.Value;
                channel2.Blog = subscription;
                channel2.BlogId = subscription.Id;

                var post1 = PostTests.UniqueNote(random);
                post1.Id = PostId1.Value;
                post1.LiveDate = PostDate1;
                post1.Channel = channel1;
                post1.ChannelId = channel1.Id;

                var post2 = PostTests.UniqueNote(random);
                post2.Id = PostId2.Value;
                post2.LiveDate = PostDate2;
                post2.Channel = channel2;
                post2.ChannelId = channel2.Id;

                databaseContext.Posts.Add(post1);
                databaseContext.Posts.Add(post2);

                await databaseContext.SaveChangesAsync();
            }
        }
    }
}