namespace Fifthweek.Api.Posts.Commands
{
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class RescheduleWithQueueCommand
    {
        public Requester Requester { get; private set; }

        public PostId PostId { get; private set; }

        public QueueId QueueId { get; private set; }  
    }
}