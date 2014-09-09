namespace Dexter.Api.Repositories
{
    using System.Data.Entity;

    using Dexter.Api.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class DexterDbContext : IdentityDbContext<IdentityUser>, IDexterDbContext
    {
        public DexterDbContext()
            : base("DexterDbContext")
        {
        }

        public IDbSet<Client> Clients { get; set; }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }
    }
}