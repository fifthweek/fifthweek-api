namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class ReplaceWeeklyReleaseTimesDbStatementTests : PersistenceTestsBase
    {
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly HourOfWeek ExistingReleaseA = HourOfWeek.Parse(0);
        private static readonly HourOfWeek ExistingReleaseB = HourOfWeek.Parse(2);
        private static readonly HourOfWeek ExistingReleaseC = HourOfWeek.Parse(45);
        private static readonly HourOfWeek NewReleaseA = HourOfWeek.Parse(20);
        private static readonly HourOfWeek NewReleaseB = HourOfWeek.Parse(27);
        private static readonly HourOfWeek NewReleaseC = HourOfWeek.Parse(92);
        private static readonly WeeklyReleaseSchedule WeeklyReleaseSchedule = WeeklyReleaseSchedule.Parse(new[] { NewReleaseA, NewReleaseB, NewReleaseC });

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private ReplaceWeeklyReleaseTimesDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new ReplaceWeeklyReleaseTimesDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null, WeeklyReleaseSchedule);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireWeeklyReleaseSchedule()
        {
            await this.target.ExecuteAsync(CollectionId, null);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(CollectionId, WeeklyReleaseSchedule);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, WeeklyReleaseSchedule);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task WhenReleaseTimesAreAllExisting_ItShouldHaveNoEffect()
        {
            await this.AssertSuccessAsync(
                WeeklyReleaseSchedule.Parse(new[] { ExistingReleaseA, ExistingReleaseB, ExistingReleaseC }),
                Enumerable.Empty<HourOfWeek>(),
                Enumerable.Empty<HourOfWeek>());
        }

        [TestMethod]
        public async Task WhenReleaseTimesAreAllNew_ItShouldDeleteExistingTimesAndInsertNewTimes()
        {
            await this.AssertSuccessAsync(
                WeeklyReleaseSchedule.Parse(new[] { NewReleaseA, NewReleaseB, NewReleaseC }),
                new[] { NewReleaseA, NewReleaseB, NewReleaseC },
                new[] { ExistingReleaseA, ExistingReleaseB, ExistingReleaseC });
        }

        [TestMethod]
        public async Task WhenReleaseTimesContainAllExistingAndSomeNew_ItShouldKeepExistingTimesAndInsertNewTimes()
        {
            await this.AssertSuccessAsync(
                WeeklyReleaseSchedule.Parse(new[] { ExistingReleaseA, ExistingReleaseB, ExistingReleaseC, NewReleaseA, NewReleaseB, NewReleaseC }),
                new[] { NewReleaseA, NewReleaseB, NewReleaseC },
                Enumerable.Empty<HourOfWeek>());
        }

        [TestMethod]
        public async Task WhenReleaseTimesContainSomeExistingAndSomeNew_ItShouldKeepExistingTimesAndInsertNewTimes()
        {
            await this.AssertSuccessAsync(
                WeeklyReleaseSchedule.Parse(new[] { ExistingReleaseA, NewReleaseA, NewReleaseB, NewReleaseC }),
                new[] { NewReleaseA, NewReleaseB, NewReleaseC },
                new[] { ExistingReleaseB, ExistingReleaseC });
        }

        private async Task AssertSuccessAsync(WeeklyReleaseSchedule schedule, IEnumerable<HourOfWeek> expectedInserts, IEnumerable<HourOfWeek> expectedDeletes)
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId, schedule);

                return new ExpectedSideEffects
                {
                    Inserts = expectedInserts.Select(_ => new WeeklyReleaseTime(CollectionId.Value, null, (byte)_.Value)).ToArray(),
                    Deletes = expectedDeletes.Select(_ => new WeeklyReleaseTime(CollectionId.Value, null, (byte)_.Value)).ToArray()
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var creator = UserTests.UniqueEntity(random);

                var subscription = SubscriptionTests.UniqueEntity(random);
                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;

                var channel = ChannelTests.UniqueEntity(random);
                channel.Id = ChannelId.Value;
                channel.Subscription = subscription;
                channel.SubscriptionId = subscription.Id;

                var collection = CollectionTests.UniqueEntity(random);
                collection.Id = CollectionId.Value;
                collection.Channel = channel;
                collection.ChannelId = channel.Id;

                var weeklyReleaseTimes =
                    new[] { ExistingReleaseA, ExistingReleaseB, ExistingReleaseC }.Select(
                        _ => new WeeklyReleaseTime(CollectionId.Value, (byte)_.Value));

                databaseContext.Collections.Add(collection);
                await databaseContext.SaveChangesAsync();

                await databaseContext.Database.Connection.InsertAsync(weeklyReleaseTimes);
            }
        }
    }
}