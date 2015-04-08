namespace Fifthweek.Api.Channels.Tests.Commands
{
    using System;
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
    public class UpdateChannelCommandHandlerTests : PersistenceTestsBase
    {
        private const bool IsVisibleToNonSubscribers = false;
        private static readonly UserId UserId = new UserId(Guid.NewGuid());
        private static readonly Requester Requester = Requester.Authenticated(UserId);
        private static readonly ChannelId ChannelId = new ChannelId(Guid.NewGuid());
        private static readonly ValidChannelName Name = ValidChannelName.Parse("Bat puns");
        private static readonly ValidChannelDescription Description = ValidChannelDescription.Parse("Bat puns\nBadPuns");
        private static readonly ValidChannelPriceInUsCentsPerWeek Price = ValidChannelPriceInUsCentsPerWeek.Parse(10);
        private static readonly UpdateChannelCommand Command = new UpdateChannelCommand(Requester, ChannelId, Name, Description, Price, IsVisibleToNonSubscribers);
        
        private Mock<IRequesterSecurity> requesterSecurity;
        private Mock<IChannelSecurity> channelSecurity;
        private Mock<IFifthweekDbConnectionFactory> connectionFactory;
        private UpdateChannelCommandHandler target;

        [TestInitialize]
        public void Initialize()
        {
            this.requesterSecurity = new Mock<IRequesterSecurity>();
            this.requesterSecurity.SetupFor(Requester);
            this.channelSecurity = new Mock<IChannelSecurity>();

            // Give potentially side-effective components strict mock behaviour.
            this.connectionFactory = new Mock<IFifthweekDbConnectionFactory>(MockBehavior.Strict);

            this.InitializeTarget(this.connectionFactory.Object);
        }

        public void InitializeTarget(IFifthweekDbConnectionFactory connectionFactory)
        {
            this.target = new UpdateChannelCommandHandler(this.requesterSecurity.Object, this.channelSecurity.Object, connectionFactory);
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
            await this.target.HandleAsync(new UpdateChannelCommand(Requester.Unauthenticated, ChannelId, Name, Description, Price, IsVisibleToNonSubscribers));
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
                await this.CreateChannelAsync(testDatabase);
                await this.target.HandleAsync(Command);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                return ExpectedSideEffects.None;
            });
        }

        [TestMethod]
        public async Task ItShouldUpdateChannel()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateChannelAsync(testDatabase);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(Command);

                var expectedChannel = new Channel(ChannelId.Value)
                {
                    IsVisibleToNonSubscribers = IsVisibleToNonSubscribers,
                    Name = Name.Value,
                    Description = Description.Value,
                    PriceInUsCentsPerWeek = Price.Value
                };

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actual =>
                        {
                            expectedChannel.BlogId = actual.BlogId;
                            expectedChannel.CreationDate = actual.CreationDate;
                            return expectedChannel;
                        }
                    }
                };
            });
        }

        [TestMethod]
        public async Task ItShouldNotAllowDefaultChannelToBeHidden()
        {
            await this.DatabaseTestAsync(async testDatabase =>
            {
                this.InitializeTarget(testDatabase);
                await this.CreateChannelAsync(testDatabase, createAsDefaultChannel: true);
                await testDatabase.TakeSnapshotAsync();

                await this.target.HandleAsync(new UpdateChannelCommand(Requester, ChannelId, Name, Description, Price, isVisibleToNonSubscribers: false));

                var expectedChannel = new Channel(ChannelId.Value)
                {
                    IsVisibleToNonSubscribers = true,
                    Name = Name.Value,
                    Description = Description.Value,
                    PriceInUsCentsPerWeek = Price.Value
                };

                return new ExpectedSideEffects
                {
                    Update = new WildcardEntity<Channel>(expectedChannel)
                    {
                        Expected = actual =>
                        {
                            expectedChannel.BlogId = actual.BlogId;
                            expectedChannel.CreationDate = actual.CreationDate;
                            return expectedChannel;
                        }
                    }
                };
            });
        }

        private async Task CreateChannelAsync(TestDatabaseContext testDatabase, bool createAsDefaultChannel = false)
        {
            using (var databaseContext = testDatabase.CreateContext())
            {
                await databaseContext.CreateTestChannelAsync(UserId.Value, ChannelId.Value, createAsDefaultChannel ? ChannelId.Value : Guid.NewGuid());
                await databaseContext.Database.Connection.UpdateAsync(
                    new Channel(ChannelId.Value) { IsVisibleToNonSubscribers = true },
                    Channel.Fields.IsVisibleToNonSubscribers);
            }
        }
    } 
}