namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CommentOnPostCommandHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = PostId.Random();
        private static readonly CommentId CommentId = CommentId.Random();
        private static readonly ValidComment Content = ValidComment.Parse("comment");
        private static readonly DateTime Timestamp = DateTime.UtcNow;

        private CommentOnPostCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<ICommentOnPostDbStatement> commentOnPost;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.commentOnPost = new Mock<ICommentOnPostDbStatement>(MockBehavior.Strict);

            this.target = new CommentOnPostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.commentOnPost.Object);
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
            await this.target.HandleAsync(new CommentOnPostCommand(Requester.Unauthenticated, PostId, CommentId, Content, Timestamp));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserCanCommentOnThePost()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new CommentOnPostCommand(Requester, PostId, CommentId, Content, Timestamp));
        }

        [TestMethod]
        public async Task ItShouldCommentOnPostDatabase()
        {
            this.postSecurity.Setup(v => v.AssertCommentOrLikeAllowedAsync(UserId, PostId, Timestamp)).Returns(Task.FromResult(0));
            this.commentOnPost.Setup(v => v.ExecuteAsync(UserId, PostId, CommentId, Content, Timestamp))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(new CommentOnPostCommand(Requester, PostId, CommentId, Content, Timestamp));

            this.commentOnPost.Verify();
        }
    }
}
