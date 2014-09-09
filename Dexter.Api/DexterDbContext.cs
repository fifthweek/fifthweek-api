namespace Dexter.Api
{
    using System.Data.Entity;

    using Dexter.Api.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class DexterDbContext : IdentityDbContext<IdentityUser>
    {
        public DexterDbContext()
            : base("DexterDbContext")
        {
 
        }
        
        public DbSet<Client> Clients { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}