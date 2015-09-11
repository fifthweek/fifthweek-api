namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    public static class QueueTests
    {
        public static Queue UniqueEntity(Random random)
        {
            return new Queue(
                Guid.NewGuid(),
                default(Guid),
                null,
                "Collection " + random.Next(),
                new SqlDateTime(DateTime.UtcNow).Value);
        }
    }
}