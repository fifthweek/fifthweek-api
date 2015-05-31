namespace Fifthweek.Api.Aggregations.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Controllers;
    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserStateControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId, FifthweekRole.Creator);

        private static readonly UserAccessSignatures UserAccessSignatures =
            new UserAccessSignatures(
                100,
                new BlobContainerSharedAccessInformation("containerName", "uri", "signature", DateTime.UtcNow),
                new List<UserAccessSignatures.PrivateAccessSignature>
                    {
                        new UserAccessSignatures.
                            PrivateAccessSignature(
                            new ChannelId(Guid.NewGuid()),
                            new BlobContainerSharedAccessInformation(
                            "containerName2",
                            "uri2",
                            "signature2",
                            DateTime.UtcNow)),
                    });

        private static readonly UserState UserState = new UserState(UserAccessSignatures, null, null, null, null, null);

        private UserStateController target;
        private Mock<IQueryHandler<GetUserStateQuery, UserState>> getUserState;
        private Mock<IRequesterContext> requesterContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserState = new Mock<IQueryHandler<GetUserStateQuery, UserState>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.target = new UserStateController(this.getUserState.Object, this.requesterContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);
            
            this.getUserState.Setup(v => v.HandleAsync(new GetUserStateQuery(Requester, UserId)))
                .ReturnsAsync(UserState);

            var result = await this.target.GetUserState(UserId.Value.EncodeGuid());

            Assert.AreEqual(UserState, result);
        }

        [TestMethod]
        public async Task WhenGettingVisitorState_ItShouldReturnResultFromUserStateQuery()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester.Unauthenticated);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserStateQuery(Requester.Unauthenticated, null)))
                .ReturnsAsync(UserState);

            var result = await this.target.GetVisitorState();

            Assert.AreEqual(UserState, result);
        }
    }
}