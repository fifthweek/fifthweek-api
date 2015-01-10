using System;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public static class SubscriptionTests
    {
        public static Subscription UniqueEntity(Random random)
        {
            var user = UserTests.UniqueEntity(random);
            return new Subscription(
                Guid.NewGuid(),
                user,
                user.Id,
                "Subscription name " + random.Next(),
                "Subscription tagline " + random.Next(),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }
    }
}