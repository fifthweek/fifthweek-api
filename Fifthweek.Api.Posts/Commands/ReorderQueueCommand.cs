namespace Fifthweek.Api.Posts.Commands
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ReorderQueueCommand
    {
        public Requester Requester { get; private set; }

        public QueueId QueueId { get; private set; }

        public IReadOnlyList<PostId> NewPartialQueueOrder { get; private set; } 
    }
}