namespace Fifthweek.Api.FileManagement.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserAccessSignaturesQuery : IQuery<UserAccessSignatures>
    {
        public Requester Requester { get; private set; }

        [Optional]
        public UserId RequestedUserId { get; private set; }

        [Optional]
        public IReadOnlyList<ChannelId> CreatorChannelIds { get; private set; }

        [Optional]
        public IReadOnlyList<ChannelId> SubscribedChannelIds { get; private set; }

        [Optional]
        public IReadOnlyList<ChannelId> FreeAccessChannelIds { get; private set; }
    }
}