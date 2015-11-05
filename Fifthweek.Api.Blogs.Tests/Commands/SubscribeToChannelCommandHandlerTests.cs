namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SubscribeToChannelCommandHandlerTests
    {
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly SubscribeToChannelCommand Command =
            new SubscribeToChannelCommand(
                Requester.Authenticated(UserId),
                new ChannelId(Guid.NewGuid()),
                ValidAcceptedChannelPrice.Parse(10),
                Now);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IUpdateChannelSubscriptionDbStatement> updateChannelSubscription;
        private Mock<IGetIsTestUserChannelDbStatement> isTestUserChannel;

        private SubscribeToChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Command.Requester);
            this.updateChannelSubscription = new Mock<IUpdateChannelSubscriptionDbStatement>(MockBehavior.Strict);
            this.isTestUserChannel = new Mock<IGetIsTestUserChannelDbStatement>(MockBehavior.Strict);

            this.target = new SubscribeToChannelCommandHandler(
                this.requesterSecurity.Object,
                this.updateChannelSubscription.Object,
                this.isTestUserChannel.Object);
        }

        private void SetupDbStatement()
        {
            this.updateChannelSubscription
                .Setup(
                    v => v.ExecuteAsync(
                        UserId,
                        Command.ChannelId,
                        Command.AcceptedPrice,
                        Now))
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
            await this.target.HandleAsync(new SubscribeToChannelCommand(
                Requester.Unauthenticated,
                Command.ChannelId,
                Command.AcceptedPrice,
                Now));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldUpdateBlogSubscriptions()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.updateChannelSubscription.Verify();
        }

        [TestMethod]
        public async Task WhenTestUser_AndBlogBelongsToTestUser_ItShouldUpdateBlogSubscriptions()
        {
            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Command.Requester, FifthweekRole.TestUser))
                .ReturnsAsync(true);

            this.isTestUserChannel.Setup(v => v.ExecuteAsync(Command.ChannelId)).ReturnsAsync(true).Verifiable();

            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.updateChannelSubscription.Verify();
            this.isTestUserChannel.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenTestUser_AndBlogBelongsToStandardUser_ItShouldThrowAnException()
        {
            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Command.Requester, FifthweekRole.TestUser))
                .ReturnsAsync(true);

            this.isTestUserChannel.Setup(v => v.ExecuteAsync(Command.ChannelId)).ReturnsAsync(false).Verifiable();

            await this.target.HandleAsync(Command);
        }
    }
}