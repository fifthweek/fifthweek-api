namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostNoteCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime TwoDaysAgo = DateTime.UtcNow.AddDays(-2);
        private static readonly PostNoteCommand ImmediatePostCommand = new PostNoteCommand(UserId, PostId, ChannelId, Note, null);
        private static readonly PostNoteCommand Command = ImmediatePostCommand; // Treat this as our canonical testing command.
        private Mock<IChannelSecurity> channelSecurity;
        private PostNoteCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelSecurity = new Mock<IChannelSecurity>();
        }

        [TestMethod]
        public async Task WhenNotAllowedToPost_ItShouldReportAnError()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, testDatabase.NewContext());
                this.channelSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();
                await testDatabase.TakeSnapshotAsync();

                Func<Task> badMethodCall = () => this.target.HandleAsync(Command);

                await badMethodCall.AssertExceptionAsync<UnauthorizedException>();

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInFuture_ItShouldSchedulePostForLater()
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, testDatabase.NewContext());
                var scheduledPostCommand = new PostNoteCommand(UserId, PostId, ChannelId, Note, TwoDaysFromNow);
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

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
            
                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            return expectedPost;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task WhenDateIsProvidedAndIsInPast_ItShouldSchedulePostForNow()
        {
            var misscheduledPostCommand = new PostNoteCommand(UserId, PostId, ChannelId, Note, TwoDaysAgo);
            await this.ItShouldSchedulePostForNow(misscheduledPostCommand);
        }

        [TestMethod]
        public async Task WhenDateIsNotProvided_ItShouldSchedulePostForNow()
        {
            await this.ItShouldSchedulePostForNow(ImmediatePostCommand);
        }

        private async Task ItShouldSchedulePostForNow(PostNoteCommand command)
        {
            await this.NewTestDatabaseAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, testDatabase.NewContext());
                await this.CreateChannelAsync(UserId, ChannelId, testDatabase);
                await testDatabase.TakeSnapshotAsync();

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

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Post>(expectedPost)
                    {
                        Expected = actualPost =>
                        {
                            expectedPost.CreationDate = actualPost.CreationDate; // Take wildcard properties from actual value.
                            expectedPost.LiveDate = actualPost.CreationDate; // Assumes creation date is UtcNow (haven't actually been testing this so far).
                            return expectedPost;
                        }
                    }
                };
            });
        }

        private async Task CreateChannelAsync(UserId newUserId, ChannelId newChannelId, TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreateTestChannelAsync(newUserId.Value, newChannelId.Value);
            }
        }
    }
}