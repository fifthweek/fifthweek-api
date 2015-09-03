namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface ICommentOnPostDbStatement
    {
        Task ExecuteAsync(UserId userId, PostId postId, CommentId commentId, Shared.Comment content, DateTime timestamp);
    }
}