namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
    using System.Data.SqlTypes;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;

    public static class UserTests
    {
        public static FifthweekUser UniqueEntity(Random random)
        {
            return new FifthweekUser
            {
                Id = Guid.NewGuid(),
                Email = string.Format("{0}@example.com", random.Next()),
                UserName = string.Format("user_{0}", random.Next()),
                LastAccessTokenDate = new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -100)).Value,
                LastSignInDate = new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -100)).Value,
                RegistrationDate = new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -100)).Value,
                LockoutEndDateUtc = new SqlDateTime(DateTime.UtcNow.AddDays(random.NextDouble() * -100)).Value,
            };
        }

        public static async Task<FifthweekUser> CreateTestUserAsync(this IFifthweekDbContext databaseContext, Guid newUserId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            databaseContext.Users.Add(creator);
            await databaseContext.SaveChangesAsync();

            return creator;
        }
    }
}