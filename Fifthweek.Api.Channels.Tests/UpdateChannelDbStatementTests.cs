namespace Fifthweek.Api.Channels.Tests
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Channels.Commands;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Identity.Tests.Shared.Membership;
    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class UpdateChannelDbStatementTests : PersistenceTestsBase
    {
        private const bool IsVisibleToNonSubscribers = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidChannelName Name = ValidChannelName.Parse("Bat puns");
        private static readonly ValidChannelDescription Description = ValidChannelDescription.Parse("Bat puns\nBadPuns");
        private static readonly ValidChannelPriceInUsCentsPerWeek Price = ValidChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly DateTime Now = new SqlDateTime(DateTime.UtcNow).Value;

        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UpdateChannelDbStatement target;

        [TestInitialize]
        public void Initialize()
        {
            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new UpdateChannelDbStatement(connectionFactory);
        }

        [TestMethod]
        public async Task ItShouldBeIdempotent()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateChannelAsync(testDatabase);
                await this.target.ExecuteAsync(ChannelId, Name, Description, Price, IsVisibleToNonSubscribers, Now);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId, Name, Description, Price, IsVisibleToNonSubscribers, Now);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var channel = await this.CreateChannelAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId, Name, Description, Price, IsVisibleToNonSubscribers, Now);

                var expectedChannel = new Channel(ChannelId.Value)
                {
                    IsVisibleToNonSubscribers = IsVisibleToNonSubscribers,
                    Name = Name.Value,
                    Description = Description.Value,
                    PriceInUsCentsPerWeek = Price.Value,
                    PriceLastSetDate = Now,
                    BlogId = channel.BlogId,
                    CreationDate = channel.CreationDate
                };

                return new ExpectedSideEffects
                {
                    Update = expectedChannel
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotAllowDefaultChannelToBeHidden()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var channel = await this.CreateChannelAsync(testDatabase, createAsDefaultChannel: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(ChannelId, Name, Description, Price, false, Now);

                var expectedChannel = new Channel(ChannelId.Value)
                {
                    IsVisibleToNonSubscribers = true,
                    Name = Name.Value,
                    Description = Description.Value,
                    PriceInUsCentsPerWeek = Price.Value,
                    PriceLastSetDate = Now,
                    BlogId = channel.BlogId,
                    CreationDate = channel.CreationDate
                };

                return new ExpectedSideEffects
                {
                    Update = expectedChannel
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotUpdatePriceLastSetDateIfPriceHasNotChanged()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                var channel = await this.CreateChannelAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.ExecuteAsync(
                    ChannelId, 
                    Name, 
                    Description, 
                    ValidChannelPriceInUsCentsPerWeek.Parse(channel.PriceInUsCentsPerWeek), 
                    IsVisibleToNonSubscribers,
                    Now);

                var expectedChannel = new Channel(ChannelId.Value)
                {
                    IsVisibleToNonSubscribers = IsVisibleToNonSubscribers,
                    Name = Name.Value,
                    Description = Description.Value,
                    PriceInUsCentsPerWeek = channel.PriceInUsCentsPerWeek,
                    PriceLastSetDate = channel.PriceLastSetDate,
                    BlogId = channel.BlogId,
                    CreationDate = channel.CreationDate,
                };

                return new ExpectedSideEffects
                {
                    Update = expectedChannel
                };
            });
        }

        private async Task<Channel> CreateChannelAsync(TestDatabaseContext testDatabase, bool createAsDefaultChannel = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                var channel = await databaseContext.CreateTestChannelAsync(UserId.Value, ChannelId.Value, createAsDefaultChannel ? ChannelId.Value : Guid.NewGuid());

                channel.IsVisibleToNonSubscribers = true;

                await databaseContext.Database.Connection.UpdateAsync(
                    channel,
                    Channel.Fields.IsVisibleToNonSubscribers);

                return channel;
            }
        }
    }
}