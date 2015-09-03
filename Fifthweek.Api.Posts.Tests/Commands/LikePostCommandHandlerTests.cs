namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class LikePostCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = PostId.Random();
        private static readonly DateTime Timestamp = DateTime.UtcNow;

        private LikePostCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<ILikePostDbStatement> likePost;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.likePost = new Mock<ILikePostDbStatement>(MockBehavior.Strict);

            this.target = new LikePostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.likePost.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldCheckCommandIsNotNull()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserIsAuthenticated()
        {
            await this.target.HandleAsync(new LikePostCommand(Requester.Unauthenticated, PostId, Timestamp));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserCanCommentOnThePost()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new LikePostCommand(Requester, PostId, Timestamp));
        }

        [TestMethod]
        public async Task ItShouldCommentOnPostDatabase()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.likePost.Setup(v => v.ExecuteAsync(UserId, PostId, Timestamp))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new LikePostCommand(Requester, PostId, Timestamp));

            this.likePost.Verify();
        }
    }
}