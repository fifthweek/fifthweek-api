namespace Fifthweek.Api.Subscriptions.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Subscriptions.Commands;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateNoteCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime TwoDaysAgo = DateTime.UtcNow.AddDays(-2);
        private static readonly CreateNoteCommand ImmediatePostCommand = new CreateNoteCommand(UserId, ChannelId, PostId, Note, null);
        private static readonly CreateNoteCommand Command = ImmediatePostCommand; // Treat this as our canonical testing command.
        private Mock<IChannelSecurity> channelSecurity;
        private CreateNoteCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelSecurity = new Mock<IChannelSecurity>();
            this.target = new CreateNoteCommandHandler(this.channelSecurity.Object, this.NewDbContext());
        }

        [TestCleanup]
        public override void Cleanup()
        {
            base.Cleanup();
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            await this.InitializeDatabaseAsync();
            await this.SnapshotDatabaseAsync();

            this.channelSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            try
            {
                await this.target.HandleAsync(Command);
                Assert.Fail("Expected recoverable exception");
            }
            catch (UnauthorizedException)
            {
            }

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateChannelAsync(UserId, ChannelId);
            await this.target.HandleAsync(Command);
            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(Command);

            await this.AssertDatabaseAsync(ExpectedSideEffects.None);
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInFuture_ItShouldSchedulePostForLater()
        {
            await this.InitializeDatabaseAsync();
            await this.CreateChannelAsync(UserId, ChannelId);
            await this.SnapshotDatabaseAsync();

            var scheduledPostCommand = new CreateNoteCommand(UserId, ChannelId, PostId, Note, TwoDaysFromNow);
            await this.target.HandleAsync(scheduledPostCommand);

            var expectedPost = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                Note.Value,
                null,
                new SqlDateTime(TwoDaysFromNow).Value,
                default(DateTime));
            
            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<Post>(expectedPost)
                {
                    AreEqual = actualPost =>
                    {
                        expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                        return Equals(expectedPost, actualPost);
                    }
                }
            });
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInPast_ItShouldSchedulePostForNow()
        {
            var misscheduledPostCommand = new CreateNoteCommand(UserId, ChannelId, PostId, Note, TwoDaysAgo);
            await this.ItShouldSchedulePostForNow(misscheduledPostCommand);
        }

        [TestMethod]
        public async Task WhenDateIsNotProvided_ItShouldSchedulePostForNow()
        {
            await this.ItShouldSchedulePostForNow(ImmediatePostCommand);
        }

        private async Task ItShouldSchedulePostForNow(CreateNoteCommand command)
        {
            await this.InitializeDatabaseAsync();
            await this.CreateChannelAsync(UserId, ChannelId);
            await this.SnapshotDatabaseAsync();

            await this.target.HandleAsync(command);

            var expectedPost = new Post(
                PostId.Value,
                ChannelId.Value,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                Note.Value,
                null,
                default(DateTime),
                default(DateTime));

            await this.AssertDatabaseAsync(new ExpectedSideEffects
            {
                Insert = new WildcardEntity<Post>(expectedPost)
                {
                    AreEqual = actualPost =>
                    {
                        expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                        expectedPost.LiveDate = actualPost.CreationDate; // Assumes creation date is UtcNow (haven't actually been testing this so far).
                        return Equals(expectedPost, actualPost);
                    }
                }
            });
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId)
        {
            using (var databaseContext = this.NewDbContext())
            {
                await databaseContext.CreateTestChannelAsync(newUserId.Value, newChannelId.Value);
            }
        }
    }
}