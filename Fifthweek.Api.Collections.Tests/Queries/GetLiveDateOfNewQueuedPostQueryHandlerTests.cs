namespace Fifthweek.Api.Collections.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetLiveDateOfNewQueuedPostQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DateTime CalculatedLiveDate = DateTime.UtcNow.AddDays(8);
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetLiveDateOfNewQueuedPostDbStatement> getLiveDateOfNewQueuedPost;
        private GetLiveDateOfNewQueuedPostQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.getLiveDateOfNewQueuedPost = new Mock<IGetLiveDateOfNewQueuedPostDbStatement>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            
            this.requesterSecurity.SetupFor(Requester);

            this.target = new GetLiveDateOfNewQueuedPostQueryHandler(
                this.collectionSecurity.Object,
                this.requesterSecurity.Object,
                this.getLiveDateOfNewQueuedPost.Object);
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
            this.getLiveDateOfNewQueuedPost.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(CalculatedLiveDate);

            var result = await this.target.HandleAsync(new GetLiveDateOfNewQueuedPostQuery(Requester, CollectionId));

            Assert.AreEqual(result, CalculatedLiveDate);
        }
    }
}