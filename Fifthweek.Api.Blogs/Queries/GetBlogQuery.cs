namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Core;
    using Fifthweek.CodeGeneration;

    [AutoEqualityMembers, AutoConstructor]
    public partial class GetBlogQuery : IQuery<GetBlogResult>
    {
        public BlogId NewBlogId { get; private set; }
    }
}