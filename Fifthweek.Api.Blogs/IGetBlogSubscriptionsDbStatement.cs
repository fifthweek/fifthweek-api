namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Queries;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IGetBlogSubscriptionsDbStatement
    {
        Task<GetBlogSubscriptionsResult> ExecuteAsync(UserId userId);
    }
}