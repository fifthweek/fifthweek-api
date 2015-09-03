namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Posts.Controllers;
    using Fifthweek.Api.Posts.Shared;

    public interface IGetCommentsDbStatement
    {
        Task<CommentsResult> ExecuteAsync(PostId postId);
    }
}