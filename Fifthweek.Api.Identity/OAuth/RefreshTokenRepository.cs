namespace Fifthweek.Api.Identity.OAuth
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Dapper;

    using Fifthweek.Api.Persistence;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IFifthweekDbContext fifthweekDbContext;

        public RefreshTokenRepository(IFifthweekDbContext fifthweekDbContext)
        {
            this.fifthweekDbContext = fifthweekDbContext;
        }

        public Task<RefreshToken> TryGetRefreshTokenAsync(string username, string clientId)
        {
            return this.fifthweekDbContext.RefreshTokens.SingleOrDefaultAsync(r => r.Username == username && r.ClientId == clientId);
        }

        public async Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId)
        {
            var results = await this.fifthweekDbContext.Database.Connection.QueryAsync<RefreshToken>(
                @"SELECT * FROM RefreshTokens WHERE HashedId=@hashedTokenId", new { hashedTokenId });
            return results.Single();
        }

        public Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            this.fifthweekDbContext.RefreshTokens.Add(refreshToken);
            return this.fifthweekDbContext.SaveChangesAsync();
        }

        public Task RemoveRefreshTokens(string username, string clientId)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                @"DELETE FROM RefreshTokens WHERE Username=@username AND ClientId=@clientId", 
                new { username, clientId });
        }

        public Task RemoveRefreshToken(string hashedTokenId)
        {
            return this.fifthweekDbContext.Database.Connection.ExecuteAsync(
                @"DELETE FROM RefreshTokens WHERE HashedId=@hashedTokenId",
                new { hashedTokenId });
        }
    }
}