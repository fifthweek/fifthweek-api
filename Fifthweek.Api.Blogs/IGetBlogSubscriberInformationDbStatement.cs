namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;

    public interface IGetBlogSubscriberInformationDbStatement
    {
        Task<GetBlogSubscriberInformationDbStatement.GetBlogSubscriberInformationDbStatementResult> ExecuteAsync(BlogId blogId);
    }
}