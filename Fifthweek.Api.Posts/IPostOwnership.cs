namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Membership;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    public interface IPostOwnership
    {
        Task<bool> IsOwnerAsync(UserId userId, PostId postId);
    }
}