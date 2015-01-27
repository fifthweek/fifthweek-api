namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    

    [TestClass]
    public class DeletePostCommandHandlerTests
    {
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);

        private DeletePostCommandHandler target;

        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IDeletePostDbStatement> deletePost;
        private Mock<IRequesterSecurity> requesterSecurity;

        [TestInitialize]
        public void TestInitialize()
        {
            this.postSecurity = new Mock<IPostSecurity>();
            this.deletePost = new Mock<IDeletePostDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);

            this.target = new DeletePostCommandHandler(
                this.scheduleGarbageCollection.Object,
                this.postSecurity.Object,
                this.requesterSecurity.Object,
                this.deletePost.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task WhenDeletingPost_ShouldCheckCommandIsNotNull()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenDeletingPost_ShouldCheckTheUserIsAuthenticated()
        {
            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester.Unauthenticated));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenDeletingPost_ShouldCheckTheUserCanDeleteTheFile()
        {
            this.postSecurity.Setup(v => v.AssertDeletionAllowedAsync(UserId, PostId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));
        }

        [TestMethod]
        public async Task WhenDeletingPost_ShouldDeleteFromTheDatabaseAndScheduleGarbageCollection()
        {
            this.postSecurity.Setup(v => v.AssertDeletionAllowedAsync(UserId, PostId))
                .Returns(Task.FromResult(0));

            this.deletePost.Setup(v => v.ExecuteAsync(PostId))
                .Returns(Task.FromResult(0))
                .Verifiable();

            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync())
                .Returns(Task.FromResult(0))
                .Verifiable();
            
            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.deletePost.Verify();
            this.scheduleGarbageCollection.Verify();
        }
    }
}