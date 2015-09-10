namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetLiveDateOfNewQueuedPostDbStatementTests
    {
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly IReadOnlyList<WeeklyReleaseTime> SortedReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(QueueId.Value, 10);
        private static readonly IReadOnlyList<HourOfWeek> SortedHoursOfWeek = SortedReleaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToList();
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(SortedHoursOfWeek);
        private static readonly DateTime LiveDateLowerBound = DateTime.UtcNow.AddDays(5);
        private static readonly DateTime CalculatedLiveDate = DateTime.UtcNow.AddDays(8);
        private Mock<IGetNewQueuedPostLiveDateLowerBoundDbStatement> getNewQueuedPostLiveDateLowerBound;
        private Mock<IGetWeeklyReleaseScheduleDbStatement> getCollectionWeeklyReleaseTimes;
        private Mock<IQueuedPostLiveDateCalculator> queuedPostReleaseTimeCalculator;
        private GetLiveDateOfNewQueuedPostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.getNewQueuedPostLiveDateLowerBound = new Mock<IGetNewQueuedPostLiveDateLowerBoundDbStatement>();
            this.getCollectionWeeklyReleaseTimes = new Mock<IGetWeeklyReleaseScheduleDbStatement>();
            this.queuedPostReleaseTimeCalculator = new Mock<IQueuedPostLiveDateCalculator>();
            this.target = new GetLiveDateOfNewQueuedPostDbStatement(
                this.getNewQueuedPostLiveDateLowerBound.Object, 
                this.getCollectionWeeklyReleaseTimes.Object,
                this.queuedPostReleaseTimeCalculator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldCalculateReleaseTimeOfHypotheticalNewQueuedPost()
        {
            this.getNewQueuedPostLiveDateLowerBound.Setup(_ => _.ExecuteAsync(QueueId, It.IsAny<DateTime>())).ReturnsAsync(LiveDateLowerBound);
            this.getCollectionWeeklyReleaseTimes.Setup(_ => _.ExecuteAsync(QueueId)).ReturnsAsync(WeeklyReleaseSchedule);
            this.queuedPostReleaseTimeCalculator
                .Setup(_ => _.GetNextLiveDate(LiveDateLowerBound, WeeklyReleaseSchedule))
                .Returns(CalculatedLiveDate);

            var result = await this.target.ExecuteAsync(QueueId);

            Assert.AreEqual(result, CalculatedLiveDate);
        }
    }
}