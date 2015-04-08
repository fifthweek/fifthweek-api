namespace Fifthweek.Api.Blogs
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IBlogOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.BlogId blogId);
    }
}