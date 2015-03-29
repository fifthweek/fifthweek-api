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
    public class RescheduleForTimeCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly PostId PostId = new PostId(Guid.NewGuid());
        private static readonly DateTime ScheduledPostTime = DateTime.UtcNow.AddDays(100);
        private static readonly RescheduleForTimeCommand Command = new RescheduleForTimeCommand(Requester, PostId, ScheduledPostTime);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IPostSecurity> postSecurity;
        private Mock<ISetPostLiveDateDbStatement> setBacklogPostLiveDateToNow;
        private Mock<IRemoveFromQueueIfRequiredDbStatement> removeFromQueueIfRequired;
        private RescheduleForTimeCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.postSecurity = new Mock<IPostSecurity>();

            // Mock potentially side-effecting components with strict behaviour.            
            this.setBacklogPostLiveDateToNow = new Mock<ISetPostLiveDateDbStatement>(MockBehavior.Strict);
            this.removeFromQueueIfRequired = new Mock<IRemoveFromQueueIfRequiredDbStatement>(MockBehavior.Strict);

            this.target = new RescheduleForTimeCommandHandler(
                this.requesterSecurity.Object, 
                this.postSecurity.Object, 
                this.setBacklogPostLiveDateToNow.Object,
                this.removeFromQueueIfRequired.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenUnauthenticated_ItShouldThrowUnauthorizedException()
        {
            await this.target.HandleAsync(new RescheduleForTimeCommand(Requester.Unauthenticated, PostId, ScheduledPostTime));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task WhenNotAllowedToWriteToPost_ItShouldThrowUnauthorizedException()
        {
            this.postSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, PostId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldSetBacklogPostLiveDateToNow()
        {
            this.removeFromQueueIfRequired.SetupFor(PostId);
            this.setBacklogPostLiveDateToNow.Setup(_ => _.ExecuteAsync(PostId, ScheduledPostTime, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.setBacklogPostLiveDateToNow.Verify();
        }
    }
}