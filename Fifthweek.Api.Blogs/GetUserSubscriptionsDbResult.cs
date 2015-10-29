namespace Fifthweek.Api.Blogs
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetUserSubscriptionsDbResult
    {
        public IReadOnlyList<BlogSubscriptionDbStatus> Blogs { get; private set; }

        public IReadOnlyList<ChannelId> FreeAccessChannelIds { get; private set; }
    }
}