namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DefragmentQueueDbStatementTests
    {
        private const int QueueSize = 10;

        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { HourOfWeek.Parse(42) });
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime ExclusiveLowerBound = Now.AddDays(10);
        private static readonly IReadOnlyList<DateTime> UnfragmentedDates = new[] { Now.AddDays(1), Now.AddDays(2), Now.AddDays(3) };

        private Mock<IGetQueueSizeDbStatement> getQueueSize;
        private Mock<IGetQueueLowerBoundDbStatement> getQueueLowerBound;
        private Mock<IQueuedPostLiveDateCalculator> liveDateCalculator;
        private Mock<IUpdateAllLiveDatesInQueueDbStatement> updateAllLiveDatesInQueue;

        private DefragmentQueueDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.getQueueSize = new Mock<IGetQueueSizeDbStatement>();
            this.getQueueLowerBound = new Mock<IGetQueueLowerBoundDbStatement>();
            this.liveDateCalculator = new Mock<IQueuedPostLiveDateCalculator>();

            // Give side-effecting components strict mock behaviour.
            this.updateAllLiveDatesInQueue = new Mock<IUpdateAllLiveDatesInQueueDbStatement>(MockBehavior.Strict);

            this.target = new DefragmentQueueDbStatement(this.getQueueSize.Object, this.getQueueLowerBound.Object, this.liveDateCalculator.Object, this.updateAllLiveDatesInQueue.Object);
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
            await this.target.ExecuteAsync(CollectionId, null, Now);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task ItShouldRequireUtcDate()
        {
            await this.target.ExecuteAsync(CollectionId, WeeklyReleaseSchedule, DateTime.Now);
        }

        [TestMethod]
        public async Task WhenQueueIsEmpty_ItShouldHaveNoEffect()
        {
            this.getQueueSize.Setup(_ => _.ExecuteAsync(CollectionId, Now)).ReturnsAsync(0);
            
            await this.target.ExecuteAsync(CollectionId, WeeklyReleaseSchedule, Now);
        }

        [TestMethod]
        public async Task WhenQueueIsNotEmpty_ItShouldUpdateAllLiveDatesInQueueWithUnfragmentedDates()
        {
            this.getQueueSize.Setup(_ => _.ExecuteAsync(CollectionId, Now)).ReturnsAsync(QueueSize);
            this.getQueueLowerBound.Setup(_ => _.ExecuteAsync(CollectionId, Now)).ReturnsAsync(ExclusiveLowerBound);
            this.liveDateCalculator.Setup(_ => _.GetNextLiveDates(ExclusiveLowerBound, WeeklyReleaseSchedule, QueueSize)).Returns(UnfragmentedDates);
            this.updateAllLiveDatesInQueue.Setup(_ => _.ExecuteAsync(CollectionId, UnfragmentedDates, Now)).Returns(Task.FromResult(0)).Verifiable();

            await this.target.ExecuteAsync(CollectionId, WeeklyReleaseSchedule, Now);

            this.updateAllLiveDatesInQueue.Verify();
        }
    }
}