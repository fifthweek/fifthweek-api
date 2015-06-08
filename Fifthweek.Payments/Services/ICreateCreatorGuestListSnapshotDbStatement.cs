namespace Fifthweek.Payments.Services
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface ICreateCreatorGuestListSnapshotDbStatement
    {
        Task ExecuteAsync(UserId creatorId);
    }
}