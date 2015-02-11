namespace Fifthweek.Api.Channels.Tests
{
    using System;
    using System.Data.Entity;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class DeleteChannelDbStatementTests : PersistenceTestsBase
    {
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private DeleteChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new DeleteChannelDbStatement(connectionFactory);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public async Task ItShouldRequireChannelId()
        {
            await this.target.ExecuteAsync(null);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateChannelAsync(testDatabase);
                await this.target.ExecuteAsync(ChannelId);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldDeleteChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var expectedDeletion = await this.CreateChannelAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId);

                return new ExpectedSideEffects
                {
                    Delete = expectedDeletion,
                    ExcludedFromTest = entity =>
                        entity is WeeklyReleaseTime ||
                        entity is Collection ||
                        entity is Post 
                };
            });
        }

        private async Task<Channel> CreateChannelAsync(TestDatabaseContext testDatabase)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreatePopulatedTestCollectionAsync(Guid.NewGuid(), ChannelId.Value, Guid.NewGuid());
            }

            using (var databaseContext = testDatabase.CreateContext())
            {
                var channelId = ChannelId.Value;
                return await databaseContext.Channels.FirstAsync(_ => _.Id == channelId);
            }
        }
    } 
}