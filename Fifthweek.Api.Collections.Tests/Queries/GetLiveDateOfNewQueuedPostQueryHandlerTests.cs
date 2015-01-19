namespace Fifthweek.Api.Collections.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetLiveDateOfNewQueuedPostQueryHandlerTests
    {
        private const int CurrentQueueSize = 8;

        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly IReadOnlyList<WeeklyReleaseTime> SortedReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(CollectionId.Value, 10); 
        private static readonly DateTime CalculatedLiveDate = DateTime.UtcNow.AddDays(5);
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<ICountQueuedPostsInCollectionDbStatement> countQueuedPostsInCollection;
        private Mock<IGetCollectionWeeklyReleaseTimesDbStatement> getCollectionWeeklyReleaseTimes;
        private Mock<IQueuedPostReleaseTimeCalculator> queuedPostReleaseTimeCalculator;
        private GetLiveDateOfNewQueuedPostQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.countQueuedPostsInCollection = new Mock<ICountQueuedPostsInCollectionDbStatement>();
            this.getCollectionWeeklyReleaseTimes = new Mock<IGetCollectionWeeklyReleaseTimesDbStatement>();
            this.queuedPostReleaseTimeCalculator = new Mock<IQueuedPostReleaseTimeCalculator>();
            this.target = new GetLiveDateOfNewQueuedPostQueryHandler(
                this.collectionSecurity.Object, 
                this.countQueuedPostsInCollection.Object, 
                this.getCollectionWeeklyReleaseTimes.Object,
                this.queuedPostReleaseTimeCalculator.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(Requester.Unauthenticated, CollectionId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToPost_ItShouldThrowUnauthorizedException()
        {
            this.collectionSecurity.Setup(_ => _.AssertPostingAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(Requester, CollectionId));
        }

        [TestMethod]
        public async Task ItShouldCalculateReleaseTimeOfHypotheticalNewQueuedPost()
        {
            this.countQueuedPostsInCollection.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(CurrentQueueSize);
            this.getCollectionWeeklyReleaseTimes.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(SortedReleaseTimes);
            this.queuedPostReleaseTimeCalculator
                .Setup(_ => _.GetQueuedPostReleaseTime(It.IsAny<DateTime>(), SortedReleaseTimes, CurrentQueueSize))
                .Returns(CalculatedLiveDate);

            var result = await this.target.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(Requester, CollectionId));

            Assert.AreEqual(result, CalculatedLiveDate);
        }
    }
}