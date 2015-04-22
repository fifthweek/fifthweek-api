namespace Fifthweek.Api.Posts.Queries
{
    using System;
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Collections.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetNewsfeedQuery : IQuery<GetNewsfeedQueryResult>
    {
        public Requester Requester { get; private set; }

        [Optional]
        public UserId CreatorId { get; private set; }

        [Optional]
        public IReadOnlyList<ChannelId> ChannelIds { get; private set; }

        [Optional]
        public IReadOnlyList<CollectionId> CollectionIds { get; private set; }

        [Optional]
        public DateTime? Origin { get; private set; }

        public bool SearchForwards { get; private set; }

        public NonNegativeInt StartIndex { get; private set; }

        public PositiveInt Count { get; private set; }
    }
}