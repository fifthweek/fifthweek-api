namespace Fifthweek.Api.Collections.Tests.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Commands;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateCollectionCommandHandlerTests : PersistenceTestsBase
    {
        private const byte ExistingWeeklyReleaseTimesMaxValue = 10;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidCollectionName Name = ValidCollectionName.Parse("Bat puns");
        private static readonly HourOfWeek ExistingReleaseA = HourOfWeek.Parse(0);
        private static readonly HourOfWeek ExistingReleaseB = HourOfWeek.Parse(2);
        private static readonly HourOfWeek NewReleaseA = HourOfWeek.Parse(20);
        private static readonly HourOfWeek NewReleaseB = HourOfWeek.Parse(27);
        private static readonly HourOfWeek NewReleaseC = HourOfWeek.Parse(92);
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { NewReleaseA, NewReleaseB, NewReleaseC });
        private static readonly UpdateCollectionCommand Command = new UpdateCollectionCommand(Requester, CollectionId, ChannelId, Name, WeeklyReleaseSchedule);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<ICollectionSecurity> collectionSecurity;
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IFifthweekDbContext> databaseContext;
        private UpdateCollectionCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.collectionSecurity = new Mock<ICollectionSecurity>();
            this.channelSecurity = new Mock<IChannelSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new UpdateCollectionCommandHandler(this.requesterSecurity.Object, this.collectionSecurity.Object, this.channelSecurity.Object, databaseContext);
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
            await this.target.HandleAsync(new UpdateCollectionCommand(Requester.Unauthenticated, CollectionId, ChannelId, Name, WeeklyReleaseSchedule));
        }

        [TestMethod]
        [ExpectedException(typeof(UnauthorizedException))]
        public async Task ItShouldRequireUserHasWriteAccessToCollection()
        {
            this.collectionSecurity.Setup(_ => _.AssertWriteAllowedAsync(UserId, CollectionId)).Throws<UnauthorizedException>();

            await this.target.HandleAsync(Command);
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
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedCollection = new Collection(CollectionId.Value)
                {
                    ChannelId = ChannelId.Value,
                    Name = Name.Value
                };

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Collection>(expectedCollection)
                    {
                        Expected = actual =>
                        {
                            expectedCollection.QueueExclusiveLowerBound = actual.QueueExclusiveLowerBound;
                            expectedCollection.CreationDate = actual.CreationDate;
                            return expectedCollection;
                        }
                    },
                    ExcludedFromTest = entity => entity is WeeklyReleaseTime
                };
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateWeeklyReleaseTimes()
        {
            await this.TestWeeklyReleaseTimeUpdate(WeeklyReleaseSchedule.Parse(new[] { NewReleaseA, NewReleaseB, NewReleaseC }));
            await this.TestWeeklyReleaseTimeUpdate(WeeklyReleaseSchedule.Parse(new[] { ExistingReleaseA, ExistingReleaseB, NewReleaseA, NewReleaseB, NewReleaseC }));
            await this.TestWeeklyReleaseTimeUpdate(WeeklyReleaseSchedule.Parse(new[] { ExistingReleaseA, ExistingReleaseB }));
        }

        private async Task TestWeeklyReleaseTimeUpdate(WeeklyReleaseSchedule schedule)
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(new UpdateCollectionCommand(Requester, CollectionId, ChannelId, Name, schedule));

                var expectedInserts = new List<WeeklyReleaseTime>();
                var expectedDeletes = new List<WeeklyReleaseTime>();

                var newReleaseTimes = schedule.Value.Select(_ => _.Value).ToList();
                foreach (var newReleaseTime in newReleaseTimes)
                {
                    // Only expect insert if release time does not already exist.
                    if (newReleaseTime > ExistingWeeklyReleaseTimesMaxValue)
                    {
                        expectedInserts.Add(new WeeklyReleaseTime(CollectionId.Value, null, newReleaseTime));
                    }
                }

                for (byte existingReleaseTime = 0;
                     existingReleaseTime <= ExistingWeeklyReleaseTimesMaxValue;
                     existingReleaseTime++)
                {
                    if (!newReleaseTimes.Contains(existingReleaseTime))
                    {
                        expectedDeletes.Add(new WeeklyReleaseTime(CollectionId.Value, null, existingReleaseTime));
                    }
                }

                return new ExpectedSideEffects
                {
                    Inserts = expectedInserts,
                    Deletes = expectedDeletes,
                    ExcludedFromTest = entity => entity is Collection
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var random = new Random();
                var creator = UserTests.UniqueEntity(random);
                creator.Id = UserId.Value;

                var subscription = SubscriptionTests.UniqueEntity(random);
                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;

                var orignalChannel = ChannelTests.UniqueEntity(random);
                orignalChannel.Id = Guid.NewGuid();
                orignalChannel.Subscription = subscription;
                orignalChannel.SubscriptionId = subscription.Id;

                var newChannel = ChannelTests.UniqueEntity(random);
                newChannel.Id = ChannelId.Value;
                newChannel.Subscription = subscription;
                newChannel.SubscriptionId = subscription.Id;

                var collection = CollectionTests.UniqueEntity(random);
                collection.Id = CollectionId.Value;
                collection.Channel = orignalChannel;
                collection.ChannelId = orignalChannel.Id;

                var weeklyReleaseTimes = WeeklyReleaseTimeTests.GenerateSortedWeeklyReleaseTimes(
                    CollectionId.Value,
                    ExistingWeeklyReleaseTimesMaxValue + 1);

                databaseContext.Channels.Add(newChannel);
                databaseContext.Collections.Add(collection);
                await databaseContext.SaveChangesAsync();

                await databaseContext.Database.Connection.InsertAsync(weeklyReleaseTimes);
            }
        }
    } 
}