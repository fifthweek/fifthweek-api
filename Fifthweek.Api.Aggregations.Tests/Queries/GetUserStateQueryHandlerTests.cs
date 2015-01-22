namespace Fifthweek.Api.Aggregations.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Collections.Queries;
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
        private Mock<IQueryHandler<GetCreatedChannelsAndCollectionsQuery, ChannelsAndCollections>> getCreatedChannelsAndCollections;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getCreatorStatus = new Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>>(MockBehavior.Strict);
            this.getCreatedChannelsAndCollections = new Mock<IQueryHandler<GetCreatedChannelsAndCollectionsQuery,ChannelsAndCollections>>(MockBehavior.Strict);
            this.target = new GetUserStateQueryHandler(this.getCreatorStatus.Object, this.getCreatedChannelsAndCollections.Object);
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
            var createdChannelsAndCollections = new ChannelsAndCollections(new List<ChannelsAndCollections.Channel>());

            this.getCreatorStatus.Setup(v => v.HandleAsync(new GetCreatorStatusQuery(Requester)))
                .ReturnsAsync(creatorStatus);
            this.getCreatedChannelsAndCollections.Setup(v => v.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(Requester, UserId)))
                .ReturnsAsync(createdChannelsAndCollections);
            
            var result = await this.target.HandleAsync(new GetUserStateQuery(UserId, Requester, true));

            Assert.IsNotNull(result);
            Assert.AreEqual(creatorStatus, result.CreatorStatus);
            Assert.AreEqual(createdChannelsAndCollections, result.CreatedChannelsAndCollections);
        }
    }
}