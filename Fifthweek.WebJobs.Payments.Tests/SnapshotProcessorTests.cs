namespace Fifthweek.WebJobs.Payments.Tests
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.Tests.Shared;
    using Fifthweek.WebJobs.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class SnapshotProcessorTests
    {
        private static readonly UserId UserId = UserId.Random();

        private static readonly CancellationToken CancellationToken = CancellationToken.None;

        private Mock<ILogger> logger;
        private Mock<ICreateSnapshotMultiplexer> createSnapshotMultiplexer;
        private Mock<ICreateAllSnapshotsProcessor> createAllSnapshots;
        private SnapshotProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.logger = new Mock<ILogger>(MockBehavior.Strict);
            this.createSnapshotMultiplexer = new Mock<ICreateSnapshotMultiplexer>(MockBehavior.Strict);
            this.createAllSnapshots = new Mock<ICreateAllSnapshotsProcessor>(MockBehavior.Strict);
            
            this.target = new SnapshotProcessor(
                this.createSnapshotMultiplexer.Object,
                this.createAllSnapshots.Object);
        }

        [TestMethod]
        public async Task WhenUserIdIsNull_ItShouldCreateAllSnapshots()
        {
            this.createAllSnapshots.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(null, default(SnapshotType)),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriber_ItShouldCreateSubscriberSnapshot()
        {
            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(UserId, SnapshotType.Subscriber)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.Subscriber),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriberChannels_ItShouldCreateSubscriberChannelsSnapshot()
        {
            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(UserId, SnapshotType.SubscriberChannels)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.SubscriberChannels),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorChannels_ItShouldCreateCreatorChannelsSnapshot()
        {
            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(UserId, SnapshotType.CreatorChannels)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.CreatorChannels),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorFreeAccessUsers_ItShouldCreateCreatorFreeAccessUsersSnapshot()
        {
            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(UserId, SnapshotType.CreatorFreeAccessUsers)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.CreatorFreeAccessUsers),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenErrorOccurs_ItShouldLogAndThrow()
        {
            var exception = new DivideByZeroException("message");
            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(UserId, SnapshotType.CreatorFreeAccessUsers)).Throws(exception);

            this.logger.Setup(v => v.Error(exception));

            await ExpectedException.AssertExceptionAsync<DivideByZeroException>(
                () => this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.CreatorFreeAccessUsers),
                this.logger.Object,
                CancellationToken));
        }

        [TestMethod]
        public async Task WhenPoisonMessageOccurs_ItShouldLog()
        {
            this.logger.Setup(v => v.Warn(It.IsAny<string>(), "{ snapshotType: 'blah' }"));

            await this.target.HandlePoisonMessageAsync(
                "{ snapshotType: 'blah' }",
                this.logger.Object,
                CancellationToken);
        }
    }
}
