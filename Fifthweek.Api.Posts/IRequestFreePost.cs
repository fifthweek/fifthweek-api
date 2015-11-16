namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;

    public interface IRequestFreePost
    {
        Task ExecuteAsync(UserId requestorId, PostId postId, DateTime now);
    }
}