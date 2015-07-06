namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;

    public interface IGetIsTestUserBlogDbStatement
    {
        Task<bool> ExecuteAsync(BlogId blogId);
    }
}