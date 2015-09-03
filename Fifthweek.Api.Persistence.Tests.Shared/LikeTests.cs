namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    public static class LikeTests
    {
        public static Like Unique(Random random)
        {
            return new Like(
                Guid.NewGuid(),
                null,
                Guid.NewGuid(),
                null,
                DateTime.UtcNow);
        }
    }
}