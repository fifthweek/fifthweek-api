namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;
    using Fifthweek.Api.Persistence.Snapshots;

    public interface IFifthweekDbContext : IDisposable
    {
        Database Database { get; }

        IDbSet<RefreshToken> RefreshTokens { get; }

        IDbSet<FifthweekUser> Users { get; }

        IDbSet<FifthweekRole> Roles { get; }

        IDbSet<Blog> Blogs { get; }

        IDbSet<Channel> Channels { get; }

        IDbSet<Collection> Collections { get; }

        IDbSet<WeeklyReleaseTime> WeeklyReleaseTimes { get; }

        IDbSet<Post> Posts { get; }

        IDbSet<File> Files { get; set; }

        IDbSet<EndToEndTestEmail> EndToEndTestEmails { get; set; }

        IDbSet<FreeAccessUser> FreeAccessUsers { get; set; }

        IDbSet<ChannelSubscription> ChannelSubscriptions { get; set; }

        IDbSet<SubscriberSnapshot> SubscriberSnapshots { get; set; }

        IDbSet<SubscriberChannelsSnapshot> SubscriberChannelsSnapshots { get; set; }

        IDbSet<SubscriberChannelsSnapshotItem> SubscriberChannelsSnapshotItems { get; set; }

        IDbSet<CreatorChannelsSnapshot> CreatorChannelsSnapshots { get; set; }

        IDbSet<CreatorChannelsSnapshotItem> CreatorChannelsSnapshotItems { get; set; }

        IDbSet<CreatorFreeAccessUsersSnapshot> CreatorFreeAccessUsersSnapshots { get; set; }

        IDbSet<CreatorFreeAccessUsersSnapshotItem> CreatorFreeAccessUsersSnapshotItems { get; set; }

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}