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

        public Task UpdateLastSignInDateAndAccessTokenDateAsync(string username, DateTime timestamp)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                    @"UPDATE AspNetUsers SET LastSignInDate=@timestamp, LastAccessTokenDate=@timestamp WHERE UserName=@username",
                    new { username, timestamp });
        }

        public Task UpdateLastAccessTokenDateAsync(string username, DateTime timestamp)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                   @"UPDATE AspNetUsers SET LastAccessTokenDate=@timestamp WHERE UserName=@username",
                   new { username, timestamp });
        }
    }
}