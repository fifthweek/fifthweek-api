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
        private Mock<IIsFreePostDbStatement> isFreePost;
        private PostSecurity target;

        [TestInitialize]
        public void Initialize()
        {
            this.isPostOwner = new Mock<IIsPostOwnerDbStatement>(MockBehavior.Strict);
            this.isPostSubscriber = new Mock<IIsPostSubscriberDbStatement>(MockBehavior.Strict);
            this.isPostFreeAccessUser = new Mock<IIsPostFreeAccessUserDbStatement>(MockBehavior.Strict);
            this.isFreePost = new Mock<IIsFreePostDbStatement>(MockBehavior.Strict);
            this.target = new PostSecurity(this.isPostOwner.Object, this.isPostSubscriber.Object, this.isPostFreeAccessUser.Object, this.isFreePost.Object);
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
        public async Task WhenAuthorizingPostCommenting_ItShouldAllowIfUserIsSubscriberOwnerOrFreeAccessUser()
        {
            await this.PerformCommentTest(false, false, false, false, false);
            await this.PerformCommentTest(false, false, true, false, true);
            await this.PerformCommentTest(false, true, false, false, true);
            await this.PerformCommentTest(false, true, true, false, true);
            await this.PerformCommentTest(true, false, false, false, true);
            await this.PerformCommentTest(true, false, true, false, true);
            await this.PerformCommentTest(true, true, false, false, true);
            await this.PerformCommentTest(true, true, true, false, true);
            await this.PerformCommentTest(false, false, false, true, false);
            await this.PerformCommentTest(false, false, true, true, true);
            await this.PerformCommentTest(false, true, false, true, true);
            await this.PerformCommentTest(false, true, true, true, true);
            await this.PerformCommentTest(true, false, false, true, true);
            await this.PerformCommentTest(true, false, true, true, true);
            await this.PerformCommentTest(true, true, false, true, true);
            await this.PerformCommentTest(true, true, true, true, true);
        }

        [TestMethod]
        public async Task WhenAuthorizingPostLiking_ItShouldAllowIfUserIsSubscriberOwnerOrFreeAccessUser()
        {
            await this.PerformLikeTest(false, false, false, false, false);
            await this.PerformLikeTest(false, false, true, false, true);
            await this.PerformLikeTest(false, true, false, false, true);
            await this.PerformLikeTest(false, true, true, false, true);
            await this.PerformLikeTest(true, false, false, false, true);
            await this.PerformLikeTest(true, false, true, false, true);
            await this.PerformLikeTest(true, true, false, false, true);
            await this.PerformLikeTest(true, true, true, false, true);
            await this.PerformLikeTest(false, false, false, true, true);
            await this.PerformLikeTest(false, false, true, true, true);
            await this.PerformLikeTest(false, true, false, true, true);
            await this.PerformLikeTest(false, true, true, true, true);
            await this.PerformLikeTest(true, false, false, true, true);
            await this.PerformLikeTest(true, false, true, true, true);
            await this.PerformLikeTest(true, true, false, true, true);
            await this.PerformLikeTest(true, true, true, true, true);
        }

        [TestMethod]
        public async Task WhenAuthorizingReading_ItShouldAllowIfUserIsSubscriberOwnerOrFreeAccessUser()
        {
            await this.PerformReadTest(false, false, false, false, PostSecurityResult.Denied);
            await this.PerformReadTest(false, false, true, false, PostSecurityResult.GuestList);
            await this.PerformReadTest(false, true, false, false, PostSecurityResult.Owner);
            await this.PerformReadTest(false, true, true, false, PostSecurityResult.Owner);
            await this.PerformReadTest(true, false, false, false, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, false, true, false, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, true, false, false, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, true, true, false, PostSecurityResult.Subscriber);
            await this.PerformReadTest(false, false, false, true, PostSecurityResult.FreePost);
            await this.PerformReadTest(false, false, true, true, PostSecurityResult.GuestList);
            await this.PerformReadTest(false, true, false, true, PostSecurityResult.Owner);
            await this.PerformReadTest(false, true, true, true, PostSecurityResult.Owner);
            await this.PerformReadTest(true, false, false, true, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, false, true, true, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, true, false, true, PostSecurityResult.Subscriber);
            await this.PerformReadTest(true, true, true, true, PostSecurityResult.Subscriber);
        }

        private async Task PerformCommentTest(
            bool isPostSubscriberValue,
            bool isPostOwnerValue,
            bool isFreeAccessUserValue,
            bool isFreePostValue,
            bool expectedResult)
        {
            this.SetupReadingTest(isPostSubscriberValue, isPostOwnerValue, isFreeAccessUserValue, isFreePostValue);
            if (expectedResult)
            {
                Assert.IsTrue(await this.target.IsCommentAllowedAsync(UserId, PostId, Timestamp));
                await this.target.AssertCommentAllowedAsync(UserId, PostId, Timestamp);
            }
            else
            {
                Assert.IsFalse(await this.target.IsCommentAllowedAsync(UserId, PostId, Timestamp));
                await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
                {
                    return this.target.AssertCommentAllowedAsync(UserId, PostId, Timestamp);
                });
            }
        }

        private async Task PerformLikeTest(
            bool isPostSubscriberValue,
            bool isPostOwnerValue,
            bool isFreeAccessUserValue,
            bool isFreePostValue,
            bool expectedResult)
        {
            this.SetupReadingTest(isPostSubscriberValue, isPostOwnerValue, isFreeAccessUserValue, isFreePostValue);
            if (expectedResult)
            {
                Assert.IsTrue(await this.target.IsLikeAllowedAsync(UserId, PostId, Timestamp));
                await this.target.AssertLikeAllowedAsync(UserId, PostId, Timestamp);
            }
            else
            {
                Assert.IsFalse(await this.target.IsLikeAllowedAsync(UserId, PostId, Timestamp));
                await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
                {
                    return this.target.AssertLikeAllowedAsync(UserId, PostId, Timestamp);
                });
            }
        }

        private async Task PerformReadTest(
            bool isPostSubscriberValue,
            bool isPostOwnerValue,
            bool isFreeAccessUserValue,
            bool isFreePostValue,
            PostSecurityResult expectedResult)
        {
            this.SetupReadingTest(isPostSubscriberValue, isPostOwnerValue, isFreeAccessUserValue, isFreePostValue);
            Assert.AreEqual(expectedResult, await this.target.IsReadAllowedAsync(UserId, PostId, Timestamp));

            if (expectedResult != PostSecurityResult.Denied)
            {
                await this.target.AssertReadAllowedAsync(UserId, PostId, Timestamp);
            }
            else
            {
                await ExpectedException.AssertExceptionAsync<UnauthorizedException>(() =>
                {
                    return this.target.AssertReadAllowedAsync(UserId, PostId, Timestamp);
                });
            }
        }

        private void SetupReadingTest(bool isPostSubscriberValue, bool isPostOwnerValue, bool isFreeAccessUserValue, bool isFreePostValue)
        {
            this.isPostSubscriber.Setup(_ => _.ExecuteAsync(UserId, PostId, Timestamp)).ReturnsAsync(isPostSubscriberValue);
            this.isPostOwner.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(isPostOwnerValue);
            this.isPostFreeAccessUser.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(isFreeAccessUserValue);
            this.isFreePost.Setup(_ => _.ExecuteAsync(UserId, PostId)).ReturnsAsync(isFreePostValue);
        }
    }
}