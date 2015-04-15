namespace Fifthweek.Api.Blogs.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class AcceptChannelSubscriptionPriceChangeCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly AcceptChannelSubscriptionPriceChangeCommand Command =
            new AcceptChannelSubscriptionPriceChangeCommand(
                Requester.Authenticated(UserId),
                new ChannelId(Guid.NewGuid()),
                ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(10));

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IAcceptChannelSubscriptionPriceChangeDbStatement> acceptPriceChange;

        private AcceptChannelSubscriptionPriceChangeCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Command.Requester);
            this.acceptPriceChange = new Mock<IAcceptChannelSubscriptionPriceChangeDbStatement>(MockBehavior.Strict);

            this.target = new AcceptChannelSubscriptionPriceChangeCommandHandler(
                this.requesterSecurity.Object,
                this.acceptPriceChange.Object);
        }

        private void SetupDbStatement()
        {
            this.acceptPriceChange
                .Setup(
                    v => v.ExecuteAsync(
                        UserId,
                        Command.ChannelId,
                        Command.AcceptedPrice,
                        It.Is<DateTime>(dt => dt.Kind == DateTimeKind.Utc)))
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
            await this.target.HandleAsync(new AcceptChannelSubscriptionPriceChangeCommand(
                Requester.Unauthenticated,
                Command.ChannelId,
                Command.AcceptedPrice));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldUpdateBlogSubscriptions()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.acceptPriceChange.Verify();
        }
    }
}