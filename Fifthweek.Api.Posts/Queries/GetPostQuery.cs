namespace Fifthweek.Api.Posts.Queries
{
    using System;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetPostQuery : IQuery<GetPostQueryResult>
    {
        public Requester Requester { get; private set; }
        
        public PostId PostId { get; private set; }

        public bool RequestFreePost { get; private set; }

        public DateTime Timestamp { get; private set; }
    }
}