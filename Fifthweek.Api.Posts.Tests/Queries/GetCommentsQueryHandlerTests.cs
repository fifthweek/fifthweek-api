namespace Fifthweek.Api.Posts.Tests.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Queries;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class GetCommentsQueryHandlerTests
    {
        private static readonly UserId UserId = UserId.Random();
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = PostId.Random();
        private static readonly DateTime Timestamp = DateTime.UtcNow;

        private GetCommentsQueryHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IGetCommentsDbStatement> getComments;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.getComments = new Mock<IGetCommentsDbStatement>(MockBehavior.Strict);

            this.target = new GetCommentsQueryHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.getComments.Object);
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
            await this.target.HandleAsync(new GetCommentsQuery(Requester.Unauthenticated, PostId, Timestamp));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserCanCommentOnThePost()
        {
            this.postSecurity.Setup(v => v.AssertReadAllowedAsync(UserId, PostId, Timestamp))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new GetCommentsQuery(Requester, PostId, Timestamp));
        }

        [TestMethod]
        public async Task ItShouldCommentOnPostDatabase()
        {
            this.postSecurity.Setup(v => v.AssertReadAllowedAsync(UserId, PostId, Timestamp)).Returns(Task.FromResult(0));

            var expectedResult = new CommentsResult(
                new List<CommentsResult.Item>
                {
                    new CommentsResult.Item(CommentId.Random(), PostId.Random(), UserId.Random(), new Username("blah"), new Comment("comment"), DateTime.UtcNow),
                });
            
            this.getComments.Setup(v => v.ExecuteAsync(PostId))
                .Returns(Task.FromResult(expectedResult))
                .Verifiable();

            var result = await this.target.HandleAsync(new GetCommentsQuery(Requester, PostId, Timestamp));

            Assert.AreEqual(expectedResult, result);
            this.getComments.Verify();
        }
    }
}