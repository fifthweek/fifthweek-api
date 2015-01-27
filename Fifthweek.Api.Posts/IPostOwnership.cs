namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IPostOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, Shared.PostId postId);
    }
}