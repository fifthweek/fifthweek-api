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

    public class GetFreeAccessUsersQueryHandlerTests
    {
        private static readonly GetFreeAccessUsersQuery Query =
            new GetFreeAccessUsersQuery(
                Requester.Authenticated(new UserId(Guid.NewGuid())),
                new BlogId(Guid.NewGuid()));

        private static readonly UserId UserId = new UserId(Guid.NewGuid());

        private static readonly GetFreeAccessUsersResult Result =
            new GetFreeAccessUsersResult(
            new List<GetFreeAccessUsersResult.FreeAccessUser>
                {
                    new GetFreeAccessUsersResult.FreeAccessUser(
                        new Email("a@b.com"),
                        new UserId(Guid.NewGuid()),
                        new Username("username"))
                });

        private Mock<IBlogSecurity> blogSecurity;
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IGetFreeAccessUsersDbStatement> getFreeAccessUsers;

        private GetFreeAccessUsersQueryHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.blogSecurity = new Mock<IBlogSecurity>();
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Query.Requester);
            this.getFreeAccessUsers = new Mock<IGetFreeAccessUsersDbStatement>(MockBehavior.Strict);

            this.blogSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, Query.BlogId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.target = new GetFreeAccessUsersQueryHandler(
                this.blogSecurity.Object,
                this.requesterSecurity.Object,
                this.getFreeAccessUsers.Object);
        }

        private void SetupDbStatement()
        {
            this.getFreeAccessUsers.Setup(v => v.ExecuteAsync(Query.BlogId)).ReturnsAsync(Result).Verifiable();
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
            await this.target.HandleAsync(new GetFreeAccessUsersQuery(
                Requester.Unauthenticated,
                Query.BlogId));
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldCheckTheAuthenticatedUserOwnsTheBlog()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Query);
            this.blogSecurity.Verify();
        }

        [TestMethod]
        public async Task WhenQueryIsValid_ItShouldGetFreeAccessUsers()
        {
            this.SetupDbStatement();
            await this.target.HandleAsync(Query);
            this.getFreeAccessUsers.Verify();
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