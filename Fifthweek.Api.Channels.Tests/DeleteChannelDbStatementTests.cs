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
        
        private Mock<IFifthweekDbContext> databaseContext;
        private DeleteChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.databaseContext = new Mock<IFifthweekDbContext>(MockBehavior.Strict);

            this.InitializeTarget(this.databaseContext.Object);
        }

        public void InitializeTarget(IFifthweekDbContext databaseContext)
        {
            this.target = new DeleteChannelDbStatement(databaseContext);
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
                this.InitializeTarget(testDatabase.NewContext());
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
                this.InitializeTarget(testDatabase.NewContext());
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
            using (var databaseContext = testDatabase.NewContext())
            {
                await databaseContext.CreatePopulatedTestCollectionAsync(Guid.NewGuid(), ChannelId.Value, Guid.NewGuid());
            }

            using (var databaseContext = testDatabase.NewContext())
            {
                var channelId = ChannelId.Value;
                return await databaseContext.Channels.FirstAsync(_ => _.Id == channelId);
            }
        }
    } 
}