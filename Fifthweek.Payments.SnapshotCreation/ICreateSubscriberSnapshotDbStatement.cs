namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICreateSubscriberSnapshotDbStatement
    {
        Task ExecuteAsync(UserId subscriberId);
    }
}