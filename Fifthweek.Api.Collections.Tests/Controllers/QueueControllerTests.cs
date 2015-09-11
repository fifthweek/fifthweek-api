namespace Fifthweek.Api.Collections.Tests.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http.Results;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Controllers;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class QueueControllerTests
    {
        private static readonly DateTime NewQueuedPostLiveDate = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly ValidQueueName QueueName = ValidQueueName.Parse("Bat puns");
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { HourOfWeek.Parse(0) });
        private Mock<ICommandHandler<CreateQueueCommand>> createCollection;
        private Mock<ICommandHandler<UpdateQueueCommand>> updateCollection;
        private Mock<ICommandHandler<DeleteQueueCommand>> deleteCollection;
        private Mock<IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime>> getLiveDateOfNewQueuedPost;
        private Mock<IRequesterContext> requesterContext;
        private Mock<IGuidCreator> guidCreator;
        private Mock<IRandom> random;
        private QueueController target;

        [TestInitialize]
        public void Initialize()
        {
            this.createCollection = new Mock<ICommandHandler<CreateQueueCommand>>();
            this.updateCollection = new Mock<ICommandHandler<UpdateQueueCommand>>();
            this.deleteCollection = new Mock<ICommandHandler<DeleteQueueCommand>>();
            this.getLiveDateOfNewQueuedPost = new Mock<IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.guidCreator = new Mock<IGuidCreator>();
            this.random = new Mock<IRandom>();

            this.target = new QueueController(
                this.createCollection.Object, 
                this.updateCollection.Object,
                this.deleteCollection.Object,
                this.getLiveDateOfNewQueuedPost.Object, 
                this.requesterContext.Object, 
                this.guidCreator.Object, 
                this.random.Object);
        }

        [TestMethod]
        public async Task WhenPostingCollection_ItShouldIssueCreateCollectionCommand()
        {
            const byte HourOfWeekValue = 42;
            var initialWeeklyReleaseTime = HourOfWeek.Parse(HourOfWeekValue);
            this.random.Setup(_ => _.Next(Shared.HourOfWeek.MinValue, Shared.HourOfWeek.MaxValue + 1)).Returns(HourOfWeekValue);
            var data = new NewQueueData(BlogId, QueueName.Value);
            var command = new CreateQueueCommand(Requester, QueueId, BlogId, QueueName, initialWeeklyReleaseTime);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.guidCreator.Setup(_ => _.CreateSqlSequential()).Returns(QueueId.Value);
            this.createCollection.Setup(_ => _.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PostQueueAsync(data);

            Assert.AreEqual(result, new QueueCreation(QueueId, initialWeeklyReleaseTime));
            this.createCollection.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPostingCollectionWithoutSpecifyingCollection_ItShouldThrowBadRequestException()
        {
            await this.target.PostQueueAsync(null);
        }

        [TestMethod]
        public async Task WhenPuttingCollection_ItShouldIssueUpdateCollectionCommand()
        {
            var data = new UpdatedQueueData(QueueName.Value, WeeklyReleaseSchedule.Value.Select(_ => _.Value).ToList());
            var command = new UpdateQueueCommand(Requester, QueueId, QueueName, WeeklyReleaseSchedule);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.updateCollection.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.PutQueueAsync(QueueId.Value.EncodeGuid(), data);

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateCollection.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingCollectionWithoutSpecifyingCollectionId_ItShouldThrowBadRequestException()
        {
            await this.target.PutQueueAsync(string.Empty, new UpdatedQueueData());
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenPuttingCollectionWithoutSpecifyingCollectionBody_ItShouldThrowBadRequestException()
        {
            await this.target.PutQueueAsync(QueueId.Value.EncodeGuid(), null);
        }

        [TestMethod]
        public async Task WhenDeletingCollection_ItShouldIssueDeleteCollectionCommand()
        {
            var command = new DeleteQueueCommand(Requester, QueueId);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);
            this.deleteCollection.Setup(v => v.HandleAsync(command)).Returns(Task.FromResult(0)).Verifiable();

            var result = await this.target.DeleteQueueAsync(QueueId.Value.EncodeGuid());

            Assert.IsInstanceOfType(result, typeof(OkResult));
            this.updateCollection.Verify();
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenDeletingCollectionWithoutSpecifyingCollectionId_ItShouldThrowBadRequestException()
        {
            await this.target.PutQueueAsync(string.Empty, new UpdatedQueueData());
        }

        [TestMethod]
        public async Task WhenGettingLiveDateOfNewQueuedPost_ItShouldReturnResultFromLiveDateOfNewQueuedPostQuery()
        {
            var requester = Requester.Authenticated(UserId);
            var query = new GetLiveDateOfNewQueuedPostQuery(requester, QueueId);

            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(requester);
            this.getLiveDateOfNewQueuedPost.Setup(_ => _.HandleAsync(query)).ReturnsAsync(NewQueuedPostLiveDate);

            var result = await this.target.GetLiveDateOfNewQueuedPost(QueueId.Value.EncodeGuid());

            Assert.AreEqual(result, NewQueuedPostLiveDate);
        }

        [TestMethod]
        [ExpectedException(typeof(BadRequestException))]
        public async Task WhenGettingLiveDateOfNewQueuedPostWithoutSpecifyingCollection_ItShouldThrowBadRequestException()
        {
            await this.target.GetLiveDateOfNewQueuedPost(null);
        }
    }
}