namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence;

    public static class FileTests
    {
        public static File UniqueEntity(Random random)
        {
            return new File(
                Guid.NewGuid(),
                null,
                default(Guid),
                (FileState)random.Next(0, 3),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                "File Name " + random.Next(),
                "ext" + random.Next(100),
                random.Next(),
                "Purpose " + random.Next());
        }

        public static Task CreateTestFileWithExistingUserAsync(this IFifthweekDbContext databaseContext, Guid existingUserId, Guid newFileId)
        {
            var random = new Random();

            var file = UniqueEntity(random);
            file.Id = newFileId;
            file.UserId = existingUserId;

            return databaseContext.Database.Connection.InsertAsync(file);
        }
    }
}