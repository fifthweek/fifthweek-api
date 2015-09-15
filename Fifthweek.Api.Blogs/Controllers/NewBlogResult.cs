namespace Fifthweek.Api.Blogs.Controllers
{
    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Channels.Shared;
    using Fifthweek.CodeGeneration;

    [AutoConstructor, AutoEqualityMembers]
    public partial class NewBlogResult
    {
        public BlogId BlogId { get; private set; }

        public ChannelId ChannelId { get; private set; }
    }
}