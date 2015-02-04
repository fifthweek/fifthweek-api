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
    public class DeletePostCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());

        private DeletePostCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IDeletePostDbStatement> deletePost;
        private Mock<IRemoveFromQueueIfRequiredDbStatement> removeFromQueueIfRequired;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.deletePost = new Mock<IDeletePostDbStatement>(MockBehavior.Strict);
            this.removeFromQueueIfRequired = new Mock<IRemoveFromQueueIfRequiredDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeletePostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.deletePost.Object,
                this.removeFromQueueIfRequired.Object,
                this.scheduleGarbageCollection.Object);
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
            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester.Unauthenticated));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldCheckTheUserCanDeleteTheFile()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId))
                .Throws(new UnauthorizedException());

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));
        }

        [TestMethod]
        public async Task ItShouldDeleteFromTheDatabase()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.removeFromQueueIfRequired.SetupFor(PostId);
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.deletePost.Verify();
        }

        [TestMethod]
        public async Task ItShouldScheduleGarbageCollection()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.removeFromQueueIfRequired.SetupFor(PostId);
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.scheduleGarbageCollection.Verify();
        }
    }
}