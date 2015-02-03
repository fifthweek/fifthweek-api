namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ReviseNoteCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly ReviseNoteCommand Command = new ReviseNoteCommand(Requester, PostId, ChannelId, Note);
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IFifthweekDbContext> databaseContext;
        private ReviseNoteCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelSecurity = new Mock<IChannelSecurity>();
            this.postSecurity = new Mock<IPostSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give potentially side-effective components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new ReviseNoteCommandHandler(
                this.requesterSecurity.Object,
                this.channelSecurity.Object,
                this.postSecurity.Object,
                databaseContext);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new ReviseNoteCommand(Requester.Unauthenticated, PostId, ChannelId, Note));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToChannel_ItShouldThrowUnauthorizedException()
        {
            this.channelSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
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
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: true);
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
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: false);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenPostExists_ItShouldUpdate()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase, createPost: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedPost = new Post(PostId.Value)
                {
                    ChannelId = ChannelId.Value,
                    Comment = Note.Value
                };

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.LiveDate = actualPost.LiveDate;
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return expectedPost;
                        }
                    }
                };
            });
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId, TestDatabaseContext testDatabase, bool createPost)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestChannelAsync(newUserId.Value, newChannelId.Value);

                if (createPost)
                {
                    var post = PostTests.UniqueNote(new Random());
                    post.Id = PostId.Value;
                    post.ChannelId = newChannelId.Value;
                    await databaseContext.Database.Connection.InsertAsync(post);    
                }
            }
        }
    }
}