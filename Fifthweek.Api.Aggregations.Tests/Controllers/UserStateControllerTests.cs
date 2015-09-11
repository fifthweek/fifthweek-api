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
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UserStateControllerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId, FifthweekRole.Creator);
        private static readonly DateTime Now = DateTime.UtcNow;

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

        private static readonly UserState UserState = new UserState(UserAccessSignatures, null, null, null);

        private UserStateController target;
        private Mock<IQueryHandler<GetUserStateQuery, UserState>> getUserState;
        private Mock<IRequesterContext> requesterContext;
        private Mock<ITimestampCreator> timestampCreator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserState = new Mock<IQueryHandler<GetUserStateQuery, UserState>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.timestampCreator = new Mock<ITimestampCreator>();

            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.target = new UserStateController(this.getUserState.Object, this.timestampCreator.Object, this.requesterContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingUserState_ItShouldReturnResultFromUserStateQuery()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserStateQuery(Requester, UserId, false, Now)))
                .ReturnsAsync(UserState);

            var result = await this.target.GetUserState(UserId.Value.EncodeGuid());

            Assert.AreEqual(UserState, result);
        }

        [TestMethod]
        public async Task WhenGettingUserState_AndImpersonating_ItShouldReturnResultFromUserStateQuery()
        {
            this.requesterContext.Setup(_ => _.GetRequesterAsync()).ReturnsAsync(Requester);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserStateQuery(Requester, UserId, true, Now)))
                .ReturnsAsync(UserState);

            var result = await this.target.GetUserState(UserId.Value.EncodeGuid(), true);

            Assert.AreEqual(UserState, result);
        }

        [TestMethod]
        public async Task WhenGettingVisitorState_ItShouldReturnResultFromUserStateQuery()
        {
            this.requesterContext.Setup(v => v.GetRequesterAsync()).ReturnsAsync(Requester.Unauthenticated);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserStateQuery(Requester.Unauthenticated, null, false, Now)))
                .ReturnsAsync(UserState);

            var result = await this.target.GetVisitorState();

            Assert.AreEqual(UserState, result);
        }
    }
}