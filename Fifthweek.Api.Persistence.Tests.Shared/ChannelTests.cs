namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class ChannelTests
    {
        public static Channel UniqueEntity(Random random)
        {
            return new Channel(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Name " + random.Next(1000),
                "Description " + random.Next(1000),
                random.Next(1, 100),
                random.Next(2) == 0,
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestChannelAsync(
            this IFifthweekDbContext databaseContext,
            Guid newUserId,
            Guid newChannelId,
            Guid subscriptionId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Id = subscriptionId;
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random);
            channel.Id = newChannelId;
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            databaseContext.Channels.Add(channel);
            return databaseContext.SaveChangesAsync();
        }

        public static Task CreateTestChannelAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newChannelId)
        {
            return CreateTestChannelAsync(databaseContext, newUserId, newChannelId, Guid.NewGuid());
        }
    }
}