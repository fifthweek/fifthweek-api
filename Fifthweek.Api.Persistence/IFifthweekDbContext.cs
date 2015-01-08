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

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}