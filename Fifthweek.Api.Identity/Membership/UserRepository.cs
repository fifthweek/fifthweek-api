namespace Fifthweek.Api.Identity.Membership
{
    using System;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;

    public class UserRepository : IUserRepository
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public UserRepository(IFifthweekDbContext fifthweekDbContext)
        {
            this.fifthweekDbContext = fifthweekDbContext;
        }

        public Task UpdateLastSignInDateAndAccessTokenDateAsync(NormalizedUsername username, DateTime timestamp)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                    @"UPDATE AspNetUsers SET LastSignInDate=@timestamp, LastAccessTokenDate=@timestamp WHERE UserName=@username",
                    new { username = username.Value, timestamp });
        }

        public Task UpdateLastAccessTokenDateAsync(NormalizedUsername username, DateTime timestamp)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                   @"UPDATE AspNetUsers SET LastAccessTokenDate=@timestamp WHERE UserName=@username",
                   new {username =  username.Value, timestamp });
        }
    }
}