namespace Fifthweek.Api.Posts
{
    using System;
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostSecurity : IPostSecurity
    {
        private readonly IIsPostOwnerDbStatement isPostOwner;
        private readonly IIsPostSubscriberDbStatement isPostSubscriber;

        public Task<bool> IsWriteAllowedAsync(UserId requester, PostId postId)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return this.isPostOwner.ExecuteAsync(requester, postId);
        }

        public async Task AssertWriteAllowedAsync(UserId requester, PostId postId)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isPostingAllowed = await this.IsWriteAllowedAsync(requester, postId);
            if (!isPostingAllowed)
            {
                throw new UnauthorizedException("Not allowed to write post. {0} {1}", requester, postId);
            }
        }

        public async Task<bool> IsReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return await this.isPostSubscriber.ExecuteAsync(requester, postId, timestamp)
                || await this.isPostOwner.ExecuteAsync(requester, postId);
        }

        public async Task AssertReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isAllowed = await this.IsReadAllowedAsync(requester, postId, timestamp);
            if (!isAllowed)
            {
                throw new UnauthorizedException("Not allowed to read post. {0} {1}", requester, postId);
            }
        }

        public async Task<bool> IsCommentOrLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return await this.isPostSubscriber.ExecuteAsync(requester, postId, timestamp)
                || await this.isPostOwner.ExecuteAsync(requester, postId);
        }

        public async Task AssertCommentOrLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isCommentOrLikeAllowed = await this.IsCommentOrLikeAllowedAsync(requester, postId, timestamp);
            if (!isCommentOrLikeAllowed)
            {
                throw new UnauthorizedException("Not allowed to like or comment on post. {0} {1}", requester, postId);
            }
        }
    }
}