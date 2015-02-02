namespace Fifthweek.Api.Posts.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class PostSecurityTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private Mock<IPostOwnership> postOwnership;
        private PostSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.postOwnership = new Mock<IPostOwnership>();
            this.target = new PostSecurity(this.postOwnership.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostDeletion_ItShouldAllowIfUserOwnsPost()
        {
            this.postOwnership.Setup(_ => _.IsOwnerAsync(UserId, PostId)).ReturnsAsync(true);

            var result = await this.target.IsWriteAllowedAsync(UserId, PostId);

            Assert.IsTrue(result);

            await this.target.AssertWriteAllowedAsync(UserId, PostId);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostDeletion_ItShouldForbidIfUserDoesNotOwnPost()
        {
            this.postOwnership.Setup(_ => _.IsOwnerAsync(UserId, PostId)).ReturnsAsync(false);

            var result = await this.target.IsWriteAllowedAsync(UserId, PostId);

            Assert.IsFalse(result);

            await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
            {
                 return this.target.AssertWriteAllowedAsync(UserId, PostId);
            });
        }
    }
}