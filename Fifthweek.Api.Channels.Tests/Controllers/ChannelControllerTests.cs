namespace Fifthweek.Api.Channels.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Controllers;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Subscriptions.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ChannelControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());
        private static readonly ValidChannelName ChannelName = ValidChannelName.Parse("Premium");
        private static readonly ValidChannelPriceInUsCentsPerWeek Price = ValidChannelPriceInUsCentsPerWeek.Parse(10);

        private Mock<ICommandHandler<CreateChannelCommand>> createChannel;
        private Mock<ICommandHandler<UpdateChannelCommand>> updateChannel;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private ChannelController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createChannel = new Mock<ICommandHandler<CreateChannelCommand>>();
            this.updateChannel = new Mock<ICommandHandler<UpdateChannelCommand>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new ChannelController(this.createChannel.Object, this.updateChannel.Object, this.requesterContext.Object, this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingChannel_ItShouldIssueCreateChannelCommand()
        {
            var data = new NewChannelData(SubscriptionId, ChannelName.Value, Price.Value);
            var command = new CreateChannelCommand(Requester, ChannelId, SubscriptionId, ChannelName, Price);

            this.requesterContext.Setup(_ => _.GetRequester()).Returns(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(ChannelId.Value);
            this.createChannel.Setup(_ => _.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostChannelAsync(data);

            Assert.AreEqual(result, ChannelId);
            this.createChannel.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingChannelWithoutSpecifyingChannel_ItShouldThrowBadRequestException()
        {
            await this.target.PostChannelAsync(null);
        }


        [TestMethod]
        public async Task WhenPuttingChannel_ItShouldIssueUpdateChannelCommand()
        {
            var data = new UpdatedChannelData(ChannelName.Value, Price.Value, true);
            var command = new UpdateChannelCommand(Requester, ChannelId, ChannelName, Price, true);

            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            this.updateChannel.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutChannelAsync(ChannelId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateChannel.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingChannelWithoutSpecifyingChannelId_ItShouldThrowBadRequestException()
        {
            await this.target.PutChannelAsync(string.Empty, new UpdatedChannelData());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingChannelWithoutSpecifyingChannelBody_ItShouldThrowBadRequestException()
        {
            await this.target.PutChannelAsync(ChannelId.Value.EncodeGuid(), null);
        }
    }
}