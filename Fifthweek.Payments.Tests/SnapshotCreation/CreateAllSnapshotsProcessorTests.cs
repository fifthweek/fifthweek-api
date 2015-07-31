namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.SnapshotCreation;
    using Fifthweek.WebJobs.Payments;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateAllSnapshotsProcessorTests
    {
        private Mock<IGetAllStandardUsersDbStatement> getAllStandardUsers;
        private Mock<ICreateSnapshotMultiplexer> createSnapshotMultiplexer;

        private CreateAllSnapshotsProcessor target;

        [TestInitialize]
        public void Initialize()
        {
            this.getAllStandardUsers = new Mock<IGetAllStandardUsersDbStatement>(MockBehavior.Strict);
            this.createSnapshotMultiplexer = new Mock<ICreateSnapshotMultiplexer>(MockBehavior.Strict);

            this.target = new CreateAllSnapshotsProcessor(
                this.getAllStandardUsers.Object,
                this.createSnapshotMultiplexer.Object);
        }

        [TestMethod]
        public async Task ItShouldCreateAllSnapshots()
        {
            var userIds = new List<UserId>();
            for (int i = 0; i < 10; i++)
            {
                userIds.Add(UserId.Random());
            }

            this.getAllStandardUsers.Setup(v => v.ExecuteAsync()).ReturnsAsync(userIds);

            this.createSnapshotMultiplexer.Setup(v => v.ExecuteAsync(It.IsAny<UserId>(), It.IsAny<SnapshotType>()))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync();

            foreach (var userId in userIds)
            {
                this.createSnapshotMultiplexer.Verify(v => v.ExecuteAsync(userId, SnapshotType.CreatorChannels), Times.Once);
                this.createSnapshotMultiplexer.Verify(v => v.ExecuteAsync(userId, SnapshotType.CreatorFreeAccessUsers), Times.Once);
                this.createSnapshotMultiplexer.Verify(v => v.ExecuteAsync(userId, SnapshotType.Subscriber), Times.Once);
                this.createSnapshotMultiplexer.Verify(v => v.ExecuteAsync(userId, SnapshotType.SubscriberChannels), Times.Once);
            }
        }
    }
}