namespace Fifthweek.Api.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Threading;
    using System.Threading.Tasks;

    using Fifthweek.Api.Entities;

    using Microsoft.AspNet.Identity.EntityFramework;

    public interface IFifthweekDbContext : IDisposable
    {
        IDbSet<Client> Clients { get; set; }

        IDbSet<RefreshToken> RefreshTokens { get; set; }

        IDbSet<IdentityUser> Users { get; set; }

        IDbSet<IdentityRole> Roles { get; set; }

        Task<int> SaveChangesAsync();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}