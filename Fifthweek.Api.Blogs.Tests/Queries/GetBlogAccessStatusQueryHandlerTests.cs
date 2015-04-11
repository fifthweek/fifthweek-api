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
    public class GetBlogAccessStatusQueryHandlerTests
    {
        private static readonly UserId RequesterId = new UserId(Guid.NewGuid());
        private static readonly GetBlogAccessStatusQuery Query =
            new GetBlogAccessStatusQuery(
                Requester.Authenticated(RequesterId),
                new BlogId(Guid.NewGuid()));


        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IDoesUserHaveFreeAccessDbStatement> doesUserHaveFreeAccess;

        private GetBlogAccessStatusQueryHandler target;

        private bool userHasFreeAccess;

        [TestInitialize]
        public void Initialize()
        {
            this.userHasFreeAccess = true;

            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);
            this.doesUserHaveFreeAccess = new Mock<IDoesUserHaveFreeAccessDbStatement>(MockBehavior.Strict);
            
            this.target = new GetBlogAccessStatusQueryHandler(
                this.requesterSecurity.Object,
                this.doesUserHaveFreeAccess.Object);
        }

        private void SetupDbStatement()
        {
            this.doesUserHaveFreeAccess.Setup(v => v.ExecuteAsync(Query.BlogId, RequesterId))
                .Returns(() => Task.FromResult(this.userHasFreeAccess))
                .Verifiable();
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
            await this.target.HandleAsync(new GetBlogAccessStatusQuery(
                Requester.Unauthenticated,
                Query.BlogId));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldGetFreeAccessStatus()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Query);
            this.doesUserHaveFreeAccess.Verify();
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldReturnTheResult()
        {
            this.SetupDbStatement();
            var result = await this.target.HandleAsync(Query);
            Assert.AreEqual(new GetBlogAccessStatusResult(true), result);
        }

        [TestMethod]
        public async Task WhenQueryIsValid_AndUserDoesNotHaveFreeAccess_ItShouldReturnFalseForFreeAccessStatus()
        {
            this.SetupDbStatement();
            this.userHasFreeAccess = false;
            var result = await this.target.HandleAsync(Query);
            Assert.AreEqual(new GetBlogAccessStatusResult(false), result);
        }
    }
}