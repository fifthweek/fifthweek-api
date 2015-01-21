namespace Fifthweek.Api.Aggregations.Tests.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Controllers;
    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.OAuth;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Subscriptions;
    using Fifthweek.Api.Subscriptions.Queries;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserStateControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly SubscriptionId SubscriptionId = new SubscriptionId(Guid.NewGuid());

        private UserStateController target;
        private Mock<IQueryHandler<GetUserStateQuery, UserState>> getUserState;
        private Mock<IUserContext> userContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserState = new Mock<IQueryHandler<GetUserStateQuery, UserState>>();
            this.userContext = new Mock<IUserContext>();
            this.target = new UserStateController(this.getUserState.Object, this.userContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId);
            this.userContext.Setup(v => v.IsUserInRole(FifthweekRole.Creator)).Returns(true);
            
            var query = new GetUserStateQuery(UserId, Requester.Authenticated(UserId), true);
            this.getUserState.Setup(v => v.HandleAsync(query)).ReturnsAsync(new UserState(new CreatorStatus(SubscriptionId, false)));

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.AreEqual(result.CreatorStatus.SubscriptionId, SubscriptionId.Value.EncodeGuid());
            Assert.AreEqual(result.CreatorStatus.MustWriteFirstPost, false);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery2()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId);
            this.userContext.Setup(v => v.IsUserInRole(FifthweekRole.Creator)).Returns(true);
           
            var query = new GetUserStateQuery(UserId, Requester.Authenticated(UserId), true);
            this.getUserState.Setup(v => v.HandleAsync(query)).ReturnsAsync(new UserState(new CreatorStatus(SubscriptionId, true)));

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.AreEqual(result.CreatorStatus.SubscriptionId, SubscriptionId.Value.EncodeGuid());
            Assert.AreEqual(result.CreatorStatus.MustWriteFirstPost, true);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery3()
        {
            this.userContext.Setup(v => v.TryGetUserId()).Returns(UserId);
            this.userContext.Setup(v => v.IsUserInRole(FifthweekRole.Creator)).Returns(false);
            
            var query = new GetUserStateQuery(UserId, Requester.Authenticated(UserId), false);
            this.getUserState.Setup(v => v.HandleAsync(query)).ReturnsAsync(new UserState(null));

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.IsNull(result.CreatorStatus);
        }
    }
}