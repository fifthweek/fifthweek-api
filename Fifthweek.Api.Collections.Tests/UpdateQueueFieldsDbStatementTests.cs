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
    public class UpdateQueueFieldsDbStatementTests : PersistenceTestsBase
    {
        private static readonly QueueId QueueId = new QueueId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidQueueName Name = ValidQueueName.Parse("Bat puns");
        private static readonly Queue Queue = new Queue(QueueId.Value)
        {
            Name = Name.Value
        };

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UpdateQueueFieldsDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new UpdateQueueFieldsDbStatement(connectionFactory);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateEntitiesAsync(testDatabase);
                await this.target.ExecuteAsync(Queue);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(Queue);

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

                await this.target.ExecuteAsync(Queue);

                var expectedCollection = Queue.Copy();

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Queue>(expectedCollection)
                    {
                        Expected = actual =>
                        {
                            expectedCollection.CreationDate = actual.CreationDate;
                            expectedCollection.BlogId = actual.BlogId;
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

                var blog = BlogTests.UniqueEntity(random);
                blog.Creator = creator;
                blog.CreatorId = creator.Id;

                var orignalChannel = ChannelTests.UniqueEntity(random);
                orignalChannel.Id = ChannelId.Value;
                orignalChannel.Blog = blog;
                orignalChannel.BlogId = blog.Id;

                var collection = QueueTests.UniqueEntity(random);
                collection.Id = QueueId.Value;
                collection.Blog = blog;
                collection.BlogId = blog.Id;

                databaseContext.Queues.Add(collection);
                await databaseContext.SaveChangesAsync();
            }
        }
    }
}