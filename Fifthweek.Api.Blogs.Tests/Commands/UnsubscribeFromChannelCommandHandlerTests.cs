namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UnsubscribeFromChannelCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly UnsubscribeFromChannelCommand Command =
            new UnsubscribeFromChannelCommand(
                Requester.Authenticated(UserId),
                new ChannelId(Guid.NewGuid()));

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IUnsubscribeFromChannelDbStatement> unsubscribeFromChannel;

        private UnsubscribeFromChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Command.Requester);
            this.unsubscribeFromChannel = new Mock<IUnsubscribeFromChannelDbStatement>(MockBehavior.Strict);

            this.target = new UnsubscribeFromChannelCommandHandler(
                this.requesterSecurity.Object,
                this.unsubscribeFromChannel.Object);
        }

        private void SetupDbStatement()
        {
            this.unsubscribeFromChannel
                .Setup(
                    v => v.ExecuteAsync(
                        UserId,
                        Command.ChannelId))
                .Returns(Task.FromResult(0))
                .Verifiable();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsUnauthorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new UnsubscribeFromChannelCommand(
                Requester.Unauthenticated,
                Command.ChannelId));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldUpdateBlogSubscriptions()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.unsubscribeFromChannel.Verify();
        }
    }
}