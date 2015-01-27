namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IPostSecurity
    {
        Task<bool> IsDeletionAllowedAsync(UserId requester, PostId postId);

        Task AssertDeletionAllowedAsync(UserId requester, PostId postId);
    }
}