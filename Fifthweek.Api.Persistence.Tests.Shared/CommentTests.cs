namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;

    public static class CommentTests
    {
        public static Comment Unique(Random random)
        {
            return new Comment(
                Guid.NewGuid(),
                Guid.NewGuid(),
                null,
                Guid.NewGuid(),
                null,
                "Comment " + random.Next(),
                new SqlDateTime(DateTime.UtcNow.AddSeconds(random.Next(-100, 100))).Value);
        }
    }
}