﻿namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    public static class ChannelTests
    {
        public static Channel UniqueEntity(Random random, bool createForeignEntities)
        {
            var subscription = createForeignEntities ? SubscriptionTests.UniqueEntity(random) : null;
            return new Channel(
                Guid.NewGuid(),
                subscription != null ? subscription.Id : default(Guid),
                subscription,
                random.Next(1, 100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Task CreateTestChannelAsync(this IFifthweekDbContext databaseContext, Guid newUserId, Guid newChannelId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            var subscription = SubscriptionTests.UniqueEntity(random);
            subscription.Creator = creator;
            subscription.CreatorId = creator.Id;

            var channel = ChannelTests.UniqueEntity(random, false);
            channel.Id = newChannelId;
            channel.Subscription = subscription;
            channel.SubscriptionId = subscription.Id;

            databaseContext.Channels.Add(channel);
            return databaseContext.SaveChangesAsync();
        }
    }
}