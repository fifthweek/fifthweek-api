namespace Fifthweek.Api.Channels.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Controllers;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ChannelControllerTests
    {
        private const bool IsVisibleToNonSubscribers = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ValidChannelName ChannelName = ValidChannelName.Parse("Premium");
        private static readonly ValidChannelDescription ChannelDescription = ValidChannelDescription.Parse("Premium");
        private static readonly ValidChannelPrice Price = ValidChannelPrice.Parse(10);

        private Mock<ICommandHandler<CreateChannelCommand>> createChannel;
        private Mock<ICommandHandler<UpdateChannelCommand>> updateChannel;
        private Mock<ICommandHandler<DeleteChannelCommand>> deleteChannel;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private ChannelController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createChannel = new Mock<ICommandHandler<CreateChannelCommand>>();
            this.updateChannel = new Mock<ICommandHandler<UpdateChannelCommand>>();
            this.deleteChannel = new Mock<ICommandHandler<DeleteChannelCommand>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.target = new ChannelController(this.createChannel.Object, this.updateChannel.Object, this.deleteChannel.Object, this.requesterContext.Object, this.guidCreator.Object);
        }

        [TestMethod]
        public async Task WhenPostingChannel_ItShouldIssueCreateChannelCommand()
        {
            var data = new NewChannelData(BlogId, ChannelName.Value, ChannelDescription.Value, Price.Value, IsVisibleToNonSubscribers);
            var command = new CreateChannelCommand(Requester, ChannelId, BlogId, ChannelName, ChannelDescription, Price, IsVisibleToNonSubscribers);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
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
            var data = new UpdatedChannelData(ChannelName.Value, ChannelDescription.Value, Price.Value, true);
            var command = new UpdateChannelCommand(Requester, ChannelId, ChannelName, ChannelDescription, Price, true);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
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

        [TestMethod]
        public async Task WhenDeletingChannel_ItShouldIssueDeleteChannelCommand()
        {
            var command = new DeleteChannelCommand(Requester, ChannelId);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.deleteChannel.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.DeleteChannelAsync(ChannelId.Value.EncodeGuid());

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateChannel.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenDeletingChannelWithoutSpecifyingChannelId_ItShouldThrowBadRequestException()
        {
            await this.target.PutChannelAsync(string.Empty, new UpdatedChannelData());
        }
    }
}