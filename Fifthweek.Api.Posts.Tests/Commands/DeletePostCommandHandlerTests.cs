﻿namespace Fifthweek.Api.Posts.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Posts.Commands;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeletePostCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly DateTime Now = DateTime.UtcNow;

        private DeletePostCommandHandler target;

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<IDeletePostDbStatement> deletePost;
        private Mock<IDefragmentQueueIfRequiredDbStatement> defragmentQueueIfRequired;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private Mock<ITimestampCreator> timestampCreator;

        [TestInitialize]
        public void TestInitialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            this.timestampCreator = new Mock<ITimestampCreator>();
            this.timestampCreator.Setup(v => v.Now()).Returns(Now);

            this.deletePost = new Mock<IDeletePostDbStatement>(MockBehavior.Strict);
            this.defragmentQueueIfRequired = new Mock<IDefragmentQueueIfRequiredDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeletePostCommandHandler(
                this.requesterSecurity.Object,
                this.postSecurity.Object,
                this.deletePost.Object,
                this.defragmentQueueIfRequired.Object,
                this.scheduleGarbageCollection.Object,
                this.timestampCreator.Object);
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
            this.defragmentQueueIfRequired.SetupFor(PostId);
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.deletePost.Verify();
        }

        [TestMethod]
        public async Task ItShouldScheduleGarbageCollection()
        {
            this.postSecurity.Setup(v => v.AssertWriteAllowedAsync(UserId, PostId)).Returns(Task.FromResult(0));
            this.defragmentQueueIfRequired.SetupFor(PostId);
            this.deletePost.Setup(v => v.ExecuteAsync(PostId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(v => v.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(new DeletePostCommand(PostId, Requester));

            this.scheduleGarbageCollection.Verify();
        }
    }
}