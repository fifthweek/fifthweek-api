namespace Fifthweek.Api.Blogs.Queries
{
    using System.Collections.Generic;

    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetFreeAccessUsersResult
    {
        public IReadOnlyList<FreeAccessUser> FreeAccessUsers { get; private set; }

        [AutoConstructor, AutoEqualityMembers]
        public partial class FreeAccessUser
        {
            public Email Email { get; private set; }

            [Optional]
            public UserId UserId { get; private set; }

            [Optional]
            public Username Username { get; private set; }

            public IReadOnlyList<ChannelId> ChannelIds { get; private set; }
        }
    }
}