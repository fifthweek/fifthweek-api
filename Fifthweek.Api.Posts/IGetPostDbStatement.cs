namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface IGetPostDbStatement
    {
        Task<GetPostDbResult> ExecuteAsync(UserId requestorId, PostId postId);
    }
}