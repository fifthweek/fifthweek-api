namespace Fifthweek.Api.Posts.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IPostSecurity
    {
        Task<bool> IsDeletionAllowedAsync(UserId requester, PostId postId);

        Task AssertDeletionAllowedAsync(UserId requester, PostId postId);
    }
}