﻿namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Subscriptions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    using ChannelId = Fifthweek.Api.Channels.Shared.ChannelId;

    [TestClass]
    public class PostNoteCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly DateTime TwoDaysFromNow = DateTime.UtcNow.AddDays(2);
        private static readonly DateTime TwoDaysAgo = DateTime.UtcNow.AddDays(-2);
        private static readonly PostNoteCommand ImmediatePostCommand = new PostNoteCommand(Requester, PostId, ChannelId, Note, null);
        private static readonly PostNoteCommand Command = ImmediatePostCommand; // Treat this as our canonical testing command.
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private PostNoteCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelSecurity = new Mock<IChannelSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give side-effecting components strict mock behaviour.
            var databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.target = new PostNoteCommandHandler(
                this.channelSecurity.Object,
                this.requesterSecurity.Object,
                databaseContext.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new PostNoteCommand(Requester.Unauthenticated, PostId, ChannelId, Note, null));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToPost_ItShouldThrowUnauthorizedException()
        {
            this.channelSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task WhenReRun_ItShouldHaveNoEffect()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, this.requesterSecurity.Object, testDatabase.NewContext());
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
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, this.requesterSecurity.Object, testDatabase.NewContext());
                var scheduledPostCommand = new PostNoteCommand(Requester, PostId, ChannelId, Note, TwoDaysFromNow);
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
                    false,
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
            var misscheduledPostCommand = new PostNoteCommand(Requester, PostId, ChannelId, Note, TwoDaysAgo);
            await this.ItShouldSchedulePostForNow(misscheduledPostCommand);
        }

        [TestMethod]
        public async Task WhenDateIsNotProvided_ItShouldSchedulePostForNow()
        {
            await this.ItShouldSchedulePostForNow(ImmediatePostCommand);
        }

        private async Task ItShouldSchedulePostForNow(PostNoteCommand command)
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.target = new PostNoteCommandHandler(this.channelSecurity.Object, this.requesterSecurity.Object, testDatabase.NewContext());
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
                    false,
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