namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface IUnlikePostDbStatement
    {
        Task ExecuteAsync(UserId userId, PostId postId);
    }
}