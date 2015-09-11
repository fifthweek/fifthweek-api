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
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Queries;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Queries;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Membership.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetUserStateQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
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

        private static readonly GetUserSubscriptionsResult UserSubscriptions =
            new GetUserSubscriptionsResult(new List<BlogSubscriptionStatus>
                {
                    new BlogSubscriptionStatus(new BlogId(Guid.NewGuid()), "name", new UserId(Guid.NewGuid()), new Username("username"), null, false, new List<ChannelSubscriptionStatus> { new ChannelSubscriptionStatus(new ChannelId(Guid.NewGuid()), "name", 10, 10, true, DateTime.UtcNow, DateTime.UtcNow, true) }),
                    new BlogSubscriptionStatus(new BlogId(Guid.NewGuid()), "name2", new UserId(Guid.NewGuid()), new Username("username2"), null, false, new List<ChannelSubscriptionStatus> { new ChannelSubscriptionStatus(new ChannelId(Guid.NewGuid()), "name2", 20, 20, true, DateTime.UtcNow, DateTime.UtcNow, true) })
                });

        private GetUserStateQueryHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>> getUserAccessSignatures;
        private Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>> getAccountSettings;
        private Mock<IQueryHandler<GetBlogChannelsAndQueuesQuery, GetBlogChannelsAndQueuesResult>> getBlogChannelsAndCollections;
        private Mock<IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>> getBlogSubscriptions;
        private Mock<IImpersonateIfRequired> impersonateIfRequired;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();

            this.getUserAccessSignatures = new Mock<IQueryHandler<GetUserAccessSignaturesQuery, UserAccessSignatures>>();

            // Give potentially side-effecting components strict mock behaviour.
            this.getAccountSettings = new Mock<IQueryHandler<GetAccountSettingsQuery, GetAccountSettingsResult>>(MockBehavior.Strict);
            this.getBlogChannelsAndCollections = new Mock<IQueryHandler<GetBlogChannelsAndQueuesQuery, GetBlogChannelsAndQueuesResult>>(MockBehavior.Strict);
            this.getBlogSubscriptions = new Mock<IQueryHandler<GetUserSubscriptionsQuery, GetUserSubscriptionsResult>>(MockBehavior.Strict);
            this.impersonateIfRequired = new Mock<IImpersonateIfRequired>(MockBehavior.Strict);

            this.target = new GetUserStateQueryHandler(
                this.requesterSecurity.Object, 
                this.getUserAccessSignatures.Object,
                this.getAccountSettings.Object,
                this.getBlogChannelsAndCollections.Object,
                this.getBlogSubscriptions.Object,
                this.impersonateIfRequired.Object);
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
            await this.target.HandleAsync(new GetUserStateQuery(Requester, new UserId(Guid.NewGuid()), false, Now));
        }

        [TestMethod]
        public async Task WhenCalledAsASignedOutUser_ItShouldReturnAnonymousUserStateWithoutCreatorState()
        {
            // Should not be called.
            this.requesterSecurity = new Mock<IRequesterSecurity>(MockBehavior.Strict);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester.Unauthenticated, null, null, null)))
                .ReturnsAsync(UserAccessSignatures);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester.Unauthenticated, null, false, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.IsNull(result.AccountSettings);
            Assert.IsNull(result.Blog);
            Assert.IsNull(result.Subscriptions);
        }

        [TestMethod]
        public async Task WhenCalledAsAUser_ItShouldReturnRegisteredUserStateWithoutCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null, 10, PaymentStatus.Retry1, true, 1m, null);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, null, new List<ChannelId> { UserSubscriptions.Blogs[0].Channels[0].ChannelId, UserSubscriptions.Blogs[1].Channels[0].ChannelId })))
                .ReturnsAsync(UserAccessSignatures);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now)))
                .ReturnsAsync(accountSettings);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester, UserId)))
                .ReturnsAsync(UserSubscriptions);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId, false, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(accountSettings, result.AccountSettings);

            Assert.IsNull(result.Blog);
        }

        [TestMethod]
        public async Task WhenCalledAsAUser_AndImpersonationRequested_ItShouldReturnRegisteredUserStateWithoutCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null, 10, PaymentStatus.Retry1, true, 1m, null);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, null, new List<ChannelId> { UserSubscriptions.Blogs[0].Channels[0].ChannelId, UserSubscriptions.Blogs[1].Channels[0].ChannelId })))
                .ReturnsAsync(UserAccessSignatures);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now)))
                .ReturnsAsync(accountSettings);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester, UserId)))
                .ReturnsAsync(UserSubscriptions);

            var impersonatingUserId = UserId.Random();
            var impersonatingRequester = Requester.Authenticated(impersonatingUserId);
            this.impersonateIfRequired.Setup(v => v.ExecuteAsync(impersonatingRequester, UserId))
                .ReturnsAsync(Requester);

            var result = await this.target.HandleAsync(
                new GetUserStateQuery(impersonatingRequester, UserId, true, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(accountSettings, result.AccountSettings);

            Assert.IsNull(result.Blog);
        }

        [TestMethod]
        public async Task WhenCalledAsAUser_AndImpersonationRequestedButNotRequired_ItShouldReturnRegisteredUserStateWithoutCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null, 10, PaymentStatus.Retry1, true, 1m, null);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, null, new List<ChannelId> { UserSubscriptions.Blogs[0].Channels[0].ChannelId, UserSubscriptions.Blogs[1].Channels[0].ChannelId })))
                .ReturnsAsync(UserAccessSignatures);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now)))
                .ReturnsAsync(accountSettings);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester, UserId)))
                .ReturnsAsync(UserSubscriptions);

            this.impersonateIfRequired.Setup(v => v.ExecuteAsync(Requester, UserId))
                .ReturnsAsync(null);

            var result = await this.target.HandleAsync(
                new GetUserStateQuery(Requester, UserId, true, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(accountSettings, result.AccountSettings);

            Assert.IsNull(result.Blog);
        }

        [TestMethod]
        public async Task WhenCalledAsACreator_ItShouldReturnUserStateWithCreatorState()
        {
            this.requesterSecurity.SetupFor(Requester);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var creatorStatus = new CreatorStatus(new BlogId(Guid.NewGuid()), true);
            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null, 10, PaymentStatus.Retry1, true, 1m, null);
            var blogChannelsAndCollections = new GetBlogChannelsAndQueuesResult(
                new BlogWithFileInformation(new BlogId(Guid.NewGuid()), new BlogName("My Subscription"), new Introduction("Once upon a time there was an intro."), DateTime.UtcNow, null, null, null,
                    new List<ChannelResult> { new ChannelResult(new ChannelId(Guid.NewGuid()), "name", 10, true) },
                    new List<QueueResult> { new QueueResult(new QueueId(Guid.NewGuid()), "name", new List<HourOfWeek>()) }));

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, new List<ChannelId> { blogChannelsAndCollections.Blog.Channels[0].ChannelId }, new List<ChannelId> { UserSubscriptions.Blogs[0].Channels[0].ChannelId, UserSubscriptions.Blogs[1].Channels[0].ChannelId })))
                .ReturnsAsync(UserAccessSignatures);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester, UserId)))
                .ReturnsAsync(UserSubscriptions);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now)))
                .ReturnsAsync(accountSettings);
            this.getBlogChannelsAndCollections.Setup(v => v.HandleAsync(new GetBlogChannelsAndQueuesQuery(UserId)))
                .ReturnsAsync(blogChannelsAndCollections);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId, false, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(accountSettings, result.AccountSettings);
            Assert.AreEqual(blogChannelsAndCollections.Blog, result.Blog);
        }

        [TestMethod]
        public async Task WhenCalledAsACreatorWithNoBlog_ItShouldReturnUserStateWithoutABlog()
        {
            this.requesterSecurity.SetupFor(Requester);

            this.requesterSecurity.Setup(v => v.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var creatorStatus = new CreatorStatus(null, true);
            var accountSettings = new GetAccountSettingsResult(new Username("username"), new Email("a@b.com"), null, 10, PaymentStatus.Retry1, true, 1m, null);

            this.getUserAccessSignatures.Setup(v => v.HandleAsync(new GetUserAccessSignaturesQuery(Requester, UserId, null, new List<ChannelId> { UserSubscriptions.Blogs[0].Channels[0].ChannelId, UserSubscriptions.Blogs[1].Channels[0].ChannelId })))
                .ReturnsAsync(UserAccessSignatures);
            this.getBlogSubscriptions.Setup(v => v.HandleAsync(new GetUserSubscriptionsQuery(Requester, UserId)))
                .ReturnsAsync(UserSubscriptions);
            this.getAccountSettings.Setup(v => v.HandleAsync(new GetAccountSettingsQuery(Requester, UserId, Now)))
                .ReturnsAsync(accountSettings);
            this.getBlogChannelsAndCollections.Setup(v => v.HandleAsync(new GetBlogChannelsAndQueuesQuery(UserId)))
                .ReturnsAsync(null);

            var result = await this.target.HandleAsync(new GetUserStateQuery(Requester, UserId, false, Now));

            Assert.IsNotNull(result);
            Assert.AreEqual(UserAccessSignatures, result.AccessSignatures);
            Assert.AreEqual(UserSubscriptions, result.Subscriptions);
            Assert.AreEqual(accountSettings, result.AccountSettings);
            Assert.IsNull(result.Blog);
        }
    }
}