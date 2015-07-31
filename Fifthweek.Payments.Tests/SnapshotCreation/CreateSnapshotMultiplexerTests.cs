namespace Fifthweek.Payments.Tests.SnapshotCreation
{
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Payments.SnapshotCreation;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateSnapshotMultiplexerTests
    {
        private static readonly UserId UserId = UserId.Random();

        private Mock<ICreateSubscriberSnapshotDbStatement> createSubscriberSnapshot;
        private Mock<ICreateSubscriberChannelsSnapshotDbStatement> createSubscriberChannelsSnapshot;
        private Mock<ICreateCreatorChannelsSnapshotDbStatement> createCreatorChannelsSnapshot;
        private Mock<ICreateCreatorFreeAccessUsersSnapshotDbStatement> createCreatorFreeAccessUsersSnapshot;
        private CreateSnapshotMultiplexer target;

        [TestInitialize]
        public void Initialize()
        {
            this.createSubscriberSnapshot = new Mock<ICreateSubscriberSnapshotDbStatement>(MockBehavior.Strict);
            this.createSubscriberChannelsSnapshot = new Mock<ICreateSubscriberChannelsSnapshotDbStatement>(MockBehavior.Strict);
            this.createCreatorChannelsSnapshot = new Mock<ICreateCreatorChannelsSnapshotDbStatement>(MockBehavior.Strict);
            this.createCreatorFreeAccessUsersSnapshot = new Mock<ICreateCreatorFreeAccessUsersSnapshotDbStatement>(MockBehavior.Strict);
            this.target = new CreateSnapshotMultiplexer(
                this.createSubscriberSnapshot.Object,
                this.createSubscriberChannelsSnapshot.Object,
                this.createCreatorChannelsSnapshot.Object,
                this.createCreatorFreeAccessUsersSnapshot.Object);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriber_ItShouldCreateSubscriberSnapshot()
        {
            this.createSubscriberSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(UserId, SnapshotType.Subscriber);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsSubscriberChannels_ItShouldCreateSubscriberChannelsSnapshot()
        {
            this.createSubscriberChannelsSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(UserId, SnapshotType.SubscriberChannels);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorChannels_ItShouldCreateCreatorChannelsSnapshot()
        {
            this.createCreatorChannelsSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(UserId, SnapshotType.CreatorChannels);
        }

        [TestMethod]
        public async Task WhenSnapshotTypeIsCreatorFreeAccessUsers_ItShouldCreateCreatorFreeAccessUsersSnapshot()
        {
            this.createCreatorFreeAccessUsersSnapshot.Setup(v => v.ExecuteAsync(UserId)).Returns(Task.FromResult(0));

            await this.target.ExecuteAsync(UserId, SnapshotType.CreatorFreeAccessUsers);
        }
    }
}