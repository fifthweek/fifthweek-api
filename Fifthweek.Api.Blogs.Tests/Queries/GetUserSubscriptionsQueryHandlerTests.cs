namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
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
            new GetUserSubscriptionsQuery(Requester.Authenticated(UserId));

        private static readonly IReadOnlyList<BlogSubscriptionDbResult> DatabaseResult =
            new List<BlogSubscriptionDbResult>
            {
                new BlogSubscriptionDbResult(
                    new BlogId(Guid.NewGuid()),
                    "Blog1",
                    new UserId(Guid.NewGuid()),
                    new Username("Username1"),
                    new FileId(Guid.NewGuid()),
                    true,
                    new List<ChannelSubscriptionStatus>()),
                new BlogSubscriptionDbResult(
                    new BlogId(Guid.NewGuid()),
                    "Blog2",
                    new UserId(Guid.NewGuid()),
                    new Username("Username2"),
                    null,
                    true,
                    new List<ChannelSubscriptionStatus>()),
            };

        private static readonly GetUserSubscriptionsResult Result =
            new GetUserSubscriptionsResult(new List<BlogSubscriptionStatus>
                {
                    new BlogSubscriptionStatus(
                        DatabaseResult[0].BlogId,
                        DatabaseResult[0].Name,
                        DatabaseResult[0].CreatorId,
                        DatabaseResult[0].CreatorUsername,
                        new FileInformation(DatabaseResult[0].ProfileImageFileId, "Container1"),
                        DatabaseResult[0].FreeAccess,
                        DatabaseResult[0].Channels),
                    new BlogSubscriptionStatus(
                        DatabaseResult[1].BlogId,
                        DatabaseResult[1].Name,
                        DatabaseResult[1].CreatorId,
                        DatabaseResult[1].CreatorUsername,
                        null,
                        DatabaseResult[1].FreeAccess,
                        DatabaseResult[1].Channels),
                });

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
                .Setup(v => v.GetFileInformationAsync(DatabaseResult[0].CreatorId, DatabaseResult[0].ProfileImageFileId, FilePurposes.ProfileImage))
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
        public async Task WhenUserIsUnautorized_ItShouldThrowAnException()
        {
            await this.target.HandleAsync(new GetUserSubscriptionsQuery(Requester.Unauthenticated));
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