namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
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
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { HourOfWeek.Parse(42) });

        private DeletePostCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<ITryGetQueuedPostCollectionDbStatement> tryGetPostQueuedCollection;
        private Mock<IGetWeeklyReleaseScheduleDbStatement> getWeeklyReleaseSchedule;
        private Mock<IDeletePostDbStatement> deletePost;
        private Mock<IDefragmentQueueDbStatement> defragmentQueue;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();
            this.tryGetPostQueuedCollection = new Mock<ITryGetQueuedPostCollectionDbStatement>();
            this.getWeeklyReleaseSchedule = new Mock<IGetWeeklyReleaseScheduleDbStatement>();

            this.deletePost = new Mock<IDeletePostDbStatement>(MockBehavior.Strict);
            this.defragmentQueue = new Mock<IDefragmentQueueDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeletePostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.tryGetPostQueuedCollection.Object,
                this.getWeeklyReleaseSchedule.Object,
                this.deletePost.Object,
                this.defragmentQueue.Object,
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
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0));
            
            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.deletePost.Verify();
        }

        [TestMethod]
        public async Task ItShouldDefragmentQueueIfPostWasQueued()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0));
            this.tryGetPostQueuedCollection.Setup(_ => _.ExecuteAsync(PostId, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc))).ReturnsAsync(CollectionId);
            this.getWeeklyReleaseSchedule.Setup(_ => _.ExecuteAsync(CollectionId)).ReturnsAsync(WeeklyReleaseSchedule);

            var callOrder = 0;
            this.deletePost.Setup(v => v.ExecuteAsync(PostId))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(0, callOrder++))
                .Verifiable();

            this.defragmentQueue.Setup(_ => _.ExecuteAsync(CollectionId, WeeklyReleaseSchedule, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(1, callOrder++))
                .Verifiable();

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.deletePost.Verify();
            this.defragmentQueue.Verify();
        }

        [TestMethod]
        public async Task ItShouldScheduleGarbageCollection()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.scheduleGarbageCollection.Verify();
        }
    }
}