namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateWeeklyReleaseScheduleDbStatementTests
    {
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { HourOfWeek.Parse(42) });
        private static readonly DateTime Now = DateTime.UtcNow;

        private Mock<IReplaceWeeklyReleaseTimesDbStatement> replaceWeeklyReleaseTimes;
        private Mock<IDefragmentQueueDbStatement> defragmentQueue;

        private UpdateWeeklyReleaseScheduleDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.replaceWeeklyReleaseTimes = new Mock<IReplaceWeeklyReleaseTimesDbStatement>();
            this.defragmentQueue = new Mock<IDefragmentQueueDbStatement>();
            this.target = new UpdateWeeklyReleaseScheduleDbStatement(this.replaceWeeklyReleaseTimes.Object, this.defragmentQueue.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, WeeklyReleaseSchedule, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireWeeklyReleaseSchedule()
        {
            await this.target.ExecuteAsync(QueueId, null, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(QueueId, WeeklyReleaseSchedule, DateTime.Now);
        }

        [TestMethod]
        public async Task ItShouldReplaceWeeklyReleaseTimes()
        {
            this.replaceWeeklyReleaseTimes.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.ExecuteAsync(QueueId, WeeklyReleaseSchedule, Now);

            this.replaceWeeklyReleaseTimes.Verify();
        }

        [TestMethod]
        public async Task ItShouldDefragmentQueueAfterReplacingWeeklyReleaseTimes()
        {
            var callOrder = 0;
            this.replaceWeeklyReleaseTimes.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(0, callOrder++));

            this.defragmentQueue.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule, Now))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(1, callOrder++))
                .Verifiable();

            await this.target.ExecuteAsync(QueueId, WeeklyReleaseSchedule, Now);

            this.defragmentQueue.Verify();
        }
    }
}