namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface IRequestFreePostDbStatement
    {
        Task ExecuteAsync(UserId requestorId, PostId postId);
    }
}