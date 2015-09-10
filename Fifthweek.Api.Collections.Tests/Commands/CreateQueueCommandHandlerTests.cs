namespace Fifthweek.Api.Collections.Tests.Commands
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;
    using Fifthweek.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class CreateQueueCommandHandlerTests : PersistenceTestsBase
    {
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidQueueName Name = ValidQueueName.Parse("Bat puns");
        private static readonly HourOfWeek InitialWeeklyReleaseTime = HourOfWeek.Parse(27);
        private static readonly CreateQueueCommand Command = new CreateQueueCommand(Requester, QueueId, ChannelId, Name, InitialWeeklyReleaseTime);
        private static readonly DateTime DefaultQueueLowerBound = new DateTime(2015, 5, 31, 23, 59, 0, DateTimeKind.Utc);

        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private Mock<IRandom> random;
        private CreateQueueCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.channelSecurity = new Mock<IChannelSecurity>();
            this.random = new Mock<IRandom>();

            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new CreateQueueCommandHandler(this.requesterSecurity.Object, this.channelSecurity.Object, connectionFactory);
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
            await this.target.HandleAsync(new CreateQueueCommand(Requester.Unauthenticated, QueueId, ChannelId, Name, InitialWeeklyReleaseTime));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToChannel()
        {
            this.channelSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, ChannelId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldCreateCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedCollection = new Queue(
                    QueueId.Value,
                    ChannelId.Value,
                    null,
                    Name.Value,
                    new SqlDateTime(DefaultQueueLowerBound).Value, 
                    default(DateTime));

                return new ExpectedSideEffects
                {
                    Insert = new WildcardEntity<Queue>(expectedCollection)
                    {
                        Expected = actual =>
                        {
                            expectedCollection.CreationDate = actual.CreationDate;
                            return expectedCollection;
                        }
                    },
                    ExcludedFromTest = entity => entity is WeeklyReleaseTime
                };
            });
        }

        [TestMethod]
        public async Task ItShouldCreateWeeklyReleaseTime()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedWeeklyReleaseTime = new WeeklyReleaseTime(
                    QueueId.Value,
                    null,
                    (byte)InitialWeeklyReleaseTime.Value);

                return new ExpectedSideEffects
                {
                    Insert = expectedWeeklyReleaseTime,
                    ExcludedFromTest = entity => entity is Queue
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelAsync(UserId.Value, ChannelId.Value);
            }
        }
    } 
}