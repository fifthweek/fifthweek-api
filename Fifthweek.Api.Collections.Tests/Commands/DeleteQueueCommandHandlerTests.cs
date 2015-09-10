namespace Fifthweek.Api.Collections.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.FileManagement.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteQueueCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly DeleteQueueCommand Command = new DeleteQueueCommand(Requester, QueueId);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IQueueSecurity> collectionSecurity;
        private Mock<IDeleteQueueDbStatement> deleteCollection;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private DeleteQueueCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.collectionSecurity = new Mock<IQueueSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.deleteCollection = new Mock<IDeleteQueueDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeleteQueueCommandHandler(
                this.requesterSecurity.Object, 
                this.collectionSecurity.Object, 
                this.deleteCollection.Object, 
                this.scheduleGarbageCollection.Object);
        }


        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCommand()
        {
            await this.target.HandleAsync(null);
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserIsAuthenticated()
        {
            await this.target.HandleAsync(new DeleteQueueCommand(Requester.Unauthenticated, QueueId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToCollection()
        {
            this.collectionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, QueueId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldDeleteCollection()
        {
            this.deleteCollection.Setup(_ => _.ExecuteAsync(QueueId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.deleteCollection.Verify();
        }

        [TestMethod]
        public async Task ItShouldIssueGarbageCollection()
        {
            this.deleteCollection.Setup(_ => _.ExecuteAsync(QueueId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.scheduleGarbageCollection.Verify();
        }
    } 
}