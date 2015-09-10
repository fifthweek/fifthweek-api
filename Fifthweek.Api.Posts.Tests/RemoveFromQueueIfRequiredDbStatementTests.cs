namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class RemoveFromQueueIfRequiredDbStatementTests
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { HourOfWeek.Parse(42) });
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<ITryGetPostQueueIdStatement> tryGetPostQueuedCollection;
        private Mock<IGetWeeklyReleaseScheduleDbStatement> getWeeklyReleaseSchedule;
        private Mock<IDefragmentQueueDbStatement> defragmentQueue;
        private Mock<Func<Task>> potentialRemovalOperation;

        private DefragmentQueueIfRequiredDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.tryGetPostQueuedCollection = new Mock<ITryGetPostQueueIdStatement>();
            this.getWeeklyReleaseSchedule = new Mock<IGetWeeklyReleaseScheduleDbStatement>();

            this.potentialRemovalOperation = new Mock<Func<Task>>(MockBehavior.Strict);
            this.defragmentQueue = new Mock<IDefragmentQueueDbStatement>(MockBehavior.Strict);

            this.target = new DefragmentQueueIfRequiredDbStatement(this.tryGetPostQueuedCollection.Object, this.getWeeklyReleaseSchedule.Object, this.defragmentQueue.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequirePostId()
        {
            await this.target.ExecuteAsync(null, Now, this.potentialRemovalOperation.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(PostId, DateTime.Now, this.potentialRemovalOperation.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireOperation()
        {
            await this.target.ExecuteAsync(PostId, Now, null);
        }

        [TestMethod]
        public async Task WhenPostIsQueued_ItShouldDefragmentQueue()
        {
            this.tryGetPostQueuedCollection.Setup(_ => _.ExecuteAsync(PostId, Now)).ReturnsAsync(QueueId);
            this.getWeeklyReleaseSchedule.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(WeeklyReleaseSchedule);

            var callOrder = 0;
            this.potentialRemovalOperation.Setup(_ => _())
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(0, callOrder++))
                .Verifiable();

            this.defragmentQueue.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(1, callOrder++))
                .Verifiable();

            await this.target.ExecuteAsync(PostId, Now, this.potentialRemovalOperation.Object);

            this.potentialRemovalOperation.Verify();
            this.defragmentQueue.Verify();
        }

        [TestMethod]
        public async Task WhenPostIsNotQueued_ItShouldNotDefragmentQueue()
        {
            this.tryGetPostQueuedCollection.Setup(_ => _.ExecuteAsync(PostId, Now)).ReturnsAsync(null);

            this.potentialRemovalOperation.Setup(_ => _())
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync(PostId, Now, this.potentialRemovalOperation.Object);

            this.potentialRemovalOperation.Verify();
        }
    }
}