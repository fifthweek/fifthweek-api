namespace Fifthweek.Api.Blogs.Queries
{
    using System;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetBlogChannelsAndQueuesQuery : IQuery<GetBlogChannelsAndQueuesResult>
    {
        public UserId UserId { get; private set; }
    }
}