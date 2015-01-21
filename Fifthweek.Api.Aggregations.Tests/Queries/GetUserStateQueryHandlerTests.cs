namespace Fifthweek.Api.Aggregations.Tests.Queries
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserStateQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private GetUserStateQueryHandler target;

        private Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>> getCreatorStatus;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getCreatorStatus = new Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>>(MockBehavior.Strict);
            this.target = new GetUserStateQueryHandler(this.getCreatorStatus.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalled_ItShouldCheckTheQueryIsNotNull()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalled_ItShouldCheckTheAuthenticatedUserIsCorrect()
        {
            await this.target.HandleAsync(new GetUserStateQuery(new UserId(Guid.NewGuid()), Requester, true));
        }

        [TestMethod]
        public async Task WhenCalledAsAUser_ItShouldReturnUserStateWithoutCreatorState()
        {
            var result = await this.target.HandleAsync(new GetUserStateQuery(UserId, Requester, false));

            Assert.IsNotNull(result);
            Assert.IsNull(result.CreatorStatus);
        }

        [TestMethod]
        public async Task WhenCalledAsACreator_ItShouldReturnUserStateWithCreatorState()
        {
            var creatorStatus = new CreatorStatus(new SubscriptionId(Guid.NewGuid()), true);
            this.getCreatorStatus.Setup(v => v.HandleAsync(new GetCreatorStatusQuery(Requester))).ReturnsAsync(creatorStatus);
            var result = await this.target.HandleAsync(new GetUserStateQuery(UserId, Requester, true));

            Assert.IsNotNull(result);
            Assert.AreEqual(creatorStatus, result.CreatorStatus);
        }
    }
}