namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class CollectionTests
    {
        public static Collection UniqueEntity(Random random, bool createForeignEntities)
        {
            var channel = createForeignEntities ? ChannelTests.UniqueEntity(random, true) : null;
            return new Collection(
                Guid.NewGuid(),
                channel != null ? channel.Id : default(Guid),
                channel,
                "Channel " + random.Next());
        }
    }
}