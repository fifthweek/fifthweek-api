namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Azure;
    using Fifthweek.Payments.SnapshotCreation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RequestSnapshotServiceTests
    {
        private static readonly UserId UserId = UserId.Random();

        private Mock<IQueueService> queueService;
        private RequestSnapshotService target;

        [TestInitialize]
        public void Initialize()
        {
            this.queueService = new Mock<IQueueService>(MockBehavior.Strict);
            this.target = new RequestSnapshotService(this.queueService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenUserIdIsNull_ItShouldThrowAnException()
        {
            await this.target.ExecuteAsync(null, default(SnapshotType));
        }

        [TestMethod]
        public async Task WhenCalledWithCreatorChannels_ItShouldCallQueueService()
        {
            await this.PerformTest(SnapshotType.CreatorChannels);
        }

        [TestMethod]
        public async Task WhenCalledWithCreatorFreeAccessUsers_ItShouldCallQueueService()
        {
            await this.PerformTest(SnapshotType.CreatorFreeAccessUsers);
        }

        [TestMethod]
        public async Task WhenCalledWithSubscriber_ItShouldCallQueueService()
        {
            await this.PerformTest(SnapshotType.Subscriber);
        }

        [TestMethod]
        public async Task WhenCalledWithSubscriberChannels_ItShouldCallQueueService()
        {
            await this.PerformTest(SnapshotType.SubscriberChannels);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public async Task WhenQueueServiceFails_ItShouldThrowAnException()
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Fifthweek.Payments.Shared.Constants.RequestSnapshotQueueName,
                    new CreateSnapshotMessage(UserId, SnapshotType.CreatorChannels),
                    null,
                    RequestSnapshotService.SnapshotDelay)).Throws(new DivideByZeroException());

            await this.target.ExecuteAsync(UserId, SnapshotType.CreatorChannels);
        }

        private async Task PerformTest(SnapshotType snapshotType)
        {
            this.queueService.Setup(
                v =>
                v.AddMessageToQueueAsync(
                    Fifthweek.Payments.Shared.Constants.RequestSnapshotQueueName,
                    new CreateSnapshotMessage(UserId, snapshotType),
                    null,
                    RequestSnapshotService.SnapshotDelay)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(UserId, snapshotType);
        }
    }
}