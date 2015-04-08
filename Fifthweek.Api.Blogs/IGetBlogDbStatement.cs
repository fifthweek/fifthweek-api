namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;

    public interface IGetBlogDbStatement
    {
        Task<GetBlogDbStatement.GetBlogDataDbResult> ExecuteAsync(BlogId blogId);
    }
}