namespace Fifthweek.Api.Collections.Tests.Commands
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateQueueCommandHandlerTests
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidQueueName Name = ValidQueueName.Parse("Bat puns");
        private static readonly HourOfWeek NewReleaseA = HourOfWeek.Parse(20);
        private static readonly HourOfWeek NewReleaseB = HourOfWeek.Parse(27);
        private static readonly HourOfWeek NewReleaseC = HourOfWeek.Parse(92);
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { NewReleaseA, NewReleaseB, NewReleaseC });
        private static readonly UpdateQueueCommand Command = new UpdateQueueCommand(Requester, QueueId, Name, WeeklyReleaseSchedule);
        private static readonly Queue Queue = new Queue(QueueId.Value)
        {
            Name = Name.Value
        };

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IQueueSecurity> collectionSecurity;
        private Mock<IUpdateQueueFieldsDbStatement> updateCollectionFields;
        private Mock<IUpdateWeeklyReleaseScheduleDbStatement> updateWeeklyReleaseSchedule;
        private UpdateQueueCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.collectionSecurity = new Mock<IQueueSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.updateCollectionFields = new Mock<IUpdateQueueFieldsDbStatement>(MockBehavior.Strict);
            this.updateWeeklyReleaseSchedule = new Mock<IUpdateWeeklyReleaseScheduleDbStatement>(MockBehavior.Strict);

            this.target = new UpdateQueueCommandHandler(
                this.requesterSecurity.Object, 
                this.collectionSecurity.Object, 
                this.updateCollectionFields.Object,
                this.updateWeeklyReleaseSchedule.Object);
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
            await this.target.HandleAsync(new UpdateQueueCommand(Requester.Unauthenticated, QueueId, Name, WeeklyReleaseSchedule));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToCollection()
        {
            this.collectionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, QueueId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldUpdateCollectionFields()
        {
            this.updateCollectionFields.Setup(_ => _.ExecuteAsync(Queue)).Returns(Task.FromResult(0)).Verifiable();
            this.updateWeeklyReleaseSchedule.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0));

            await this.target.HandleAsync(Command);

            this.updateCollectionFields.Verify();
        }

        [TestMethod]
        public async Task ItShouldUpdateReleaseScheduledAfterUpdatingCollectionFields()
        {
            var callOrder = 0;
            this.updateCollectionFields.Setup(_ => _.ExecuteAsync(Queue))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(0, callOrder++));

            this.updateWeeklyReleaseSchedule.Setup(_ => _.ExecuteAsync(QueueId, WeeklyReleaseSchedule, It.Is<DateTime>(now => now.Kind == DateTimeKind.Utc)))
                .Returns(Task.FromResult(0))
                .Callback(() => Assert.AreEqual(1, callOrder++))
                .Verifiable();

            await this.target.HandleAsync(Command);

            this.updateWeeklyReleaseSchedule.Verify();
        }
    } 
}