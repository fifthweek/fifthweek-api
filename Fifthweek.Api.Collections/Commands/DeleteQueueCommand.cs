namespace Fifthweek.Api.Collections.Commands
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class DeleteQueueCommand
    {
        public Requester Requester { get; private set; }

        public QueueId QueueId { get; private set; }
    }
}