namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.SqlTypes;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.AssemblyResolution;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class GetPostDbStatementTests : PersistenceTestsBase
    {
        private static readonly Random Random = new Random();
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly DateTime Timestamp = DateTime.UtcNow;
        private static readonly UserId CreatorId = new UserId(Guid.NewGuid());
        private static readonly string CreatorUsername = Guid.NewGuid().ToString();
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = ChannelId.Random();
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly PreviewText PreviewText = new PreviewText("Preview hey guys!");
        private static readonly Comment Content = new Comment("Hey guys!");
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;
        private static readonly int PreviewWordCount = 11;
        private static readonly int WordCount = 22;
        private static readonly int ImageCount = 33;
        private static readonly int FileCount = 44;

        private static readonly FileId FileId1 = FileId.Random();
        private static readonly string FileName1 = "FileName1";
        private static readonly string FileExtension1 = "FileExtension1";
        private static readonly string FilePurpose1 = FilePurposes.PostFile;
        private static readonly long FileSize1 = 1024;
        private static readonly int FileWidth1 = 800;
        private static readonly int FileHeight1 = 600;

        private static readonly FileId FileId2 = FileId.Random();
        private static readonly string FileName2 = "FileName2";
        private static readonly string FileExtension2 = "FileExtension2";
        private static readonly string FilePurpose2 = FilePurposes.PostImage;
        private static readonly long FileSize2 = 10242;
        private static readonly int FileWidth2 = 8002;
        private static readonly int FileHeight2 = 6002;

        private static readonly string ContentType1 = "ContentType1";
        private static readonly string ContentType2 = "ContentType2";

        private static readonly string ContainerName1 = "ContainerName1";
        private static readonly string ContainerName2 = "ContainerName2";

        private static readonly DateTime CreationDate = Now.AddDays(-12);
        private static readonly DateTime LiveDate = Now.AddDays(-10);
        private static readonly DateTime PublicExpiry = Now.AddDays(2);
        private static readonly DateTime PrivateExpiry = Now.AddDays(1);

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private GetPostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            DapperTypeHandlerRegistration.Register(FifthweekAssembliesResolver.Assemblies);

            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new GetPostDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(UserId, null);
        }

        [TestMethod]
        public async Task WhenUserHasNotLikedPost_ItShouldReturnPostAndFiles()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, likePost: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, PostId);

                AssertExpectedResult(false, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserHasLikedPost_ItShouldReturnPostAndFiles()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, likePost: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, PostId);

                AssertExpectedResult(true, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenUserIsLoggedOut_ItShouldReturnPostAndFiles()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, likePost: false);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(null, PostId);

                AssertExpectedResult(false, result);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostIsNotFound_ItShouldReturnNull()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase, likePost: true);
                await testDatabase.TakeSnapshotAsync();

                var result = await this.target.ExecuteAsync(UserId, PostId.Random());

                Assert.IsNull(result);

                return ExpectedSideEffects.None;
            });
        }

        private static void AssertExpectedResult(bool hasLiked, GetPostDbResult result)
        {
            Assert.AreEqual(
                new PreviewNewsfeedPost(
                    CreatorId,
                    CreatorUsername,
                    null,
                    PostId,
                    BlogId,
                    BlogId.ToString(),
                    ChannelId,
                    ChannelId.ToString(),
                    PreviewText,
                    Content,
                    FileId1,
                    PreviewWordCount,
                    WordCount,
                    ImageCount,
                    FileCount,
                    LiveDate,
                    FileName1,
                    FileExtension1,
                    FileSize1,
                    FileWidth1,
                    FileHeight1,
                    1 + (hasLiked ? 1 : 0),
                    1,
                    hasLiked,
                    CreationDate),
                result.Post);

            Assert.AreEqual(2, result.Files.Count);
            var file1 = result.Files.Single(v => v.FileId.Equals(FileId1));
            var file2 = result.Files.Single(v => v.FileId.Equals(FileId2));

            Assert.AreEqual(
                new GetPostDbResult.PostFileDbResult(
                    FileId1,
                    FileName1,
                    FileExtension1,
                    FilePurpose1,
                    FileSize1,
                    FileWidth1,
                    FileHeight1),
                file1);

            Assert.AreEqual(
                new GetPostDbResult.PostFileDbResult(
                    FileId2,
                    FileName2,
                    FileExtension2,
                    FilePurpose2,
                    FileSize2,
                    FileWidth2,
                    FileHeight2),
                file2);
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase, bool likePost)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestBlogAsync(CreatorId.Value, BlogId.Value, null, Random, CreatorUsername, BlogId.ToString());

                var channel = ChannelTests.UniqueEntity(Random);
                channel.BlogId = BlogId.Value;
                channel.Name = ChannelId.ToString();
                channel.Id = ChannelId.Value;
                await databaseContext.Database.Connection.InsertAsync(channel);

                var file1 = FileTests.UniqueEntity(Random);
                file1.BlobSizeBytes = FileSize1;
                file1.ChannelId = ChannelId.Value;
                file1.FileExtension = FileExtension1;
                file1.FileNameWithoutExtension = FileName1;
                file1.Id = FileId1.Value;
                file1.Purpose = FilePurpose1;
                file1.RenderHeight = FileHeight1;
                file1.RenderWidth = FileWidth1;
                file1.UserId = CreatorId.Value;
                await databaseContext.Database.Connection.InsertAsync(file1);

                var file2 = FileTests.UniqueEntity(Random);
                file2.BlobSizeBytes = FileSize2;
                file2.ChannelId = ChannelId.Value;
                file2.FileExtension = FileExtension2;
                file2.FileNameWithoutExtension = FileName2;
                file2.Id = FileId2.Value;
                file2.Purpose = FilePurpose2;
                file2.RenderHeight = FileHeight2;
                file2.RenderWidth = FileWidth2;
                file2.UserId = CreatorId.Value;
                await databaseContext.Database.Connection.InsertAsync(file2);

                var post = PostTests.UniqueFileOrImage(Random);
                post.ChannelId = ChannelId.Value;
                post.PreviewText = PreviewText.Value;
                post.Content = Content.Value;
                post.CreationDate = CreationDate;
                post.FileCount = FileCount;
                post.Id = PostId.Value;
                post.ImageCount = ImageCount;
                post.LiveDate = LiveDate;
                post.PreviewImageId = FileId1.Value;
                post.PreviewWordCount = PreviewWordCount;
                post.WordCount = WordCount;

                await databaseContext.Database.Connection.InsertAsync(post);
                await databaseContext.Database.Connection.InsertAsync(new PostFile(post.Id, file1.Id));
                await databaseContext.Database.Connection.InsertAsync(new PostFile(post.Id, file2.Id));

                await databaseContext.CreateTestUserAsync(UserId.Value, Random);
                var user2 = await databaseContext.CreateTestUserAsync(Guid.NewGuid(), Random);

                await databaseContext.Database.Connection.InsertAsync(new Like(post.Id, null, user2.Id, null, CreationDate));
                await databaseContext.Database.Connection.InsertAsync(new Persistence.Comment(Guid.NewGuid(), post.Id, null, user2.Id, null, "coment", CreationDate));

                if (likePost)
                {
                    await databaseContext.Database.Connection.InsertAsync(new Like(post.Id, null, UserId.Value, null, CreationDate));
                }
            }
        }
    }
}