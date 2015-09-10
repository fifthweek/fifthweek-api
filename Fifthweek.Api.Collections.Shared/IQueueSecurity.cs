namespace Fifthweek.Api.Collections.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IQueueSecurity
    {
        Task<bool> IsWriteAllowedAsync(UserId requester, QueueId queueId);

        Task AssertWriteAllowedAsync(UserId requester, QueueId queueId);
    }
}