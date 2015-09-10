namespace Fifthweek.Api.Blogs.Queries
{
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class GetBlogChannelsAndQueuesResult
    {
        public BlogWithFileInformation Blog { get; private set; }
    }
}