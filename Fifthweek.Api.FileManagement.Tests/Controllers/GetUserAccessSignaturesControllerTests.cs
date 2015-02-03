namespace Fifthweek.Api.FileManagement.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Controllers;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserAccessSignaturesControllerTests
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
                            new UserId(Guid.NewGuid()),
                            new BlobContainerSharedAccessInformation(
                            "containerName2",
                            "uri2",
                            "signature2",
                            DateTime.UtcNow)),
                    });

        private UserAccessSignaturesController target;
        private Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>> getUserState;
        private Mock<IRequesterContext> requesterContext;

        [TestInitialize]
        public void TestInitialize()
        {
            this.getUserState = new Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>>();
            this.requesterContext = new Mock<IRequesterContext>();
            this.target = new UserAccessSignaturesController(this.getUserState.Object, this.requesterContext.Object);
        }

        [TestMethod]
        public async Task WhenGettingUserAccessSignatures_ItShouldReturnResultFromUserAccessSignaturesQuery()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.AreEqual(UserAccessSignatures, result);
        }

        [TestMethod]
        public async Task WhenGettingUserAccessSignatures_ItShouldReturnResultFromUserAccessSignaturesQuery2()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester.Unauthenticated);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, UserId)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.Get(UserId.Value.EncodeGuid());

            Assert.AreEqual(UserAccessSignatures, result);
        }

        [TestMethod]
        public async Task WhenGettingUserAccessSignatures_ItShouldReturnResultFromUserAccessSignaturesQuery3()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester.Unauthenticated);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.Get(null);

            Assert.AreEqual(UserAccessSignatures, result);
        }

        [TestMethod]
        public async Task WhenGettingUserAccessSignatures_ItShouldReturnResultFromUserAccessSignaturesQuery4()
        {
            this.requesterContext.Setup(v => v.GetRequester()).Returns(Requester);

            this.getUserState.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, null)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.Get(" ");

            Assert.AreEqual(UserAccessSignatures, result);
        }
    }
}