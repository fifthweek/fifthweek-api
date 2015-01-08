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

        public FifthweekDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IDbSet<Subscription> Subscriptions { get; set; }

        public IDbSet<Channel> Channels { get; set; }

        public static FifthweekDbContext Create()
        {
            return new FifthweekDbContext();
        }
    }
}