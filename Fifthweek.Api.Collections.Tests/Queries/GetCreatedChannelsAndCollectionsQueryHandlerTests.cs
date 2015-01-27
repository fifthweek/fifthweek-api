namespace Fifthweek.Api.Collections.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCreatedChannelsAndCollectionsQueryHandlerTests
    {
        public static readonly UserId UserId = new UserId(Guid.NewGuid());
        public static readonly Requester Requester = Requester.Authenticated(UserId);

        private GetCreatedChannelsAndCollectionsQueryHandler target;
        private Mock<IGetChannelsAndCollectionsDbStatement> getChannelsAndCollections;
        private Mock<IRequesterSecurity> requesterSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            // Give potentially side-effecting components strict mock behaviour.
            this.getChannelsAndCollections = new Mock<IGetChannelsAndCollectionsDbStatement>(MockBehavior.Strict);

            this.target = new GetCreatedChannelsAndCollectionsQueryHandler(this.requesterSecurity.Object, this.getChannelsAndCollections.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenCalled_ItShouldCheckQueryIsNotNull()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenCalled_ItShouldCheckTheRequestedUserMatchesTheAuthenticatedUser()
        {
            await this.target.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        public async Task WhenCalled_ItShouldCallTheDbStatement()
        {
            this.getChannelsAndCollections.Setup(v => v.ExecuteAsync(UserId))
                .ReturnsAsync(
                    new ChannelsAndCollections(new List<ChannelsAndCollections.Channel>()))
                .Verifiable();

            await this.target.HandleAsync(new GetCreatedChannelsAndCollectionsQuery(Requester, UserId));

            this.getChannelsAndCollections.Verify();
        }
    }
}