namespace Fifthweek.Api.Posts.Shared
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IPostSecurity
    {
        Task<bool> IsWriteAllowedAsync(UserId requester, PostId postId);

        Task AssertWriteAllowedAsync(UserId requester, PostId postId);
    }
}