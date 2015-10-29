namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using Comment = Fifthweek.Api.Posts.Shared.Comment;

    [TestClass]
    public class RevisePostDbStatementTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ValidPreviewText PreviewText = ValidPreviewText.Parse("preview-text");
        private static readonly ValidComment Content = ValidComment.Parse("comment");
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly FileId ImageId = new FileId(Guid.NewGuid());
        private static readonly IReadOnlyList<FileId> FileIds = new List<FileId> { FileId, ImageId };
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private RevisePostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new RevisePostDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenPostIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, Content, PreviewText, ImageId, FileIds, 1, 2, 3, 4);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenFileIdsIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(PostId, Content, PreviewText, ImageId, null, 1, 2, 3, 4);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.InitializeTarget(testDatabase);
                    await this.CreateCollectionAsync(testDatabase, createPost: true);
                    await this.target.ExecuteAsync(PostId, Content, PreviewText, ImageId, FileIds, 1, 2, 3, 4);
                    await testDatabase.TakeSnapshotAsync();

                    await this.target.ExecuteAsync(PostId, Content, PreviewText, ImageId, FileIds, 1, 2, 3, 4);

                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.InitializeTarget(testDatabase);
                    await this.CreateCollectionAsync(testDatabase, createPost: false);
                    await testDatabase.TakeSnapshotAsync();

                    await this.target.ExecuteAsync(PostId, Content, PreviewText, ImageId, FileIds, 1, 2, 3, 4);

                    return ExpectedSideEffects.None;
                });
        }

        [TestMethod]
        public async Task WhenPostExists_ItShouldUpdatePost()
        {
            await this.DatabaseTestAsync(async testDatabase =>
                {
                    this.InitializeTarget(testDatabase);
                    var post = await this.CreateCollectionAsync(testDatabase, createPost: true);
                    await testDatabase.TakeSnapshotAsync();

                    await this.target.ExecuteAsync(PostId, Content, PreviewText, ImageId, FileIds, 1, 2, 3, 4);

                    var existingFileId = post.PreviewImageId;
                    var expectedPost = post.Copy(v => 
                    {
                        v.Content = Content.Value;
                        v.PreviewText = PreviewText.Value;
                        v.PreviewImageId = ImageId.Value;
                        v.PreviewWordCount = 1;
                        v.WordCount = 2;
                        v.ImageCount = 3;
                        v.FileCount = 4;
                    });

                    return new ExpectedSideEffects
                    {
                        Update = expectedPost,
                        Delete = new PostFile(PostId.Value, existingFileId.Value),
                        Inserts = new List<PostFile> { new PostFile(PostId.Value, ImageId.Value), new PostFile(PostId.Value, FileId.Value) },
                    };
                });
        }

        private async Task<Post> CreateCollectionAsync(TestDatabaseContext testDatabase, bool createPost)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestEntitiesAsync(UserId.Value, ChannelId.Value, QueueId.Value);

                if (createPost)
                {
                    var existingFileId = Guid.NewGuid();
                    await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, existingFileId);
                    await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);
                    await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, ImageId.Value);

                    var post = PostTests.UniqueFileOrImage(new Random());
                    post.Id = PostId.Value;
                    post.ChannelId = ChannelId.Value;
                    post.QueueId = QueueId.Value;
                    post.PreviewImageId = existingFileId;
                    await databaseContext.Database.Connection.InsertAsync(post);

                    var postFiles = new List<PostFile>();
                    postFiles.Add(new PostFile(post.Id, existingFileId));

                    await databaseContext.Database.Connection.InsertAsync(postFiles);

                    return post;
                }

                return null;
            }
        }
    }
}