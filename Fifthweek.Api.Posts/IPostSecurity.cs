namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IPostSecurity
    {
        Task<bool> IsDeletionAllowedAsync(UserId requester, PostId postId);

        Task AssertDeletionAllowedAsync(UserId requester, PostId postId);
    }
}