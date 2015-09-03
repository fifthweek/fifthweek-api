namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface ILikePostDbStatement
    {
        Task ExecuteAsync(UserId userId, PostId postId, DateTime timestamp);
    }
}