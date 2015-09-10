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

                var result = await this.target.ExecuteAsync(BlogId);

                Assert.AreEqual(BlogId, result.Blog.BlogId);
                Assert.AreEqual(CreatorId, result.Blog.CreatorId);
                Assert.AreEqual(HeaderFileId, result.Blog.HeaderImageFileId);
                Assert.AreEqual(CreationDate, result.Blog.CreationDate);
                Assert.AreEqual(Description, result.Blog.Description.Value);
                Assert.AreEqual(ExternalVideoUrl, result.Blog.Video.Value);
                Assert.AreEqual(Introduction, result.Blog.Introduction.Value);
                Assert.AreEqual(Name, result.Blog.BlogName.Value);
                Assert.AreEqual(Tagline, result.Blog.Tagline.Value);

                Assert.IsNotNull(result);
                Assert.AreEqual(3, result.Channels.Count);

                var channel1 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId1));
                var channel2 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId2));
                var channel3 = result.Channels.Single(v => v.ChannelId.Equals(ChannelId3));

                foreach (var channel in result.Channels)
                {
                    Assert.AreEqual(Description, channel.Description);
                    Assert.AreEqual(IsVisibleToNonSubscribers, channel.IsVisibleToNonSubscribers);
                    Assert.AreEqual(Name, channel.Name);
                    Assert.AreEqual(Price, channel.Price);
                }

                Assert.AreEqual(0, channel1.Collections.Count);
                Assert.AreEqual(1, channel2.Collections.Count);
                Assert.AreEqual(2, channel3.Collections.Count);

                foreach (var collection in channel2.Collections.Concat(channel3.Collections))
                {
                    Assert.AreEqual(Name, collection.Name);
                }

                var collection1 = channel2.Collections.Single(v => v.QueueId.Equals(QueueId1));
                var collection2 = channel3.Collections.Single(v => v.QueueId.Equals(QueueId2));
                var collection3 = channel3.Collections.Single(v => v.QueueId.Equals(QueueId3));

                Assert.AreEqual(1, collection1.WeeklyReleaseSchedule.Count);
                Assert.AreEqual(2, collection2.WeeklyReleaseSchedule.Count);
                Assert.AreEqual(3, collection3.WeeklyReleaseSchedule.Count);

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
                this.target = new GetBlogChannelsAndQueuesDbStatement(testDatabase);

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
            await this.CreateChannelsAndCollectionsAsync(testDatabase);
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
            blog.Tagline = Tagline;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(blog);
            }
        }

        private async Task CreateChannelsAndCollectionsAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();

            // First channel has no collections.
            var channel1 = ChannelTests.UniqueEntity(random);
            channel1.Id = ChannelId1.Value;
            ConfigureChannel(channel1);

            // Second channel has one collection.
            var channel2 = ChannelTests.UniqueEntity(random);
            channel2.Id = ChannelId2.Value;
            ConfigureChannel(channel2);

            var collection1 = QueueTests.UniqueEntity(random);
            collection1.Id = QueueId1.Value;
            collection1.Channel = channel2;
            collection1.ChannelId = channel2.Id;
            ConfigureCollection(collection1);

            // Third channel has two collections.
            var channel3 = ChannelTests.UniqueEntity(random);
            channel3.Id = ChannelId3.Value;
            ConfigureChannel(channel3);

            var collection2 = QueueTests.UniqueEntity(random);
            collection2.Id = QueueId2.Value;
            collection2.Channel = channel3;
            collection2.ChannelId = channel3.Id;
            ConfigureCollection(collection2);

            var collection3 = QueueTests.UniqueEntity(random);
            collection3.Id = QueueId3.Value;
            collection3.Channel = channel3;
            collection3.ChannelId = channel3.Id;
            ConfigureCollection(collection3);

            var wrt1 = WeeklyReleaseTimeTests.UniqueEntity(random, collection1.Id);
            var wrt2a = WeeklyReleaseTimeTests.UniqueEntity(random, collection2.Id);
            var wrt2b = WeeklyReleaseTimeTests.UniqueEntity(random, collection2.Id);
            var wrt3a = WeeklyReleaseTimeTests.UniqueEntity(random, collection3.Id);
            var wrt3b = WeeklyReleaseTimeTests.UniqueEntity(random, collection3.Id);
            var wrt3c = WeeklyReleaseTimeTests.UniqueEntity(random, collection3.Id);

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(channel1);
                await connection.InsertAsync(channel2);
                await connection.InsertAsync(channel3);
                await connection.InsertAsync(collection1);
                await connection.InsertAsync(collection2);
                await connection.InsertAsync(collection3);
                await connection.InsertAsync(wrt1);
                await connection.InsertAsync(wrt2a);
                await connection.InsertAsync(wrt2b);
                await connection.InsertAsync(wrt3a);
                await connection.InsertAsync(wrt3b);
                await connection.InsertAsync(wrt3c);
            }
        }

        private static void ConfigureCollection(Queue queue)
        {
            queue.Name = Name;
            queue.QueueExclusiveLowerBound = QueueExclusiveLowerBound;
        }

        private static void ConfigureChannel(Channel channel)
        {
            channel.BlogId = BlogId.Value;
            channel.CreationDate = CreationDate;
            channel.Description = Description;
            channel.IsVisibleToNonSubscribers = IsVisibleToNonSubscribers;
            channel.Name = Name;
            channel.Price = Price;
            channel.PriceLastSetDate = PriceLastSetDate;
        }
    }
}