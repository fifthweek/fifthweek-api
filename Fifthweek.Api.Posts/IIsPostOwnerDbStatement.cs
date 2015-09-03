namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IIsPostOwnerDbStatement
    {
        Task<bool> ExecuteAsync(UserId userId, Shared.PostId postId);
    }
}