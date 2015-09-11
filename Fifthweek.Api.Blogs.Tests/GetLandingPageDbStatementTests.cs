namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Data.Entity;
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
        private static readonly FileId ProfileFileId = new FileId(Guid.NewGuid());
        private static readonly FileId HeaderFileId = new FileId(Guid.NewGuid());
        private static readonly ChannelId ChannelId1 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId2 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId3 = new ChannelId(Guid.NewGuid());
        private static readonly ChannelId ChannelId4 = new ChannelId(Guid.NewGuid());
        private static readonly DateTime CreationDate = new SqlDateTime(DateTime.UtcNow.AddDays(-10)).Value;
        private static readonly DateTime PriceLastSetDate = new SqlDateTime(DateTime.UtcNow.AddDays(-5)).Value;
        private static readonly string Description = "description";
        private static readonly bool IsVisibleToNonSubscribers = true;
        private static readonly string Name = "name";
        private static readonly int Price = 10;
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
        public async Task WhenUsernameDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(new Username(Guid.NewGuid().ToString()));

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenBlogDoesNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenChannelsDoNotExist_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new GetLandingPageDbStatement(testDatabase);

                await this.CreateDataAsync(testDatabase, true, false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(Username);

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
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

                Assert.AreEqual(CreatorId, result.UserId);
                Assert.AreEqual(ProfileFileId, result.ProfileImageFileId);

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

                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId1)));
                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId2)));
                Assert.IsNotNull(result.Channels.FirstOrDefault(v => v.ChannelId.Equals(ChannelId3)));

                foreach (var channel in result.Channels)
                {
                    Assert.AreEqual(IsVisibleToNonSubscribers, channel.IsVisibleToNonSubscribers);
                    Assert.AreEqual(Name, channel.Name);
                    Assert.AreEqual(Price, channel.Price);
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

        private async Task CreateDataAsync(TestDatabaseContext testDatabase, bool createBlog = true, bool createChannels = true)
        {
            await this.CreateUserAsync(testDatabase);
            await this.CreateHeaderFileAsync(testDatabase);
            
            if (createBlog)
            {
                await this.CreateBlogAsync(testDatabase);
                
                if (createChannels)
                {
                    await this.CreateChannelsAndCollectionsAsync(testDatabase);
                }
            }
        }

        private async Task CreateUserAsync(TestDatabaseContext testDatabase)
        {
            var random = new Random();
            var user = UserTests.UniqueEntity(random);
            user.Id = CreatorId.Value;
            user.UserName = Username.Value;

            var file = FileTests.UniqueEntity(random);
            file.Id = ProfileFileId.Value;
            file.UserId = CreatorId.Value;

            using (var databaseContext = testDatabase.CreateContext())
            {
                databaseContext.Users.Add(user);
                await databaseContext.SaveChangesAsync();
            }

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(file);
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                file = await databaseContext.Files.FirstAsync(v => v.Id == ProfileFileId.Value);
                user = await databaseContext.Users.FirstAsync(v => v.Id == CreatorId.Value);
                user.ProfileImageFile = file;
                user.ProfileImageFileId = file.Id;
                await databaseContext.SaveChangesAsync();
            }
        }

        private async Task CreateHeaderFileAsync(TestDatabaseContext testDatabase)
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

        private async Task CreateChannelsAndCollectionsAsync(TestDatabaseContext testDatabase)
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

            // Forth channel is not visible.
            var channel4 = ChannelTests.UniqueEntity(random);
            channel4.Id = ChannelId4.Value;
            ConfigureChannel(channel4);
            channel4.IsVisibleToNonSubscribers = false;

            using (var connection = testDatabase.CreateConnection())
            {
                await connection.InsertAsync(channel1);
                await connection.InsertAsync(channel2);
                await connection.InsertAsync(channel3);
                await connection.InsertAsync(channel4);
            }
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