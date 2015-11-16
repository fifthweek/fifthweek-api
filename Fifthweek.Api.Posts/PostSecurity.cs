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
        private readonly IIsPostFreeAccessUserDbStatement isPostFreeAccessUser;
        private readonly IIsFreePostDbStatement isFreePostDbStatement;

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

        public async Task<PostSecurityResult> IsReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            if (await this.isPostSubscriber.ExecuteAsync(requester, postId, timestamp))
            {
                return PostSecurityResult.Subscriber;
            }
            
            if (await this.isPostOwner.ExecuteAsync(requester, postId))
            {
                return PostSecurityResult.Owner;
            }
            
            if (await this.isPostFreeAccessUser.ExecuteAsync(requester, postId))
            {
                return PostSecurityResult.GuestList;
            }

            if (await this.isFreePostDbStatement.ExecuteAsync(requester, postId))
            {
                return PostSecurityResult.FreePost;
            }

            return PostSecurityResult.Denied;
        }

        public async Task AssertReadAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isAllowed = await this.IsReadAllowedAsync(requester, postId, timestamp);
            if (isAllowed == PostSecurityResult.Denied)
            {
                throw new UnauthorizedException("Not allowed to read post. {0} {1}", requester, postId);
            }
        }

        public async Task<bool> IsCommentAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return await this.isPostSubscriber.ExecuteAsync(requester, postId, timestamp)
                || await this.isPostOwner.ExecuteAsync(requester, postId)
                || await this.isPostFreeAccessUser.ExecuteAsync(requester, postId);
        }

        public async Task AssertCommentAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isCommentOrLikeAllowed = await this.IsCommentAllowedAsync(requester, postId, timestamp);
            if (!isCommentOrLikeAllowed)
            {
                throw new UnauthorizedException("Not allowed to comment on post. {0} {1}", requester, postId);
            }
        }

        public async Task<bool> IsLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return await this.isPostSubscriber.ExecuteAsync(requester, postId, timestamp)
                || await this.isPostOwner.ExecuteAsync(requester, postId)
                || await this.isPostFreeAccessUser.ExecuteAsync(requester, postId)
                || await this.isFreePostDbStatement.ExecuteAsync(requester, postId);
        }

        public async Task AssertLikeAllowedAsync(UserId requester, PostId postId, DateTime timestamp)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            var isCommentOrLikeAllowed = await this.IsLikeAllowedAsync(requester, postId, timestamp);
            if (!isCommentOrLikeAllowed)
            {
                throw new UnauthorizedException("Not allowed to like post. {0} {1}", requester, postId);
            }
        }
    }
}