namespace Fifthweek.Api.Collections.Tests
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteCollectionDbStatementTests : PersistenceTestsBase
    {
        private static readonly CollectionId CollectionId = new CollectionId(Guid.NewGuid());
        
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private DeleteCollectionDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteCollectionDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireCollectionId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateCollectionAsync(testDatabase);
                await this.target.ExecuteAsync(CollectionId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldDeleteCollection()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var expectedDeletion = await this.CreateCollectionAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(CollectionId);

                return new ExpectedSideEffects
                {
                    Delete = expectedDeletion,
                    ExcludedFromTest = entity =>
                        entity is WeeklyReleaseTime ||
                        entity is Post 
                };
            });
        }

        private async Task<Collection> CreateCollectionAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreatePopulatedTestCollectionAsync(Guid.NewGuid(), Guid.NewGuid(), CollectionId.Value);
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                var collectionId = CollectionId.Value;
                return await databaseContext.Collections.FirstAsync(_ => _.Id == collectionId);
            }
        }
    } 
}