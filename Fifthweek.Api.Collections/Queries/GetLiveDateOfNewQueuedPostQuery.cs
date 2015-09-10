namespace Fifthweek.Api.Collections.Queries
{
    using System;

    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetLiveDateOfNewQueuedPostQuery : IQuery<DateTime>
    {
        public Requester Requester { get; private set; }

        public QueueId QueueId { get; private set; }
    }
}