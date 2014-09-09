namespace Dexter.Api.Repositories
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;

    using Dexter.Api.Entities;

    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IDexterDbContext dexterDbContext;

        public RefreshTokenRepository(IDexterDbContext dexterDbContext)
        {
            this.dexterDbContext = dexterDbContext;
        }

        public Task<RefreshToken> TryGetRefreshTokenAsync(string username, string clientId)
        {
            return this.dexterDbContext.RefreshTokens.SingleOrDefaultAsync(r => r.Username == username && r.ClientId == clientId);
        }

        public Task<RefreshToken> TryGetRefreshTokenAsync(string hashedTokenId)
        {
            // TODO: This cast is dodgy. Perhaps use solution such as these:
            // http://stackoverflow.com/questions/21800967/no-findasync-method-on-idbsett
            // http://stackoverflow.com/questions/23730949/adapter-pattern-for-idbset-properties-of-a-dbcontext-class
            var set = (DbSet<RefreshToken>)this.dexterDbContext.RefreshTokens;
            return set.FindAsync(hashedTokenId);
        }

        public Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            this.dexterDbContext.RefreshTokens.Add(refreshToken);
            return Task.FromResult(0);
        }

        public Task RemoveRefreshTokenAsync(RefreshToken refreshToken)
        {
            this.dexterDbContext.RefreshTokens.Remove(refreshToken);
            return Task.FromResult(0);
        }

        public Task<List<RefreshToken>> GetAllRefreshTokensAsync()
        {
            return Task.FromResult(this.dexterDbContext.RefreshTokens.ToList());
        }
    }
}