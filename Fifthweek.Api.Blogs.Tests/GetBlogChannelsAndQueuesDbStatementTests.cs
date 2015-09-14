namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetBlogChannelsAndQueuesDbStatementTests : PersistenceTestsBase
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId1 = new QueueId(Guid.NewGuid());
        private static readonly QueueId QueueId2 = new QueueId(Guid.NewGuid());
        private static readonly QueueId QueueId3 = new QueueId(Guid.NewGuid());
        private static readonly DateTime CreationDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;
        private static readonly DateTime PriceLastSetDate = new SqlDateTime(DateTime.UtcNow.AddDays(-5)).Value;
        private static readonly DateTime QueueExclusiveLowerBound = new SqlDateTime(DateTime.UtcNow.AddDays(-100)).Value;
        private static readonly string Description = "description";
        private static readonly bool IsVisibleToNonSubscribers = true;
        private static readonly string Name = "name";
        private static readonly int Price = 10;
        private static readonly string ExternalVideoUrl = "url";
        private static readonly string Introduction = "introduction";
        private static readonly string Tagline = "tagline";

        private GetBlogChannelsAndQueuesDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetBlogChannelsAndQueuesDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
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
                this.target = new GetBlogChannelsAndQueuesDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(CreatorId);

                Assert.AreEqual(BlogId, result.Blog.BlogId);
                Assert.AreEqual(CreatorId, result.Blog.CreatorId);
                Assert.AreEqual(HeaderFileId, result.Blog.HeaderImageFileId);
                Assert.AreEqual(CreationDate, result.Blog.CreationDate);
                Assert.AreEqual(Description, result.Blog.Description.Value);
                Assert.AreEqual(ExternalVideoUrl, result.Blog.Video.Value);
                Assert.AreEqual(Introduction, result.Blog.Introduction.Value);
                Assert.AreEqual(Name, result.Blog.BlogName.Value);

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Channels.Count);

                foreach (var channel in result.Channels)
                {
                    Assert.AreEqual(IsVisibleToNonSubscribers, channel.IsVisibleToNonSubscribers);
                    Assert.AreEqual(Name, channel.Name);
                    Assert.AreEqual(Price, channel.Price);
                }

                Assert.AreEqual(3, result.Blog.Queues.Count);

                foreach (var collection in result.Blog.Queues)
                {
                    Assert.AreEqual(Name, collection.Name);
                }

                var queue1 = result.Blog.Queues.Single(v => v.QueueId.Equals(QueueId1));
                var queue2 = result.Blog.Queues.Single(v => v.QueueId.Equals(QueueId2));
                var queue3 = result.Blog.Queues.Single(v => v.QueueId.Equals(QueueId3));

                Assert.AreEqual(1, queue1.WeeklyReleaseSchedule.Count);
                Assert.AreEqual(2, queue2.WeeklyReleaseSchedule.Count);
                Assert.AreEqual(3, queue3.WeeklyReleaseSchedule.Count);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGettingBlog_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogChannelsAndQueuesDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);

                await this.target.ExecuteAsync(CreatorId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CreatorId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGettingANonExistantBlog_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetBlogChannelsAndQueuesDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new UserId(Guid.NewGuid()));

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private async Task CreateDataAsync(TestDatabaseContext testDatabase)
        {
            await this.CreateUserAsync(testDatabase);
            await this.CreateFileAsync(testDatabase);
            await this.CreateBlogAsync(testDatabase);
            await this.CreateChannelsAndQueuesAsync(testDatabase);
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
            blog.CreationDate = CreationDate;
            blog.Description = Description;
            blog.ExternalVideoUrl = ExternalVideoUrl;
            blog.Introduction = Introduction;
            blog.Name = Name;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(blog);
            }
        }

        private async Task CreateChannelsAndQueuesAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();

            var channel1 = ChannelTests.UniqueEntity(random);
            channel1.Id = ChannelId1.Value;
            ConfigureChannel(channel1);

            var channel2 = ChannelTests.UniqueEntity(random);
            channel2.Id = ChannelId2.Value;
            ConfigureChannel(channel2);

            var channel3 = ChannelTests.UniqueEntity(random);
            channel3.Id = ChannelId3.Value;
            ConfigureChannel(channel3);

            var queue1 = QueueTests.UniqueEntity(random);
            queue1.Id = QueueId1.Value;
            ConfigureQueue(queue1);

            var queue2 = QueueTests.UniqueEntity(random);
            queue2.Id = QueueId2.Value;
            ConfigureQueue(queue2);

            var queue3 = QueueTests.UniqueEntity(random);
            queue3.Id = QueueId3.Value;
            ConfigureQueue(queue3);

            var wrt1 = WeeklyReleaseTimeTests.UniqueEntity(random, queue1.Id);
            var wrt2a = WeeklyReleaseTimeTests.UniqueEntity(random, queue2.Id);
            var wrt2b = WeeklyReleaseTimeTests.UniqueEntity(random, queue2.Id);
            var wrt3a = WeeklyReleaseTimeTests.UniqueEntity(random, queue3.Id);
            var wrt3b = WeeklyReleaseTimeTests.UniqueEntity(random, queue3.Id);
            var wrt3c = WeeklyReleaseTimeTests.UniqueEntity(random, queue3.Id);

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(channel1);
                await connection.InsertAsync(channel2);
                await connection.InsertAsync(channel3);
                await connection.InsertAsync(queue1);
                await connection.InsertAsync(queue2);
                await connection.InsertAsync(queue3);
                await connection.InsertAsync(wrt1);
                await connection.InsertAsync(wrt2a);
                await connection.InsertAsync(wrt2b);
                await connection.InsertAsync(wrt3a);
                await connection.InsertAsync(wrt3b);
                await connection.InsertAsync(wrt3c);
            }
        }

        private static void ConfigureQueue(Queue queue)
        {
            queue.Name = Name;
            queue.BlogId = BlogId.Value;
        }

        private static void ConfigureChannel(Channel channel)
        {
            channel.BlogId = BlogId.Value;
            channel.CreationDate = CreationDate;
            channel.IsVisibleToNonSubscribers = IsVisibleToNonSubscribers;
            channel.Name = Name;
            channel.Price = Price;
            channel.PriceLastSetDate = PriceLastSetDate;
        }
    }
}