namespace Fifthweek.Api.Aggregations.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Aggregations.Queries;
    using Fifthweek.Api.Azure;
    using Fifthweek.Api.Blogs;
    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserStateQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

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

        private static readonly GetUserSubscriptionsResult UserSubscriptions =
            new GetUserSubscriptionsResult(new List<BlogSubscriptionStatus>());

        private GetUserStateQueryHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>> getUserAccessSignatures;
        private Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>> getCreatorStatus;
        private Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>> getAccountSettings;
        private Mock<IQueryHandler<GetBlogChannelsAndCollectionsQuery, GetBlogChannelsAndCollectionsResult>> getBlogChannelsAndCollections;
        private Mock<IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>> getBlogSubscriptions;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();

            this.getUserAccessSignatures = new Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>>();

            // Give potentially side-effecting components strict mock behaviour.
            this.getCreatorStatus = new Mock<IQueryHandler<GetCreatorStatusQuery, CreatorStatus>>(MockBehavior.Strict);
            this.getAccountSettings = new Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>>(MockBehavior.Strict);
            this.getBlogChannelsAndCollections = new Mock<IQueryHandler<GetBlogChannelsAndCollectionsQuery, GetBlogChannelsAndCollectionsResult>>(MockBehavior.Strict);
            this.getBlogSubscriptions = new Mock<IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>>(MockBehavior.Strict);

            this.target = new GetUserStateQueryHandler(
                this.requesterSecurity.Object, 
                this.getUserAccessSignatures.Object,
                this.getCreatorStatus.Object, 
                this.getAccountSettings.Object,
                this.getBlogChannelsAndCollections.Object,
                this.getBlogSubscriptions.Object);
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
            this.requesterSecurity.SetupFor(Requester);
            await this.target.HandleAsync(new GetUserStateQuery(Requester, new UserId(Guid.NewGuid())));
        }

        [TestMethod]
        public async Task WhenCalledAsASignedOutUser_ItShouldReturnAnonymousUserStateWithoutCreatorState()
        {
            // Should not be called.
            this.requesterSecurity = new Mock<IRequesterSecurity>(MockBehavior.Strict);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester.Unauthenticated, null));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.IsNull(result.CreatorStatus);
            Assert.IsNull(result.CreatedChannelsAndCollections);
            Assert.IsNull(result.AccountSettings);
            Assert.IsNull(result.Blog);
            Assert.IsNull(result.Subscriptions);
        }

        [TestMethod]
        public async Task WhenCalledAsAUser_ItShouldReturnRegisteredUserStateWithoutCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId)))
                .ReturnsAsync(UserAccessSignatures);

            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester)))
                .ReturnsAsync(UserSubscriptions);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);

            Assert.IsNull(result.CreatorStatus);
            Assert.IsNull(result.CreatedChannelsAndCollections);
            Assert.IsNull(result.AccountSettings);
            Assert.IsNull(result.Blog);
        }

        [TestMethod]
        public async Task WhenCalledAsACreator_ItShouldReturnUserStateWithCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var creatorStatus = new CreatorStatus(new BlogId(Guid.NewGuid()), true);
            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null);
            var blogChannelsAndCollections = new GetBlogChannelsAndCollectionsResult(
                new BlogWithFileInformation(new BlogId(Guid.NewGuid()), UserId, new BlogName("My Subscription"), new Tagline("Tagline is great"), new Introduction("Once upon a time there was an intro."), DateTime.UtcNow, null, null, null),
                new ChannelsAndCollections(new List<ChannelsAndCollections.Channel>()));

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId)))
                .ReturnsAsync(UserAccessSignatures);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester)))
                .ReturnsAsync(UserSubscriptions);
            this.getCreatorStatus.Setup(v => v.HandleAsync(new GetCreatorStatusQuery(Requester, UserId)))
                .ReturnsAsync(creatorStatus);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId)))
                .ReturnsAsync(accountSettings);
            this.getBlogChannelsAndCollections.Setup(v => v.HandleAsync(new GetBlogChannelsAndCollectionsQuery(creatorStatus.BlogId)))
                .ReturnsAsync(blogChannelsAndCollections);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(creatorStatus, result.CreatorStatus);
            Assert.AreEqual(accountSettings, result.AccountSettings);
            Assert.AreEqual(blogChannelsAndCollections.Blog, result.Blog);
            Assert.AreEqual(blogChannelsAndCollections.ChannelsAndCollections, result.CreatedChannelsAndCollections);
        }

        [TestMethod]
        public async Task WhenCalledAsACreatorWithNoSubscription_ItShouldReturnUserStateWithoutSubscription()
        {
            this.requesterSecurity.SetupFor(Requester);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var creatorStatus = new CreatorStatus(null, true);
            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId)))
                .ReturnsAsync(UserAccessSignatures);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester)))
                .ReturnsAsync(UserSubscriptions);
            this.getCreatorStatus.Setup(v => v.HandleAsync(new GetCreatorStatusQuery(Requester, UserId)))
                .ReturnsAsync(creatorStatus);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId)))
                .ReturnsAsync(accountSettings);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(creatorStatus, result.CreatorStatus);
            Assert.AreEqual(accountSettings, result.AccountSettings);
            Assert.IsNull(result.Blog);
        }
    }
}