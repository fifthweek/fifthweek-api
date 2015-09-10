namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IQueueOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.QueueId queueId);
    }
}