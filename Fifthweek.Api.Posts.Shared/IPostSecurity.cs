namespace Fifthweek.Api.Posts.Shared
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Identity.Shared.Membership;

    public interface IPostSecurity
    {
        Task<bool> IsWriteAllowedAsync(UserId requester, PostId postId);

        Task AssertWriteAllowedAsync(UserId requester, PostId postId);

        Task<bool> IsCommentAllowedAsync(UserId requester, PostId postId, DateTime timestamp);

        Task AssertCommentAllowedAsync(UserId requester, PostId postId, DateTime timestamp);

        Task<bool> IsLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp);

        Task AssertLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp);

        Task<PostSecurityResult> IsReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp);

        Task AssertReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp);
    }
}