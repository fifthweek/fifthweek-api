namespace Fifthweek.Api.Blogs.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class BlogSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly BlogId BlogId = new BlogId(Guid.NewGuid());
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IBlogOwnership> blogOwnership;
        private BlogSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.blogOwnership = new Mock<IBlogOwnership>();
            this.target = new BlogSecurity(this.requesterSecurity.Object, this.blogOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldAllowIfUserHasCreatorRole()
        {
            this.requesterSecurity.Setup(_ => _.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(true);

            var result = await this.target.IsCreationAllowedAsync(Requester);

            Assert.IsTrue(result);

            await this.target.AssertCreationAllowedAsync(Requester);
        }

        [TestMethod]
        public async Task WhenAuthorizingCreation_ItShouldForbidIfUserHasCreatorRole()
        {
            this.requesterSecurity.Setup(_ => _.IsInRoleAsync(Requester, FifthweekRole.Creator)).ReturnsAsync(false);

            var result = await this.target.IsCreationAllowedAsync(Requester);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertCreationAllowedAsync(Requester);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldAllowIfUserOwnsBlog()
        {
            this.blogOwnership.Setup(_ => _.IsOwnerAsync(UserId, BlogId)).ReturnsAsync(true);

            var result = await this.target.IsWriteAllowedAsync(UserId, BlogId);

            Assert.IsTrue(result);

            await this.target.AssertWriteAllowedAsync(UserId, BlogId);
        }

        [TestMethod]
        public async Task WhenAuthorizingUpdate_ItShouldForbidIfUserDoesNotOwnBlog()
        {
            this.blogOwnership.Setup(_ => _.IsOwnerAsync(UserId, BlogId)).ReturnsAsync(false);

            var result = await this.target.IsWriteAllowedAsync(UserId, BlogId);

            Assert.IsFalse(result);

            Func<Task> badMethodCall = () => this.target.AssertWriteAllowedAsync(UserId, BlogId);

            await badMethodCall.AssertExceptionAsync<UnauthorizedException>();
        }
    }
}