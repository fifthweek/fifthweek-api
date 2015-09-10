namespace Fifthweek.Api.Collections
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class QueueSecurity : IQueueSecurity
    {
        private readonly IQueueOwnership queueOwnership;

        public Task<bool> IsWriteAllowedAsync(UserId requester, Shared.QueueId queueId)
        {
            requester.AssertNotNull("requester");
            queueId.AssertNotNull("queueId");

            return this.queueOwnership.IsOwnerAsync(requester, queueId);
        }

        public async Task AssertWriteAllowedAsync(UserId requester, Shared.QueueId queueId)
        {
            requester.AssertNotNull("requester");
            queueId.AssertNotNull("queueId");

            var isWriteAllowed = await this.IsWriteAllowedAsync(requester, queueId);
            if (!isWriteAllowed)
            {
                throw new UnauthorizedException("Not allowed to write to collection. {0} {1}", requester, queueId);
            }
        }
    }
}