namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class SubscriptionTests
    {
        public static Subscription UniqueEntity(Random random)
        {
            var user = UserTests.UniqueEntity(random);
            var headerImage = random.Next(1) == 1 ? null : FileTests.UniqueEntity(random);
            return new Subscription(
                Guid.NewGuid(),
                user.Id,
                user,
                "Name " + random.Next(),
                "Subscription tagline " + random.Next(),
                "Subscription intro " + random.Next(),
                random.Next(1) == 1 ? null : "Subscription description " + random.Next(),
                random.Next(1) == 1 ? null : "http://external/video" + random.Next(),
                headerImage == null ? (Guid?)null : headerImage.Id,
                headerImage,
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }
    }
}