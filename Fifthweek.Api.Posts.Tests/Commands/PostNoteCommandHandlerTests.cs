namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostNoteCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly ValidNote Note = ValidNote.Parse("Hey peeps!");
        private static readonly DateTime ScheduleDate = DateTime.UtcNow.AddDays(2);
        private static readonly PostNoteCommand Command = new PostNoteCommand(Requester, PostId, ChannelId, Note, ScheduleDate);
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostNoteDbStatement> upsertNote;
        private PostNoteCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.channelSecurity = new Mock<IChannelSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give side-effecting components strict mock behaviour.
            this.upsertNote = new Mock<IPostNoteDbStatement>(MockBehavior.Strict);

            this.target = new PostNoteCommandHandler(
                this.requesterSecurity.Object,
                this.channelSecurity.Object,
                this.upsertNote.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new PostNoteCommand(Requester.Unauthenticated, PostId, ChannelId, Note, null));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToChannel_ItShouldThrowUnauthorizedException()
        {
            this.channelSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldInsertNote()
        {
            this.upsertNote.Setup(_ => _.ExecuteAsync(PostId, ChannelId, Note, ScheduleDate, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.upsertNote.Verify();
        }
    }
}