namespace Fifthweek.Api.Collections.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetLiveDateOfNewQueuedPostQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());

        private Mock<ICollectionSecurity> collectionSecurity;
        private GetLiveDateOfNewQueuedPostQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.target = new GetLiveDateOfNewQueuedPostQueryHandler(this.collectionSecurity.Object);
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
    }
}