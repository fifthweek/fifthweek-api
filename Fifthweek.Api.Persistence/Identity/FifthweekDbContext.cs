namespace Fifthweek.Api.Persistence.Identity
{
    using System;
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekDbContext : IdentityDbContext<FifthweekUser, FifthweekRole, Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>, IFifthweekDbContext
    {
        public FifthweekDbContext()
            : base("FifthweekDbContext")
        {
        }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public static FifthweekDbContext Create()
        {
            return new FifthweekDbContext();
        }
    }
}