using System;

namespace Fifthweek.Api.Persistence.Tests.Shared
{
    public static class SubscriptionTests
    {
        public static Subscription UniqueEntity()
        {
            var random = new Random();
            var user = UserTests.UniqueEntity();
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