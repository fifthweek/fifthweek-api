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
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly IReadOnlyList<WeeklyReleaseTime> SortedReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(CollectionId.Value, 10);
        private static readonly IReadOnlyList<HourOfWeek> SortedHoursOfWeek = SortedReleaseTimes.Select(_ => HourOfWeek.Parse(_.HourOfWeek)).ToList();
        private static readonly DateTime LiveDateLowerBound = DateTime.UtcNow.AddDays(5);
        private static readonly DateTime CalculatedLiveDate = DateTime.UtcNow.AddDays(8);
        private Mock<IGetNewQueuedPostLiveDateLowerBoundDbStatement> getNewQueuedPostLiveDateLowerBound;
        private Mock<IGetCollectionWeeklyReleaseTimesDbStatement> getCollectionWeeklyReleaseTimes;
        private Mock<IQueuedPostLiveDateCalculator> queuedPostReleaseTimeCalculator;
        private GetLiveDateOfNewQueuedPostDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            this.getNewQueuedPostLiveDateLowerBound = new Mock<IGetNewQueuedPostLiveDateLowerBoundDbStatement>();
            this.getCollectionWeeklyReleaseTimes = new Mock<IGetCollectionWeeklyReleaseTimesDbStatement>();
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
            IReadOnlyList<HourOfWeek> actualReleaseTimes = null;

            this.getNewQueuedPostLiveDateLowerBound.Setup(_ => _.ExecuteAsync(CollectionId, It.IsAny<DateTime>())).ReturnsAsync(LiveDateLowerBound);
            this.getCollectionWeeklyReleaseTimes.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(SortedReleaseTimes);
            this.queuedPostReleaseTimeCalculator
                .Setup(_ => _.GetNextLiveDate(LiveDateLowerBound, It.IsAny<IReadOnlyList<HourOfWeek>>()))
                .Returns(CalculatedLiveDate)
                .Callback((DateTime dateTime, IReadOnlyList<HourOfWeek> releaseTimes) =>
                {
                    actualReleaseTimes = releaseTimes;
                });

            var result = await this.target.ExecuteAsync(CollectionId);

            Assert.IsNotNull(actualReleaseTimes);
            CollectionAssert.AreEqual(actualReleaseTimes.ToList(), SortedHoursOfWeek.ToList());

            Assert.AreEqual(result, CalculatedLiveDate);
        }
    }
}