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
            Name = Name.Value
        };

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UpdateCollectionFieldsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new UpdateCollectionFieldsDbStatement(connectionFactory);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
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
                this.InitializeTarget(testDatabase);
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
                            expectedCollection.ChannelId = actual.ChannelId;
                            return expectedCollection;
                        }
                    }
                };
            });
        }

        private async Task CreateEntitiesAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var random = new Random();
                var creator = UserTests.UniqueEntity(random);

                var subscription = BlogTests.UniqueEntity(random);
                subscription.Creator = creator;
                subscription.CreatorId = creator.Id;

                var orignalChannel = ChannelTests.UniqueEntity(random);
                orignalChannel.Id = ChannelId.Value;
                orignalChannel.Blog = subscription;
                orignalChannel.BlogId = subscription.Id;

                var collection = CollectionTests.UniqueEntity(random);
                collection.Id = CollectionId.Value;
                collection.Channel = orignalChannel;
                collection.ChannelId = orignalChannel.Id;

                databaseContext.Collections.Add(collection);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}