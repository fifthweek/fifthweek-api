namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IRequestSnapshotService
    {
        Task ExecuteAsync(UserId userId, SnapshotType snapshotType);
    }
}