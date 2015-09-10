namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
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

    [TestClass]
    public class ReviseImageCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ValidComment Comment = ValidComment.Parse("Hey peeps!");
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly FileId FileId = new FileId(Guid.NewGuid());
        private static readonly ReviseImageCommand Command = new ReviseImageCommand(Requester, PostId, FileId, Comment);
        private Mock<IPostSecurity> postSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private ReviseImageCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.postSecurity = new Mock<IPostSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new ReviseImageCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                connectionFactory,
                this.scheduleGarbageCollection.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new ReviseImageCommand(Requester.Unauthenticated, PostId, FileId, Comment));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToPost_ItShouldThrowUnauthorizedException()
        {
            this.postSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, PostId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0));
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionAsync(testDatabase, createPost: true);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostDoesNotExist_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0));
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionAsync(testDatabase, createPost: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExists_ItShouldUpdateAndScheduleGarbageCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionAsync(testDatabase, createPost: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                this.scheduleGarbageCollection.Verify();

                var expectedPost = new Post(PostId.Value)
                {
                    ChannelId = ChannelId.Value,
                    QueueId = QueueId.Value,
                    ImageId = FileId.Value,
                    Comment = Comment.Value
                };

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.ScheduledByQueue = actualPost.ScheduledByQueue;
                            expectedPost.LiveDate = actualPost.LiveDate;
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return expectedPost;
                        }
                    }
                };
            });
        }

        private async Task CreateCollectionAsync(TestDatabaseContext testDatabase, bool createPost)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestCollectionAsync(UserId.Value, ChannelId.Value, QueueId.Value);

                if (createPost)
                {
                    var existingFileId = Guid.NewGuid();
                    await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, existingFileId);
                    await databaseContext.CreateTestFileWithExistingUserAsync(UserId.Value, FileId.Value);

                    var post = PostTests.UniqueFileOrImage(new Random());
                    post.Id = PostId.Value;
                    post.ChannelId = ChannelId.Value;
                    post.QueueId = QueueId.Value;
                    post.ImageId = existingFileId;
                    await databaseContext.Database.Connection.InsertAsync(post);    
                }
            }
        }
    }
}