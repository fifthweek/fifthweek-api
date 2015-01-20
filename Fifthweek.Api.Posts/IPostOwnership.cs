namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    public interface IPostOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, PostId postId);
    }
}