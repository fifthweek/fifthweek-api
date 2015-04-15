namespace Fifthweek.Api.Blogs.Commands
{
    using System.Collections.Generic;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class UpdateBlogSubscriptionsCommand
    {
        public Requester Requester { get; private set; }

        public BlogId BlogId { get; private set; }

        public IReadOnlyList<AcceptedChannelSubscription> Channels { get; private set; }
    }
}