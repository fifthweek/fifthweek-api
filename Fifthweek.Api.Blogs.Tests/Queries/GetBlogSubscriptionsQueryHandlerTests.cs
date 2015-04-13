namespace Fifthweek.Api.Blogs.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetBlogSubscriptionsQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly GetBlogSubscriptionsQuery Query =
            new GetBlogSubscriptionsQuery(Requester.Authenticated(UserId));

        private static readonly GetBlogSubscriptionsResult Result =
            new GetBlogSubscriptionsResult(new List<BlogSubscriptionStatus>());

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetBlogSubscriptionsDbStatement> getBlogSubscriptions;

        private GetBlogSubscriptionsQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);
            this.getBlogSubscriptions = new Mock<IGetBlogSubscriptionsDbStatement>(MockBehavior.Strict);

            this.target = new GetBlogSubscriptionsQueryHandler(
                this.requesterSecurity.Object,
                this.getBlogSubscriptions.Object);
        }

        private void SetupDbStatement()
        {
            this.getBlogSubscriptions.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Result).Verifiable();
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
            await this.target.HandleAsync(new GetBlogSubscriptionsQuery(Requester.Unauthenticated));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldGetFreeAccessUsers()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Query);
            this.getBlogSubscriptions.Verify();
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