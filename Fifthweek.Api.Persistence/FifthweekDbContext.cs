namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Data.Common;
    using System.Data.Entity;

    using Fifthweek.Api.Persistence.Identity;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekDbContext : IdentityDbContext<FifthweekUser, FifthweekRole, Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>, IFifthweekDbContext
    {
        public FifthweekDbContext()
            : base(FifthweekDbConnectionFactory.DefaultConnectionString)
        {
        }

        public FifthweekDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IDbSet<Subscription> Subscriptions { get; set; }

        public IDbSet<Channel> Channels { get; set; }
        
        public IDbSet<Collection> Collections { get; set; }

        public IDbSet<WeeklyReleaseTime> WeeklyReleaseTimes { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<File> Files { get; set; }

        public static FifthweekDbContext Create()
        {
            return new FifthweekDbContext();
        }
    }
}