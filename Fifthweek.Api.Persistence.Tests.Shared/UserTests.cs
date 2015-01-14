namespace Fifthweek.Api.Persistence.Tests.Shared
{
    using System;
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
                LastAccessTokenDate = DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                LastSignInDate = DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                RegistrationDate = DateTime.UtcNow.AddDays(random.NextDouble() * -100),
                LockoutEndDateUtc = DateTime.UtcNow.AddDays(random.NextDouble() * -100),
            };
        }

        public static Task CreateTestUserAsync(this IFifthweekDbContext databaseContext, Guid newUserId)
        {
            var random = new Random();
            var creator = UserTests.UniqueEntity(random);
            creator.Id = newUserId;

            databaseContext.Users.Add(creator);
            return databaseContext.SaveChangesAsync();
        }
    }
}