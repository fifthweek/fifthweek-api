namespace Fifthweek.Api.Posts.Commands
{
    using System.Collections.Generic;

    using Fifthweek.Api.Collections;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class ReorderQueueCommand
    {
        public Requester Requester { get; private set; }

        public CollectionId CollectionId { get; private set; }

        public IReadOnlyList<PostId> NewPartialQueueOrder { get; private set; } 
    }
}