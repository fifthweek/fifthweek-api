namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class PostTests
    {
        public static Post UniqueNote(Random random)
        {
            return new Post(
                Guid.NewGuid(),
                default(Guid),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                "Note " + random.Next(),
                null,
                DateTime.UtcNow.AddDays((random.NextDouble() * -100) + (random.NextDouble() * 100)),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }

        public static Post UniqueFileOrImage(Random random)
        {
            var isQueued = random.Next(1) == 1;
            var hasComment = random.Next(1) == 1;

            return new Post(
                Guid.NewGuid(),
                default(Guid),
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                hasComment ? "Comment " + random.Next() : null,
                isQueued ? random.Next(100) : (int?)null,
                isQueued ? (DateTime?)null : DateTime.UtcNow.AddDays((random.NextDouble() * -100) + (random.NextDouble() * 100)),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }
    }
}