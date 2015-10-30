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
        private Mock<IIsPostFreeAccessUserDbStatement> isPostFreeAccessUser;
        private PostSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.isPostOwner = new Mock<IIsPostOwnerDbStatement>(MockBehavior.Strict);
            this.isPostSubscriber = new Mock<IIsPostSubscriberDbStatement>(MockBehavior.Strict);
            this.isPostFreeAccessUser = new Mock<IIsPostFreeAccessUserDbStatement>(MockBehavior.Strict);
            this.target = new PostSecurity(this.isPostOwner.Object, this.isPostSubscriber.Object, this.isPostFreeAccessUser.Object);
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
        public async Task WhenAuthorizingPostCommentingOrLiking_ItShouldAllowIfUserIsSubscriberOwnerOrFreeAccessUser()
        {
            await this.PerformCommentOrLikeTest(false, false, false, false);
            await this.PerformCommentOrLikeTest(false, false, true, true);
            await this.PerformCommentOrLikeTest(false, true, false, true);
            await this.PerformCommentOrLikeTest(false, true, true, true);
            await this.PerformCommentOrLikeTest(true, false, false, true);
            await this.PerformCommentOrLikeTest(true, false, true, true);
            await this.PerformCommentOrLikeTest(true, true, false, true);
            await this.PerformCommentOrLikeTest(true, true, true, true);
        }

        [TestMethod]
        public async Task WhenAuthorizingReading_ItShouldAllowIfUserIsSubscriberOwnerOrFreeAccessUser()
        {
            await this.PerformReadTest(false, false, false, false);
            await this.PerformReadTest(false, false, true, true);
            await this.PerformReadTest(false, true, false, true);
            await this.PerformReadTest(false, true, true, true);
            await this.PerformReadTest(true, false, false, true);
            await this.PerformReadTest(true, false, true, true);
            await this.PerformReadTest(true, true, false, true);
            await this.PerformReadTest(true, true, true, true);
        }

        private async Task PerformCommentOrLikeTest(
            bool isPostSubscriberValue,
            bool isPostOwnerValue,
            bool isFreeAccessUserValue,
            bool expectedResult)
        {
            this.SetupReadingTest(isPostSubscriberValue, isPostOwnerValue, isFreeAccessUserValue);
            if (expectedResult)
            {
                Assert.IsTrue(await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp));
                await this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
            }
            else
            {
                Assert.IsFalse(await this.target.IsCommentOrLikeAllowedAsync(UserId, PostId, Timestamp));
                await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
                {
                    return this.target.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp);
                });
            }
        }

        private async Task PerformReadTest(
            bool isPostSubscriberValue,
            bool isPostOwnerValue,
            bool isFreeAccessUserValue,
            bool expectedResult)
        {
            this.SetupReadingTest(isPostSubscriberValue, isPostOwnerValue, isFreeAccessUserValue);
            if (expectedResult)
            {
                Assert.IsTrue(await this.target.IsReadAllowedAsync(UserId, PostId, Timestamp));
                await this.target.AssertReadAllowedAsync(UserId, PostId, Timestamp);
            }
            else
            {
                Assert.IsFalse(await this.target.IsReadAllowedAsync(UserId, PostId, Timestamp));
                await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
                {
                    return this.target.AssertReadAllowedAsync(UserId, PostId, Timestamp);
                });
            }
        }

        private void SetupReadingTest(bool isPostSubscriberValue, bool isPostOwnerValue, bool isFreeAccessUserValue)
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(isPostSubscriberValue);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(isPostOwnerValue);
            this.isPostFreeAccessUser.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(isFreeAccessUserValue);
        }
    }
}