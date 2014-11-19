namespace Fifthweek.Api.Repositories
{
    using System.Data.Entity;

    using Fifthweek.Api.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekDbContext : IdentityDbContext<IdentityUser>, IFifthweekDbContext
    {
        public FifthweekDbContext()
            : base("FifthweekDbContext")
        {
        }

        public IDbSet<Client> Clients { get; set; }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }
    }
}