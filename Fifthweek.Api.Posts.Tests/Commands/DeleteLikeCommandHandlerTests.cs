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
    public class DeleteLikeCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = PostId.Random();

        private DeleteLikeCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IUnlikePostDbStatement> unlikePost;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.unlikePost = new Mock<IUnlikePostDbStatement>(MockBehavior.Strict);

            this.target = new DeleteLikeCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.unlikePost.Object);
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
            await this.target.HandleAsync(new DeleteLikeCommand(Requester.Unauthenticated, PostId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserCanCommentOnThePost()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new DeleteLikeCommand(Requester, PostId));
        }

        [TestMethod]
        public async Task ItShouldCommentOnPostDatabase()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.unlikePost.Setup(v => v.ExecuteAsync(UserId, PostId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new DeleteLikeCommand(Requester, PostId));

            this.unlikePost.Verify();
        }
    }
}