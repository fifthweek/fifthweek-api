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
    public class UpdateBlogSubscriptionsCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly UpdateBlogSubscriptionsCommand Command =
            new UpdateBlogSubscriptionsCommand(
                Requester.Authenticated(UserId),
                new BlogId(Guid.NewGuid()),
                new List<AcceptedChannelSubscription>
                {
                    new AcceptedChannelSubscription(new ChannelId(Guid.NewGuid()), ValidAcceptedChannelPriceInUsCentsPerWeek.Parse(10)),
                });

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IUpdateBlogSubscriptionsDbStatement> updateBlogSubscriptions;

        private UpdateBlogSubscriptionsCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Command.Requester);
            this.updateBlogSubscriptions = new Mock<IUpdateBlogSubscriptionsDbStatement>(MockBehavior.Strict);

            this.target = new UpdateBlogSubscriptionsCommandHandler(
                this.requesterSecurity.Object,
                this.updateBlogSubscriptions.Object);
        }

        private void SetupDbStatement()
        {
            this.updateBlogSubscriptions
                .Setup(
                    v => v.ExecuteAsync(
                        UserId, 
                        Command.BlogId, 
                        Command.Channels,
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
            await this.target.HandleAsync(new UpdateBlogSubscriptionsCommand(
                Requester.Unauthenticated,
                Command.BlogId,
                Command.Channels));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldUpdateBlogSubscriptions()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Command);
            this.updateBlogSubscriptions.Verify();
        }
    }
}