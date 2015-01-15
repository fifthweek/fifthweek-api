namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class CollectionTests
    {
        public static Collection UniqueEntity(Random random)
        {
            return new Collection(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Channel " + random.Next());
        }
    }
}