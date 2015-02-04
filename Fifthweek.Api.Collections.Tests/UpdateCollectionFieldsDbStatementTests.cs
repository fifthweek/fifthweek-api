namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateCollectionFieldsDbStatementTests : PersistenceTestsBase
    {
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidCollectionName Name = ValidCollectionName.Parse("Bat puns");
        private static readonly Collection Collection = new Collection(CollectionId.Value)
        {
            ChannelId = ChannelId.Value,
            Name = Name.Value
        };

        private Mock<IFifthweekDbContext> databaseContext;
        private UpdateCollectionFieldsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new UpdateCollectionFieldsDbStatement(databaseContext);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase.NewContext());
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(Collection);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(Collection);

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

                await this.target.ExecuteAsync(Collection);

                var expectedCollection = Collection.Copy();

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
                    }
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.NewContext())
            {
                var random = new Random();
                var creator = UserTests.UniqueEntity(random);

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

                databaseContext.Channels.Add(newChannel);
                databaseContext.Collections.Add(collection);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}