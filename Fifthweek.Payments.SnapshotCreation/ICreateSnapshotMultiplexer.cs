namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICreateSnapshotMultiplexer
    {
        Task ExecuteAsync(UserId userId, SnapshotType snapshotType);
    }
}