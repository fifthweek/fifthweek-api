namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Blogs.Shared;
    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IDoesUserHaveFreeAccessDbStatement
    {
        Task<bool> ExecuteAsync(BlogId blogId, UserId userId);
    }
}