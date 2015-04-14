namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;

    public interface IGetBlogChannelsAndCollectionsDbStatement
    {
        Task<GetBlogChannelsAndCollectionsDbStatement.GetBlogChannelsAndCollectionsDbResult> ExecuteAsync(BlogId blogId);
    }
}