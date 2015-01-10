using System;
using System.Linq;
using System.Threading.Tasks;
using Fifthweek.Api.Core;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    [AutoConstructor]
    public partial class DatabaseSeed
    {
        private const int Users = 10;
        private const int Creators = 5;
        private const int SubscriptionsPerCreator = 1; // That's all our interface supports for now!
        private const int ChannelsPerSubscription = 3;

        private readonly TemporaryDatabase database;
        
        private Random random = new Random();

        public async Task PopulateWithDummyEntitiesAsync()
        {
            using (var dbContext = database.NewDbContext())
            {
                PopulateUsers(dbContext);
                PopulateSubscriptions(dbContext);
                PopulateChannels(dbContext);

                await dbContext.SaveChangesAsync();
            }
        }

        private void PopulateUsers(IFifthweekDbContext dbContext)
        {
            for (var i = 0; i < Users; i++)
            {
                dbContext.Users.Add(UserTests.UniqueEntity(this.random));
            }
        }

        private void PopulateSubscriptions(IFifthweekDbContext dbContext)
        {
            for (var creatorIndex = 0; creatorIndex < Creators; creatorIndex++)
            {
                var creator = dbContext.Users.Local[creatorIndex];
                for (var subscriptionIndex = 0; subscriptionIndex < SubscriptionsPerCreator; subscriptionIndex++)
                {
                    var subscription = SubscriptionTests.UniqueEntity(this.random);
                    subscription.Creator = creator;
                    subscription.CreatorId = creator.Id;
                    dbContext.Subscriptions.Add(subscription);
                }
            }
        }

        private void PopulateChannels(IFifthweekDbContext dbContext)
        {
            for (var creatorIndex = 0; creatorIndex < Creators; creatorIndex++)
            {
                var creator = dbContext.Users.Local[creatorIndex];
                for (var subscriptionIndex = 0; subscriptionIndex < SubscriptionsPerCreator; subscriptionIndex++)
                {
                    var subscription = dbContext.Subscriptions.Local.Single(_ => _.Creator == creator);
                    for (var channelIndex = 0; channelIndex < ChannelsPerSubscription; channelIndex++)
                    {
                        var channel = ChannelTests.UniqueEntity(this.random);
                        channel.Subscription = subscription;
                        channel.SubscriptionId = subscription.Id;
                        dbContext.Channels.Add(channel);
                    }
                }
            }
        }
    }

    public static class DatabaseSeedExtensions
    {
        public static Task PopulateWithDummyEntitiesAsync(this TemporaryDatabase database)
        {
            return new DatabaseSeed(database).PopulateWithDummyEntitiesAsync();
        }
    }
}