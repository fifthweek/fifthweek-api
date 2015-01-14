namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class PostTests
    {
        public static Post UniqueEntity(Random random, bool createForeignEntities)
        {
            switch (random.Next(2))
            {
                case 0:
                    return UniqueFile(random, createForeignEntities);

                case 1:
                    return UniqueFile(random, createForeignEntities);

                default:
                    return UniqueFile(random, createForeignEntities);
            }
        }

        public static Post UniqueFile(Random random, bool createForeignEntities)
        {
            return UniqueFileOrImage(random, true, createForeignEntities);
        }

        public static Post UniqueImage(Random random, bool createForeignEntities)
        {
            return UniqueFileOrImage(random, false, createForeignEntities);
        }

        public static Post UniqueNote(Random random, bool createForeignEntities)
        {
            var channel = createForeignEntities ? ChannelTests.UniqueEntity(random, true) : null;

            return new Post(
                Guid.NewGuid(),
                channel != null ? channel.Id : default(Guid),
                channel,
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

        private static Post UniqueFileOrImage(Random random, bool isFile, bool createForeignEntities)
        {
            var channel = createForeignEntities ? ChannelTests.UniqueEntity(random, true) : null;
            var collection = createForeignEntities ? CollectionTests.UniqueEntity(random, true) : null;
            var file = createForeignEntities ? FileTests.UniqueEntity(random) : null;

            var isQueued = random.Next(1) == 1;
            var hasComment = random.Next(1) == 1;

            return new Post(
                Guid.NewGuid(),
                channel != null ? channel.Id : default(Guid),
                channel,
                collection == null ? (Guid?)null : collection.Id,
                collection,
                file != null ? (isFile ? file.Id : (Guid?)null) : null,
                isFile ? file : null,
                file != null ? (!isFile ? file.Id : (Guid?)null) : null,
                !isFile ? file : null,
                hasComment ? "Comment " + random.Next() : null,
                isQueued ? random.Next(100) : (int?)null,
                isQueued ? (DateTime?)null : DateTime.UtcNow.AddDays((random.NextDouble() * -100) + (random.NextDouble() * 100)),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100));
        }
    }
}