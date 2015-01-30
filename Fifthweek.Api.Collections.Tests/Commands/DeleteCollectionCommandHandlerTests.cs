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
    public class DeleteCollectionCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly DeleteCollectionCommand Command = new DeleteCollectionCommand(Requester, CollectionId);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<IDeleteCollectionDbStatement> deleteCollection;
        private Mock<IScheduleGarbageCollectionStatement> scheduleGarbageCollection;
        private DeleteCollectionCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.collectionSecurity = new Mock<ICollectionSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.deleteCollection = new Mock<IDeleteCollectionDbStatement>(MockBehavior.Strict);
            this.scheduleGarbageCollection = new Mock<IScheduleGarbageCollectionStatement>(MockBehavior.Strict);

            this.target = new DeleteCollectionCommandHandler(
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
            await this.target.HandleAsync(new DeleteCollectionCommand(Requester.Unauthenticated, CollectionId));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToCollection()
        {
            this.collectionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldDeleteCollection()
        {
            this.deleteCollection.Setup(_ => _.ExecuteAsync(CollectionId)).Returns(Task.FromResult(0)).Verifiable();
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.deleteCollection.Verify();
        }

        [TestMethod]
        public async Task ItShouldIssueGarbageCollection()
        {
            this.deleteCollection.Setup(_ => _.ExecuteAsync(CollectionId)).Returns(Task.FromResult(0));
            this.scheduleGarbageCollection.Setup(_ => _.ExecuteAsync()).Returns(Task.FromResult(0)).Verifiable();

            await this.target.HandleAsync(Command);

            this.scheduleGarbageCollection.Verify();
        }
    } 
}