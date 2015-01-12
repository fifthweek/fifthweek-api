namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;

    using Fifthweek.Api.Persistence;
    using Fifthweek.Api.Persistence.Tests.Shared;

    public static class FileTests
    {
        public static File UniqueEntity(Random random)
        {
            var user = UserTests.UniqueEntity(random);
            return new File(
                Guid.NewGuid(),
                user,
                user.Id,
                (FileState)random.Next(0, 3),
                DateTime.UtcNow,
                DateTime.UtcNow,
                DateTime.UtcNow,
                DateTime.UtcNow,
                "File Name " + random.Next(),
                "File Extension " + random.Next(),
                random.Next(),
                "Purpose " + random.Next());
        }
    }
}