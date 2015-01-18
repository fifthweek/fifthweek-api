namespace Fifthweek.Api.Collections.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Controllers;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CollectionControllerTests
    {
        private static readonly DateTime NewQueuedPostLiveDate = DateTime.UtcNow;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());

        private Mock<IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime>> getLiveDateOfNewQueuedPost;
        private Mock<IUserContext> userContext;
        private CollectionController target;

        [TestInitialize]
        public void Initialize()
        {
            this.getLiveDateOfNewQueuedPost = new Mock<IQueryHandler<GetLiveDateOfNewQueuedPostQuery, DateTime>>();
            this.userContext = new Mock<IUserContext>();

            this.target = new CollectionController(this.getLiveDateOfNewQueuedPost.Object, this.userContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingLiveDateOfNewQueuedPost_ItShouldReturnResultFromLiveDateOfNewQueuedPostQuery()
        {
            var query = new GetLiveDateOfNewQueuedPostQuery(Requester.Authenticated(UserId), CollectionId);

            this.userContext.Setup(_ => _.TryGetUserId()).Returns(UserId);
            this.getLiveDateOfNewQueuedPost.Setup(_ => _.HandleAsync(query)).ReturnsAsync(NewQueuedPostLiveDate);

            var result = await this.target.GetLiveDateOfNewQueuedPost(CollectionId.Value.EncodeGuid());

            Assert.AreEqual(result, NewQueuedPostLiveDate);
        }
    }
}