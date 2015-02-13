namespace Fifthweek.Api.Persistence
{
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Persistence.Identity;

    public interface IFifthweekDbContext : IDisposable
    {
        Database Database { get; }

        IDbSet<RefreshToken> RefreshTokens { get; }

        IDbSet<FifthweekUser> Users { get; }

        IDbSet<FifthweekRole> Roles { get; }

        IDbSet<Subscription> Subscriptions { get; }

        IDbSet<Channel> Channels { get; }

        IDbSet<Collection> Collections { get; }

        IDbSet<WeeklyReleaseTime> WeeklyReleaseTimes { get; }

        IDbSet<Post> Posts { get; }

        IDbSet<File> Files { get; set; }

        IDbSet<EndToEndTestEmail> EndToEndTestEmails { get; set; }

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}