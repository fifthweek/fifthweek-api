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
        private static readonly DateTime Timestamp = DateTime.UtcNow;
        private Mock<IIsPostOwnerDbStatement> isPostOwner;
        private Mock<IIsPostSubscriberDbStatement> isPostSubscriber;
        private PostSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.isPostOwner = new Mock<IIsPostOwnerDbStatement>();
            this.isPostSubscriber = new Mock<IIsPostSubscriberDbStatement>();
            this.target = new PostSecurity(this.isPostOwner.Object, this.isPostSubscriber.Object);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostWriting_ItShouldAllowIfUserOwnsPost()
        {
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(true);

            var result = await this.target.IsWriteAllowedAsync(UserId, PostId);

            Assert.IsTrue(result);

            await this.target.AssertWriteAllowedAsync(UserId, PostId);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostWriting_ItShouldForbidIfUserDoesNotOwnPost()
        {
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(false);

            var result = await this.target.IsWriteAllowedAsync(UserId, PostId);

            Assert.IsFalse(result);

            await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
            {
                return this.target.AssertWriteAllowedAsync(UserId, PostId);
            });
        }

        [TestMethod]
        public async Task WhenAuthorizingPostCommentingOrLiking_ItShouldAllowIfUserIsSubscriber()
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(true);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(false);

            var result = await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);

            Assert.IsTrue(result);

            await this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostCommentingOrLiking_ItShouldForbidIfUserIsNotSubscriberOrOwner()
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(false);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(false);

            var result = await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);

            Assert.IsFalse(result);

            await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
            {
                return this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
            });
        }

        [TestMethod]
        public async Task WhenAuthorizingPostCommentingOrLiking_ItShouldAllowIfUserIsOwner()
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(false);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(true);

            var result = await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);

            Assert.IsTrue(result);

            await this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostCommentingOrLiking_ItShouldAllowIfUserIsOwnerAndSubscriber()
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(true);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(true);

            var result = await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);

            Assert.IsTrue(result);

            await this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
        }
    }
}