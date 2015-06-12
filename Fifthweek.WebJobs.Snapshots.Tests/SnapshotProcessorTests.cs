namespace Fifthweek.WebJobs.Snapshots.Tests
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
        private Mock<ICreateSubscriberSnapshotDbStatement> createSubscriberSnapshot;
        private Mock<ICreateSubscriberChannelsSnapshotDbStatement> createSubscriberChannelsSnapshot;
        private Mock<ICreateCreatorChannelsSnapshotDbStatement> createCreatorChannelsSnapshot;
        private Mock<ICreateCreatorFreeAccessUsersSnapshotDbStatement> createCreatorFreeAccessUsersSnapshot;
        private SnapshotProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.logger = new Mock<ILogger>(MockBehavior.Strict);
            this.createSubscriberSnapshot = new Mock<ICreateSubscriberSnapshotDbStatement>(MockBehavior.Strict);
            this.createSubscriberChannelsSnapshot = new Mock<ICreateSubscriberChannelsSnapshotDbStatement>(MockBehavior.Strict);
            this.createCreatorChannelsSnapshot = new Mock<ICreateCreatorChannelsSnapshotDbStatement>(MockBehavior.Strict);
            this.createCreatorFreeAccessUsersSnapshot = new Mock<ICreateCreatorFreeAccessUsersSnapshotDbStatement>(MockBehavior.Strict);
            this.target = new SnapshotProcessor(
                this.createSubscriberSnapshot.Object,
                this.createSubscriberChannelsSnapshot.Object,
                this.createCreatorChannelsSnapshot.Object,
                this.createCreatorFreeAccessUsersSnapshot.Object);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriber_ItShouldCreateSubscriberSnapshot()
        {
            this.createSubscriberSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.Subscriber),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriberChannels_ItShouldCreateSubscriberChannelsSnapshot()
        {
            this.createSubscriberChannelsSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.SubscriberChannels),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorChannels_ItShouldCreateCreatorChannelsSnapshot()
        {
            this.createCreatorChannelsSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.CreatorChannels),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorFreeAccessUsers_ItShouldCreateCreatorFreeAccessUsersSnapshot()
        {
            this.createCreatorFreeAccessUsersSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.CreateSnapshotAsync(
                new CreateSnapshotMessage(UserId, SnapshotType.CreatorFreeAccessUsers),
                this.logger.Object,
                CancellationToken);
        }

        [TestMethod]
        public async Task WhenErrorOccurs_ItShouldLogAndThrow()
        {
            var exception = new DivideByZeroException("message");
            this.createCreatorFreeAccessUsersSnapshot.Setup(v => v.ExecuteAsync(UserId)).Throws(exception);

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
