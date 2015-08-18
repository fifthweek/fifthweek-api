namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetBlogSubscriberInformationQueryHandlerTests
    {
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;
        private static readonly DateTime ReleasableRevenueDate = Now.AddDays(-BlogsConstants.ReleaseableRevenueDays);

        private static readonly GetBlogSubscriberInformationQuery Query =
            new GetBlogSubscriberInformationQuery(
                Requester.Authenticated(UserId),
                BlogId);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IBlogSecurity> blogSecurity;
        private Mock<IFileInformationAggregator> fileInformationAggregator;
        private Mock<IGetBlogSubscriberInformationDbStatement> getBlogSubscriberInformation;
        private Mock<IGetCreatorRevenueDbStatement> getCreatorRevenue;
        private Mock<ITimestampCreator> timestampCreator;

        private GetBlogSubscriberInformationQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);

            this.blogSecurity = new Mock<IBlogSecurity>(MockBehavior.Strict);
            this.fileInformationAggregator = new Mock<IFileInformationAggregator>(MockBehavior.Strict);
            this.getBlogSubscriberInformation = new Mock<IGetBlogSubscriberInformationDbStatement>(MockBehavior.Strict);
            this.getCreatorRevenue = new Mock<IGetCreatorRevenueDbStatement>(MockBehavior.Strict);

            this.timestampCreator = new Mock<ITimestampCreator>(MockBehavior.Strict);
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.target = new GetBlogSubscriberInformationQueryHandler(
                this.requesterSecurity.Object,
                this.blogSecurity.Object,
                this.fileInformationAggregator.Object,
                this.getBlogSubscriberInformation.Object,
                this.getCreatorRevenue.Object,
                this.timestampCreator.Object);
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
            await this.target.HandleAsync(new GetBlogSubscriberInformationQuery(
                Requester.Unauthenticated,
                BlogId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUserIsNotBlogOwner_ItShouldThrowAnException()
        {
            this.blogSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, BlogId)).Throws(new UnauthorizedException());
            
            await this.target.HandleAsync(Query);
        }

        [TestMethod]
        public async Task WhenNoSubscribers_ItShouldReturnNoSubscribers()
        {
            this.blogSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, BlogId)).Returns(Task.FromResult(0));

            this.getCreatorRevenue.Setup(v => v.ExecuteAsync(UserId, ReleasableRevenueDate))
                .ReturnsAsync(new GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult(10, 20, 30));

            this.getBlogSubscriberInformation.Setup(v => v.ExecuteAsync(BlogId)).ReturnsAsync(
                new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult(
                    new List<GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber>()));

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(10, result.UnreleasedRevenue);
            Assert.AreEqual(20, result.ReleasedRevenue);
            Assert.AreEqual(30, result.ReleasableRevenue);
            Assert.AreEqual(0, result.Subscribers.Count);
        }

        [TestMethod]
        public async Task WhenSubscribers_ItShouldReturnSubscribersWithFileInformation()
        {
            this.blogSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, BlogId)).Returns(Task.FromResult(0));

            this.getCreatorRevenue.Setup(v => v.ExecuteAsync(UserId, ReleasableRevenueDate))
                .ReturnsAsync(new GetCreatorRevenueDbStatement.GetCreatorRevenueDbStatementResult(10, 20, 30));

            var subscriber1 = new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                new Username(Guid.NewGuid().ToString()),
                UserId.Random(),
                new FileId(Guid.NewGuid()),
                ChannelId.Random(),
                DateTime.UtcNow.AddDays(-1),
                10,
                new Email("a@b.com"),
                PaymentStatus.Retry2,
                true);

            var subscriber2 = new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                new Username(Guid.NewGuid().ToString()),
                UserId.Random(),
                new FileId(Guid.NewGuid()),
                ChannelId.Random(),
                DateTime.UtcNow.AddDays(-2),
                20,
                new Email("c@d.com"),
                PaymentStatus.None,
                false);

            var subscriber3 = new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                subscriber1.Username,
                subscriber1.UserId,
                subscriber1.ProfileImageFileId,
                ChannelId.Random(),
                DateTime.UtcNow.AddDays(-3),
                30,
                new Email("a@b.com"),
                PaymentStatus.Failed,
                true);

            var subscriber4 = new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber(
                new Username(Guid.NewGuid().ToString()),
                UserId.Random(),
                null,
                ChannelId.Random(),
                DateTime.UtcNow.AddDays(-4),
                40,
                null,
                PaymentStatus.None,
                true);

            this.getBlogSubscriberInformation.Setup(v => v.ExecuteAsync(Query.BlogId)).ReturnsAsync(
                new GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult(
                    new List<GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult.Subscriber>
                    {
                        subscriber1,
                        subscriber2,
                        subscriber3,
                        subscriber4
                    }));

            var fileInformation1 = new FileInformation(subscriber1.ProfileImageFileId, "container1");
            var fileInformation2 = new FileInformation(subscriber2.ProfileImageFileId, "container2");
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(null, subscriber1.ProfileImageFileId, FilePurposes.ProfileImage))
                .ReturnsAsync(fileInformation1);
            this.fileInformationAggregator.Setup(v => v.GetFileInformationAsync(null, subscriber2.ProfileImageFileId, FilePurposes.ProfileImage))
                .ReturnsAsync(fileInformation2);

            var result = await this.target.HandleAsync(Query);

            Assert.AreEqual(10, result.UnreleasedRevenue);
            Assert.AreEqual(20, result.ReleasedRevenue);
            Assert.AreEqual(30, result.ReleasableRevenue);
            CollectionAssert.AreEqual(
                new List<BlogSubscriberInformation.Subscriber>
                {
                    new BlogSubscriberInformation.Subscriber(
                        subscriber1.Username,
                        subscriber1.UserId,
                        fileInformation1,
                        subscriber1.FreeAccessEmail,
                        PaymentStatus.Retry2,
                        true,
                        new List<BlogSubscriberInformation.SubscriberChannel>
                        {
                            new BlogSubscriberInformation.SubscriberChannel(
                                subscriber1.ChannelId,
                                subscriber1.SubscriptionStartDate,
                                subscriber1.AcceptedPrice),
                            new BlogSubscriberInformation.SubscriberChannel(
                                subscriber3.ChannelId,
                                subscriber3.SubscriptionStartDate,
                                subscriber3.AcceptedPrice),
                        }),
                    new BlogSubscriberInformation.Subscriber(
                        subscriber2.Username,
                        subscriber2.UserId,
                        fileInformation2,
                        subscriber2.FreeAccessEmail,
                        PaymentStatus.None,
                        false,
                        new List<BlogSubscriberInformation.SubscriberChannel>
                        {
                            new BlogSubscriberInformation.SubscriberChannel(
                                subscriber2.ChannelId,
                                subscriber2.SubscriptionStartDate,
                                subscriber2.AcceptedPrice),
                        }),
                    new BlogSubscriberInformation.Subscriber(
                        subscriber4.Username,
                        subscriber4.UserId,
                        null,
                        subscriber4.FreeAccessEmail,
                        PaymentStatus.None,
                        true,
                        new List<BlogSubscriberInformation.SubscriberChannel>
                        {
                            new BlogSubscriberInformation.SubscriberChannel(
                                subscriber4.ChannelId,
                                subscriber4.SubscriptionStartDate,
                                subscriber4.AcceptedPrice),
                        }),
                },
                result.Subscribers.ToList());
        }
    }
}