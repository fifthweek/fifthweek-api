namespace Fifthweek.Api.Blogs.Queries
{
    using System;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetBlogChannelsAndCollectionsQuery : IQuery<GetBlogChannelsAndCollectionsResult>
    {
        public BlogId BlogId { get; private set; }
    }
}