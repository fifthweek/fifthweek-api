using System;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public static class ChannelTests
    {
        public static Channel UniqueEntity(Random random)
        {
            var subscription = SubscriptionTests.UniqueEntity(random);
            return new Channel(
                Guid.NewGuid(),
                subscription.Id,
                subscription,
                random.Next(1, 100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }
    }
}