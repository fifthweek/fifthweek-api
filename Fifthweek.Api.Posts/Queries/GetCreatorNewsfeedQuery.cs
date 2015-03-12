namespace Fifthweek.Api.Posts.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetCreatorNewsfeedQuery : IQuery<IReadOnlyList<GetCreatorNewsfeedQueryResult>>
    {
        public Requester Requester { get; private set; }

        public UserId RequestedUserId { get; private set; }

        public NonNegativeInt StartIndex { get; private set; }

        public PositiveInt Count { get; private set; }
    }
}