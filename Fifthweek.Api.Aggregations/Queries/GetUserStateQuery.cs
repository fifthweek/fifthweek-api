namespace Fifthweek.Api.Aggregations.Queries
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserStateQuery : IQuery<UserState>
    {
        public Requester Requester { get; private set; }
        
        [Optional]
        public UserId RequestedUserId { get; private set; }

        public bool Impersonate { get; private set; }

        public DateTime Now { get; private set; }
    }
}