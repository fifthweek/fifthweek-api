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
    public class GetUserSubscriptionsQueryHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly GetUserSubscriptionsQuery Query =
            new GetUserSubscriptionsQuery(Requester.Authenticated(UserId));

        private static readonly GetUserSubscriptionsResult Result =
            new GetUserSubscriptionsResult(new List<BlogSubscriptionStatus>());

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetUserSubscriptionsDbStatement> getUserSubscriptions;

        private GetUserSubscriptionsQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);
            this.getUserSubscriptions = new Mock<IGetUserSubscriptionsDbStatement>(MockBehavior.Strict);

            this.target = new GetUserSubscriptionsQueryHandler(
                this.requesterSecurity.Object,
                this.getUserSubscriptions.Object);
        }

        private void SetupDbStatement()
        {
            this.getUserSubscriptions.Setup(v => v.ExecuteAsync(UserId)).ReturnsAsync(Result).Verifiable();
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
        public async Task WhenQueryIsValid_ItShouldGetFreeAccessUsers()
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