namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Data.Entity;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Payments;
    using Fifthweek.Api.Persistence.Snapshots;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class FifthweekDbContext : IdentityDbContext<FifthweekUser, FifthweekRole, Guid, FifthweekUserLogin, FifthweekUserRole, FifthweekUserClaim>, IFifthweekDbContext
    {
        public FifthweekDbContext()
            : base(FifthweekDbConnectionFactory.DefaultConnectionStringKey)
        {
        }

        public FifthweekDbContext(string connectionString)
            : base(connectionString)
        {
        }

        public IDbSet<RefreshToken> RefreshTokens { get; set; }

        public IDbSet<Blog> Blogs { get; set; }

        public IDbSet<Channel> Channels { get; set; }
        
        public IDbSet<Collection> Collections { get; set; }

        public IDbSet<WeeklyReleaseTime> WeeklyReleaseTimes { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<File> Files { get; set; }

        public IDbSet<EndToEndTestEmail> EndToEndTestEmails { get; set; }

        public IDbSet<FreeAccessUser> FreeAccessUsers { get; set; }

        public IDbSet<ChannelSubscription> ChannelSubscriptions { get; set; }

        public IDbSet<SubscriberSnapshot> SubscriberSnapshots { get; set; }

        public IDbSet<SubscriberChannelsSnapshot> SubscriberChannelsSnapshots { get; set; }

        public IDbSet<SubscriberChannelsSnapshotItem> SubscriberChannelsSnapshotItems { get; set; }

        public IDbSet<CreatorChannelsSnapshot> CreatorChannelsSnapshots { get; set; }

        public IDbSet<CreatorChannelsSnapshotItem> CreatorChannelsSnapshotItems { get; set; }

        public IDbSet<CreatorFreeAccessUsersSnapshot> CreatorFreeAccessUsersSnapshots { get; set; }

        public IDbSet<CreatorFreeAccessUsersSnapshotItem> CreatorFreeAccessUsersSnapshotItems { get; set; }

        public IDbSet<AppendOnlyLedgerRecord> AppendOnlyLedgerRecords { get; set; }

        public IDbSet<UncommittedSubscriptionPayment> UncommittedSubscriptionPayments { get; set; }

        public IDbSet<CalculatedAccountBalance> CalculatedAccountBalances { get; set; }

        public IDbSet<CreatorPercentageOverride> CreatorPercentageOverrides { get; set; }

        public IDbSet<UserPaymentOrigin> UserPaymentOrigins { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // This makes sure creator percentage can never be greater than 100%!
            // Technically this forces it to be < 1.0, so even 100% is impossible, but
            // in reality we would never give the creator 100% as we would make a loss on the
            // transaction fees.
            modelBuilder.Entity<CreatorPercentageOverride>().Property(x => x.Percentage).HasPrecision(9, 9);

            base.OnModelCreating(modelBuilder);
        }

        public static FifthweekDbContext Create()
        {
            return new FifthweekDbContext();
        }
    }
}