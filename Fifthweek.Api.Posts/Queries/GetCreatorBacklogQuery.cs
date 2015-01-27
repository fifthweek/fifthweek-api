namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatorBacklogQuery : IQuery<IReadOnlyList<BacklogPost>>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }
    }
}