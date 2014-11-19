namespace Fifthweek.Api.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;

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

        public Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId)
        {
            // TODO: This cast is dodgy. Perhaps use solution such as these:
            // http://stackoverflow.com/questions/21800967/no-findasync-method-on-idbsett
            // http://stackoverflow.com/questions/23730949/adapter-pattern-for-idbset-properties-of-a-dbcontext-class
            var set = (DbSet<RefreshToken>)this.fifthweekDbContext.RefreshTokens;
            return set.FindAsync(hashedTokenId);
        }

        public Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            this.fifthweekDbContext.RefreshTokens.Add(refreshToken);
            return Task.FromResult(0);
        }

        public Task RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            this.fifthweekDbContext.RefreshTokens.Remove(refreshToken);
            return Task.FromResult(0);
        }

        public Task<List<RefreshToken>> GetAllRefreshTokensAsync()
        {
            return Task.FromResult(this.fifthweekDbContext.RefreshTokens.ToList());
        }
    }
}