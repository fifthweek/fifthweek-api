namespace Fifthweek.Api.Posts
{
    using System.Threading.Tasks;

    using Fifthweek.Api.Core;
    using Fifthweek.Api.Identity.Shared.Membership;
    using Fifthweek.Api.Posts.Shared;
    using Fifthweek.CodeGeneration;
    using Fifthweek.Shared;

    [AutoConstructor]
    public partial class PostSecurity : IPostSecurity
    {
        private readonly IPostOwnership postOwnership;

        public Task<bool> IsWriteAllowedAsync(UserId requester, PostId postId)
        {
            requester.AssertNotNull("requester");
            postId.AssertNotNull("postId");

            return this.postOwnership.IsOwnerAsync(requester, postId);
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
    }
}