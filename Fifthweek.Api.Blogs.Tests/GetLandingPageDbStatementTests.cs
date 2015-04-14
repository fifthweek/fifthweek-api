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
    public class GetLandingPageDbStatementTests : PersistenceTestsBase
    {
        private static readonly Username Username = new Username(Guid.NewGuid().ToString());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());
        private static readonly DateTime CreationDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;
        private static readonly DateTime PriceLastSetDate = new SqlDateTime(DateTime.UtcNow.AddDays(-5)).Value;
        private static readonly string Description = "description";
        private static readonly bool IsVisibleToNonSubscribers = true;
        private static readonly string Name = "name";
        private static readonly int PriceInUsCentsPerWeek = 10;
        private static readonly string ExternalVideoUrl = "url";
        private static readonly string Introduction = "introduction";
        private static readonly string Tagline = "tagline";

        private GetLandingPageDbStatement target;

        [TestInitialize]
        public void TestInitialize()
        {
            this.target = new GetLandingPageDbStatement(new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict).Object);
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
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username);

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

                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId1)));
                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId2)));
                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId3)));

                foreach (var channel in result.Channels)
                {
                    Assert.AreEqual(Description, channel.Description);
                    Assert.AreEqual(IsVisibleToNonSubscribers, channel.IsVisibleToNonSubscribers);
                    Assert.AreEqual(Name, channel.Name);
                    Assert.AreEqual(PriceInUsCentsPerWeek, channel.PriceInUsCentsPerWeek);
                }

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenGettingBlog_ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);

                await this.target.ExecuteAsync(Username);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(Username);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public async Task WhenGettingANonExistantBlog_ItShouldThrowAnException()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(new Username(Guid.NewGuid().ToString()));

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
            user.UserName = Username.Value;

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

            // Third channel has two collections.
            var channel3 = ChannelTests.UniqueEntity(random);
            channel3.Id = ChannelId3.Value;
            ConfigureChannel(channel3);

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(channel1);
                await connection.InsertAsync(channel2);
                await connection.InsertAsync(channel3);
            }
        }

        private static void ConfigureChannel(Channel channel)
        {
            channel.BlogId = BlogId.Value;
            channel.CreationDate = CreationDate;
            channel.Description = Description;
            channel.IsVisibleToNonSubscribers = IsVisibleToNonSubscribers;
            channel.Name = Name;
            channel.PriceInUsCentsPerWeek = PriceInUsCentsPerWeek;
            channel.PriceLastSetDate = PriceLastSetDate;
        }
    }
}