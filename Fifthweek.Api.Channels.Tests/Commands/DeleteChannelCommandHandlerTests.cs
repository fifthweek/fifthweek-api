namespace Fifthweek.Api.Channels.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Payments.Services;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteChannelCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly DeleteChannelCommand Command = new DeleteChannelCommand(Requester, ChannelId);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IChannelSecurity> collectionSecurity;
        private Mock<IDeleteChannelDbStatement> deleteChannel;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private DeleteChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.collectionSecurity = new Mock<IChannelSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.deleteChannel = new Mock<IDeleteChannelDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeleteChannelCommandHandler(
                this.requesterSecurity.Object, 
                this.collectionSecurity.Object, 
                this.deleteChannel.Object, 
                this.scheduleGarbageCollection.Object);
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
            await this.target.HandleAsync(new DeleteChannelCommand(Requester.Unauthenticated, ChannelId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToChannel()
        {
            this.collectionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldDeleteChannel()
        {
            this.deleteChannel.Setup(_ => _.ExecuteAsync(UserId, ChannelId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.deleteChannel.Verify();
        }

        [TestMethod]
        public async Task ItShouldIssueGarbageChannel()
        {
            this.deleteChannel.Setup(_ => _.ExecuteAsync(UserId, ChannelId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.scheduleGarbageCollection.Verify();
        }
    } 
}