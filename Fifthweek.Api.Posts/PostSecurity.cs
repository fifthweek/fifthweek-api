namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Membership;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    using UserId = Fifthweek.Api.Identity.Shared.Membership.UserId;

    [AutoConstructor]
    public partial class PostSecurity : IPostSecurity
    {
        private readonly IPostOwnership postOwnership;

        public Task<bool> IsDeletionAllowedAsync(UserId requester, PostId postId)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return this.postOwnership.IsOwnerAsync(requester, postId);
        }

        public async Task AssertDeletionAllowedAsync(UserId requester, PostId postlId)
        {
            requester.AssertNotNull("requester");
            postlId.AssertNotNull("postId");

            var isPostingAllowed = await this.IsDeletionAllowedAsync(requester, postlId);
            if (!isPostingAllowed)
            {
                throw new UnauthorizedException(string.Format("Not allowed to delete post. {0} {1}", requester, postlId));
            }
        }
    }
}