namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserSubscriptionsQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly GetUserSubscriptionsQuery Query =
            new GetUserSubscriptionsQuery(Requester.Authenticated(UserId), UserId);

        private static readonly GetUserSubscriptionsDbResult DatabaseResult =
            new GetUserSubscriptionsDbResult(
                new List<BlogSubscriptionDbStatus>
                {
                    new BlogSubscriptionDbStatus(
                        new BlogId(Guid.NewGuid()),
                        "Blog1",
                        new UserId(Guid.NewGuid()),
                        new Username("Username1"),
                        new FileId(Guid.NewGuid()),
                        true,
                        new List<ChannelSubscriptionStatus>()),
                    new BlogSubscriptionDbStatus(
                        new BlogId(Guid.NewGuid()),
                        "Blog2",
                        new UserId(Guid.NewGuid()),
                        new Username("Username2"),
                        null,
                        true,
                        new List<ChannelSubscriptionStatus>()),
                },
                new List<ChannelId> 
                {
                    ChannelId.Random(),
                    ChannelId.Random(),
                });

        private static readonly GetUserSubscriptionsResult Result =
            new GetUserSubscriptionsResult(new List<BlogSubscriptionStatus>
                {
                    new BlogSubscriptionStatus(
                        DatabaseResult.Blogs[0].BlogId,
                        DatabaseResult.Blogs[0].Name,
                        DatabaseResult.Blogs[0].CreatorId,
                        DatabaseResult.Blogs[0].CreatorUsername,
                        new FileInformation(DatabaseResult.Blogs[0].ProfileImageFileId, "Container1"),
                        DatabaseResult.Blogs[0].FreeAccess,
                        DatabaseResult.Blogs[0].Channels),
                    new BlogSubscriptionStatus(
                        DatabaseResult.Blogs[1].BlogId,
                        DatabaseResult.Blogs[1].Name,
                        DatabaseResult.Blogs[1].CreatorId,
                        DatabaseResult.Blogs[1].CreatorUsername,
                        null,
                        DatabaseResult.Blogs[1].FreeAccess,
                        DatabaseResult.Blogs[1].Channels),
                },
                DatabaseResult.FreeAccessChannelIds);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetUserSubscriptionsDbStatement> getUserSubscriptions;
        private Mock<IFileInformationAggregator> fileInformationAggregator;

        private GetUserSubscriptionsQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);
            this.getUserSubscriptions = new Mock<IGetUserSubscriptionsDbStatement>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>();

            this.target = new GetUserSubscriptionsQueryHandler(
                this.requesterSecurity.Object,
                this.getUserSubscriptions.Object,
                this.fileInformationAggregator.Object);
        }

        private void SetupDbStatement()
        {
            this.fileInformationAggregator
                .Setup(v => v.GetFileInformationAsync(null, DatabaseResult.Blogs[0].ProfileImageFileId, FilePurposes.ProfileImage))
                .ReturnsAsync(Result.Blogs[0].ProfileImage);

            this.getUserSubscriptions.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(DatabaseResult).Verifiable();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenQueryIsNull_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsUnauthenticated_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserSubscriptionsQuery(Requester.Unauthenticated, UserId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsUnauthorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserSubscriptionsQuery(Query.Requester, UserId.Random()));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldGetUserSubscriptions()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Query);
            this.getUserSubscriptions.Verify();
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldReturnTheResult()
        {
            this.SetupDbStatement();
            var result = await this.target.HandleAsync(Query);
            Assert.AreEqual(Result, result);
        }
    }
}