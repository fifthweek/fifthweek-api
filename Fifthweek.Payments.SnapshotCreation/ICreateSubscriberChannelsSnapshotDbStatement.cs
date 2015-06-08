namespace Fifthweek.Payments.SnapshotCreation
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICreateSubscriberChannelsSnapshotDbStatement
    {
        Task ExecuteAsync(UserId subscriberId);
    }
}