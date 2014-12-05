namespace Fifthweek.Api.Repositories
{
    using System.Data.Entity;

    using Fifthweek.Api.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekDbContext : IdentityDbContext<ApplicationUser>, IFifthweekDbContext
    {
        public FifthweekDbContext()
            : base("FifthweekDbContext")
        {
        }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }
    }
}