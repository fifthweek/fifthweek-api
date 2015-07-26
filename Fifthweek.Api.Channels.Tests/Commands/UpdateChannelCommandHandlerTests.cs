namespace Fifthweek.Api.Channels.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateChannelCommandHandlerTests
    {
        private const bool IsVisibleToNonSubscribers = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidChannelName Name = ValidChannelName.Parse("Bat puns");
        private static readonly ValidChannelDescription Description = ValidChannelDescription.Parse("Bat puns\nBadPuns");
        private static readonly ValidChannelPrice Price = ValidChannelPrice.Parse(10);
        private static readonly UpdateChannelCommand Command = new UpdateChannelCommand(Requester, ChannelId, Name, Description, Price, IsVisibleToNonSubscribers);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IUpdateChannelDbStatement> updateChannelDbStatement;
        private UpdateChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.channelSecurity = new Mock<IChannelSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.updateChannelDbStatement = new Mock<IUpdateChannelDbStatement>(MockBehavior.Strict);

            this.target = new UpdateChannelCommandHandler(this.requesterSecurity.Object, this.channelSecurity.Object, this.updateChannelDbStatement.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCommand()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserIsAuthenticated()
        {
            await this.target.HandleAsync(new UpdateChannelCommand(Requester.Unauthenticated, ChannelId, Name, Description, Price, IsVisibleToNonSubscribers));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToChannel()
        {
            this.channelSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldCallTheUpdateChannelDbStatement()
        {
            this.updateChannelDbStatement.Setup(v => v.ExecuteAsync(
                UserId,
                ChannelId,
                Name,
                Description,
                Price,
                IsVisibleToNonSubscribers,
                It.Is<DateTime>(dt => dt.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);
        }
    } 
}